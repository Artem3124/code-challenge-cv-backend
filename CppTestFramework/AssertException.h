#include "pch.h"

#include "AssertExceptionBase.h"

#include <iostream>
#include <string>

#ifndef ASSERT_EXCEPTION_H
#define ASSERT_EXCEPTION_H

class AssertException : public AssertExceptionBase
{
	public:
		AssertException(const std::string& expected, const std::string& actual)
			: AssertExceptionBase(expected, actual, "Expected: " + expected + "\n" + "But was: " + actual)
		{

		}
};

#endif // !ASSERT_EXCEPTION_H


