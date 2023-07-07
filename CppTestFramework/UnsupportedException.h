#include "pch.h"
#include <iostream>

#ifndef UNSUPPORTED_EXCEPTION_H
#define UNSUPPORTED_EXCEPTION_H

class UnsupportedException : public std::runtime_error
{
	public:
		UnsupportedException() : std::runtime_error("Action is not supported.")
		{

		}
};

#endif // !UNSUPPORTED_EXCEPTION_H
