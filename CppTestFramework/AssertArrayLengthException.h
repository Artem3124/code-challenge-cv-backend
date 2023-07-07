#include "pch.h"
#include "AssertExceptionBase.h"

#ifndef ASSERT_ARRAY_LENGTH_EXCEPTION_H
#define ASSERT_ARRAY_LENGTH_EXCEPTION_H

class AssertArrayLengthException : public AssertExceptionBase
{
	public:
		AssertArrayLengthException(
			const unsigned int& expectedSize,
			const unsigned int& actualSize,
			const std::string&	expected,
			const std::string&	actual)
			: AssertExceptionBase(
				std::to_string(expectedSize),
				std::to_string(actualSize),
				"Expected sequence size: " + std::to_string(expectedSize) + "\nBut was: " + std::to_string(actualSize) +
				"\nExpected: " + expected + "\nBut was: " + actual)
		{

		}
};

#endif // !ASSERT_ARRAY_LENGTH_EXCEPTION_H
