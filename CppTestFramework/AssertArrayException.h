#include "pch.h"
#include "AssertExceptionBase.h"

#include <string>

#ifndef ASSERT_ARRAY_EXCEPTION
#define ASSERT_ARRAY_EXCEPTION

class AssertArrayException : public AssertExceptionBase
{
	public:
		AssertArrayException(const std::string& expected, const std::string& actual, const unsigned int& index)
			: AssertExceptionBase(
				expected,
				actual,
				"Sequence differs at index " + std::to_string(index) + "\n" + "Expected: " + expected + "\n" + "But was: " + actual)
		{
			Index = index;
		}

		unsigned int index()
		{
			return Index;
		}

	private:
		unsigned int Index;
};

#endif // !ASSERT_ARRAY_EXCEPTION
