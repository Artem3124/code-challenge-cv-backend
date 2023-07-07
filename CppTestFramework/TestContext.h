#include "pch.h"

#include <list>
#include <iostream>
#include <vector>

#include "TestRun.h"
#include "TimeoutJobRunner.h"
#include "AssertHelpers.h"
#include "AssertExceptionBase.h"
#include "Assert.h"
#include "TestResultWriter.h"

#ifndef TEST_CONTEXT_H
#define TEST_CONTEXT_H

class TestContext
{
	public:
        TestContext()
        {

        }

        template <typename T> TestRun runSingle(T(*actualFunc)(), T(*expectedFunc)(), int(*idFunc)())
        {
            T expectedValue = expectedFunc();
            int id = idFunc();
            T actualValue;
            std::string errorMessage;
            std::string assertErrorMessage;
            TestResult testResult;

            try
            {
                actualValue = TimeoutJobRunner::run(actualFunc);
                Assert::AreEqual(expectedValue, actualValue);
                testResult = TestResult::Succeeded;
            }
            catch (AssertExceptionBase ex)
            {
                testResult = TestResult::Failed;
                assertErrorMessage = ex.what();
            }
            catch (std::exception ex)
            {
                testResult = TestResult::Failed;
                errorMessage = ex.what();
            }

            return TestRun(
                toString_(expectedValue),
                errorMessage.length() == 0 ? toString_(actualValue) : toString_(errorMessage),
                testResult,
                TimeoutJobRunner::timeElapsedSeconds(),
                id,
                errorMessage.length() == 0 ? assertErrorMessage : errorMessage
            );
        }

        template <typename T>
        void run(T(*actualFunc)(), T(*expectedFunc)(), int(*idFunc)(), int testCasesCount)
        {
            try
            {
                if (runs.size() != 0)
                {
                    runs.clear();
                }

                TimeoutJobRunner::init(_timeLimitSeconds);
                for (int i = 0; i < testCasesCount; i++)
                {
                    runs.push_back(runSingle(actualFunc, expectedFunc, idFunc));
                }
                Log(runs);
            }
            catch (std::exception ex)
            {
                std::ofstream file;
                file.open("log.log");
                    file << ex.what();
                file.close();
            }
        }

private:
    std::vector<TestRun> runs;
    
    const double _timeLimitSeconds = 60;

    void Log(std::vector<TestRun> runs)
    {
        TestResultWriter fileWriter;
        fileWriter.WriteAsJson(runs, "TestResutls.json");
        for (TestRun run : runs)
        {
            std::cout << "Expected: " << run.expected()
                << " Actual: " << run.actual()
                << " Duration: " << run.duration()
                << " Result: " << run.result() << std::endl;
        }
    }
};

#endif // !TEST_CONTEXT_H
