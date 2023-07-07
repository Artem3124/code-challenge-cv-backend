#include "pch.h"

#include "Assert.h"

#include <list>

#ifndef TEST_SUITE_H
#define TEST_SUITE_H

class TestSuite
{
public:
	// TestSuite();

private:
	template <typename T> static std::list<double(*)(void* (T, T))> metrics;
	template <typename T> static std::list<void* (T, T)> asserts;
};

//template <typename T>
//void assert()(T, T);
#endif // !TEST_SUITE_H

