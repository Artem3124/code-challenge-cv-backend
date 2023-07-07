#include "pch.h"

#include <string>

#include "Core.h"

#ifndef TEST_RUN_H
#define TEST_RUN_H

enum TestResult
{
	None = 0,
	Succeeded = 1,
	Failed = 2,
};


class TestRun
{
	public:
		TestRun()
		{
			Expected = EmptyString;
			Actual = EmptyString;
			Result = TestResult::None;
			Duration = 0;
			Id = 0;
		}

		TestRun(const std::string& expected, const std::string& actual, const TestResult result, const double& duration, const int &id, const std::string &message)
		{
			Expected = expected;
			Actual = actual;
			Result = result;
			Duration = duration;
			Id = id;
			Message = message;
		}

		const std::string message()
		{
			return Message;
		}

		const std::string expected()
		{
			return Expected;
		}

		const std::string actual()
		{
			return Actual;
		}

		const TestResult result()
		{
			return Result;
		}

		const double duration()
		{
			return Duration;
		}

		const int id()
		{
			return Id;
		}

		const void expected(const std::string &value)
		{
			Expected = value;
		}

		const void actual(const std::string &value)
		{
			Actual = value;
		}

		const void result(const TestResult value)
		{
			Result = value;
		}

		const void duration(const double& value)
		{
			Duration = value;
		}

		const void id(int value)
		{
			Id = value;
		}

		const void message(std::string value)
		{
			Message = value;
		}

	private:
		std::string Expected;
		std::string Actual;
		int Id;
		TestResult Result;
		double Duration;
		std::string Message;
};

#endif // !TEST_RUN_H
