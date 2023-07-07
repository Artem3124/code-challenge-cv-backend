#include "pch.h"

#include <string>
#include <stdexcept>

#ifndef ASSERT_EXCEPTION_BASE_H
#define ASSERT_EXCEPTION_BASE_H

class AssertExceptionBase : public std::runtime_error
{
	protected:
		AssertExceptionBase(const std::string& expected, const std::string& actual, const std::string& what)
			: std::runtime_error(what.c_str())
		{
			Expected = expected;
			Actual = actual;
			What = what;
		}

	public:
		const std::string expected()
		{
			return Expected;
		}

		const std::string actual()
		{
			return Actual;
		}

	private:
		std::string Expected;
		std::string Actual;
		std::string What;
};

#endif // !ASSERT_EXCEPTION_BASE_H