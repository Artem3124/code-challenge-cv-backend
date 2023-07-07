#include "pch.h"

#include <thread>
#include <ios>
#include <windows.h>
#include <list>

#include "Core.h"

#ifndef TIMEOUT_JOB_RUNNER_H
#define TIMEOUT_JOB_RUNNER_H

class ProgressToken
{
    public:
        ProgressToken()
        {
            Done = false;
        }

        bool done()
        {
            return Done;
        }

        void setDone()
        {
            Done = true;
        }

    private:
        bool Done;
};

class TimeoutJobRunner
{
public:
    static void init(const double& timeLimitSeconds)
    {
        if (IsRunning)
        {
            throw std::runtime_error("Cannot initialize new job while running.");
        }
        TimeLimitSeconds = timeLimitSeconds;
    }

    static void timeoutCallback(ProgressToken* progressToken)
    {
        for (auto start = now(); !progressToken->done(); ElapsedTimeSeconds = TimeoutJobRunner::now() - start)
        {
            if (ElapsedTimeSeconds.count() >= TimeLimitSeconds)
            {
                throw std::runtime_error("Time limit exceeded.");
            }
        }
    }

    const static double timeElapsedSeconds()
    {
        return ElapsedTimeSeconds.count();
    }

    template <typename T> static T run(T(*job)())
    {
        if (IsRunning)
        {
            throw std::runtime_error("Cannot start new job while running.");
        }

        IsRunning = true;
        std::thread timeoutTask;
        ProgressToken* progressToken = new ProgressToken();
        T value;
        std::string runtimeError = EmptyString;

        try
        {
            timeoutTask = std::thread([progressToken, &value, &runtimeError, job]() {
                try
                {
                    value = job();
                }
                catch (const std::exception &ex)
                {
                    runtimeError = ex.what();
                }
                progressToken->setDone();
            });
            timeoutCallback(progressToken);
            if (runtimeError != EmptyString)
            {
                throw std::runtime_error(runtimeError.c_str());
            }
        }
        catch (const std::exception&)
        {
            cleanup(progressToken, timeoutTask);
            throw;
        }

        cleanup(progressToken, timeoutTask);

        return value;
    }

private:
    TimeoutJobRunner();

    static void cleanup(ProgressToken* progressToken, std::thread &task)
    {
        task.detach();
        delete progressToken;
        IsRunning = false;
    }

    const inline static std::chrono::system_clock::time_point now()
    {
        return std::chrono::system_clock::now();
    }

    static std::chrono::duration<double> ElapsedTimeSeconds;
    static double TimeLimitSeconds;
    static bool IsRunning;
};


//std::chrono::duration<double> TimeoutJobRunner::ElapsedTimeSeconds;
//double TimeoutJobRunner::TimeLimitSeconds;
//bool TimeoutJobRunner::IsRunning;

#endif // !TIMEOUT_JOB_RUNNER_H


