#include "pch.h"

#include "AssertArrayLengthException.h"
#include "AssertArrayException.h"
#include "AssertHelpers.h"
#include "AssertException.h"

#include <list>
#include <vector>

#ifndef ASSERT_H
#define ASSERT_H

class Assert
{
	public:
		template <typename T> static void AreEqual(T expected, T actual)
		{
			if (expected != actual)
			{
				HandleAssertFailed(expected, actual);
			}
		}

		template <typename T> static void AreEqual(std::vector<T> expected, std::vector<T> actual)
		{
			if (expected.size() != actual.size())
			{
				throw AssertArrayLengthException(expected.size(), actual.size(), toString_(expected), toString_(actual));
			}
			for (int i = 0; i < expected.size(); i++)
			{
				if (expected[i] != actual[i])
				{
					throw AssertArrayException(toString_(expected), toString_(actual), i);
				}
			}
		}

		static void AreEqual(std::string expected, std::string actual)
		{
			if (expected.length() != actual.length())
			{
				throw AssertArrayLengthException(expected.length(), actual.length(), expected, actual);
			}
			for (int i = 0; i < expected.length(); i++)
			{
				if (expected[i] != actual[i])
				{
					throw AssertArrayException(std::to_string(expected[i]), std::to_string(actual[i]), i);
				}
			}
		}

		template <typename T> static void AreEqual(std::list<T> expected, std::list<T> actual)
		{
			if (expected.size() != actual.size())
			{
				throw AssertArrayLengthException(expected.size(), actual.size(), toString_(expected), toString_(actual));
			}
			for (int i = 0; i < expected.size(); i++)
			{
				if (expected[i] != actual[i])
				{
					throw AssertArrayException(toString_(expected), toString_(actual), i);
				}
			}
		}

	private:
		Assert();

		template<typename T> static void HandleAssertFailed(T expected, T actual)
		{
			if (expected != actual)
			{
				throw AssertException(toString_(expected), toString_(actual));
			}
		}
};

#endif // !ASSERT_H

