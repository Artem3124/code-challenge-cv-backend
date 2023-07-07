#include "pch.h"

#include "TestRun.h"
#include "AssertHelpers.h"
#include "Core.h"

#include <list>
#include <fstream>
#include <sstream>

#ifndef TEST_RESULT_WRITER_H
#define TEST_RESULT_WRITER_H

class TestResultWriter
{
	public:
		void WriteAsJson(std::vector<TestRun> results, std::string fileName)
		{
			std::ofstream file;
			try
			{
				file.open(fileName.c_str());

				file << "[\n";
				for (int i = 0; i < results.size(); i++)
				{
					bool last = (i == results.size() - 1);
					file << TestRunAsJson(results[i]) << (last ? "\n" : ",\n");
				}
				file << "]\n";
			}
			catch (std::exception ex)
			{
				std::cout << "Unexpected error while saving test runs." << std::endl;
				std::cout << ex.what() << std::endl;
			}
			file.close();
		}

	private:
		std::string TestRunAsJson(TestRun run)
		{
			return "{\n" +
				asJsonProperty(asJsonPropertyName(Nameof(run.expected)), wrapString(run.expected())) +
				asJsonProperty(asJsonPropertyName(Nameof(run.actual)), wrapString(run.actual())) +
				asJsonProperty(asJsonPropertyName(Nameof(run.id)), std::to_string(run.id())) +
				asJsonProperty(asJsonPropertyName(Nameof(run.duration)), std::to_string(run.duration())) +
				asJsonProperty(asJsonPropertyName(Nameof(run.message)), wrapString(run.message())) +
				asJsonProperty(asJsonPropertyName(Nameof(run.result)), testResultToString(run.result()), true) +
					"\n}";
		}

		std::string testResultToString(TestResult runResult)
		{
			std::string value = EmptyString;
			switch (runResult)
			{
			case TestResult::Succeeded:
				value = "Succeeded";
				break;
			case TestResult::Failed:
				value = "Failed";
				break;
			}
			return wrapString(value);
		}

		std::string asJsonProperty(std::string nameof, std::string value, bool last = false)
		{
			return nameof + ": " + value + (last ? EmptyString : ",\n");
		}

		std::string wrapString(std::string value)
		{
			return "\"" + value + "\"";
		}

		std::string asJsonPropertyName(std::string value)
		{
			std::string substring;
			std::list<std::string> substrings;
			std::stringstream stream(value);
			while (std::getline(stream, substring, '.'))
			{
				substrings.push_back(substring);
			}

			return wrapString(substrings.back());
		}
};

#endif // !TEST_RESULT_WRITER_H
