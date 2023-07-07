#include "pch.h"

#include <vector>
#include <string>

#include "SupportedTypes.h"
#include "UnsupportedException.h"

#ifndef ASSERT_HELPERS_H
#define ASSERT_HELPERS_H

template<typename T> std::string toString_(T value);
template <typename T> std::string toString(const std::vector<T>& value);

template<typename T> std::string toString_(T value)
{
	std::string typeName = typeid(T).name();
	if (typeName == _integer)
	{
		return std::to_string(reinterpret_cast<int&>(value));
	}
	if (typeName == _float)
	{
		return std::to_string(reinterpret_cast<float&>(value));
	}
	if (typeName == _double)
	{
		return std::to_string(reinterpret_cast<double&>(value));
	}
	if (typeName == _char)
	{
		return "\"" + std::to_string(reinterpret_cast<char&>(value)) + "\"";
	}
	if (typeName == _string)
	{
		return "\"" + reinterpret_cast<std::string&>(value) + "\"";
	}
	if (typeName == _bool)
	{
		return std::to_string(reinterpret_cast<bool&>(value));
	}
	if (typeName ==  _integerVector)
	{
		return toString(reinterpret_cast<std::vector<int>&>(value));
	}
	if (typeName == _floatVector)
	{
		return toString(reinterpret_cast<std::vector<float>&>(value));
	}
	if (typeName == _doubleVector)
	{
		return toString(reinterpret_cast<std::vector<double>&>(value));
	}
	if (typeName == _charVector)
	{
		return toString(reinterpret_cast<std::vector<char>&>(value));
	}
	if (typeName == _stringVector)
	{
		return toString(reinterpret_cast<std::vector<std::string>&>(value));
	}
	if (typeName == _boolVector)
	{
		return toString(reinterpret_cast<std::vector<bool>&>(value));
	}

	throw UnsupportedException();
}

template <typename T> std::string toString(const std::vector<T>& value)
{
	std::string str = "[";
	for (int i = 0; i < value.size(); i++)
	{
		str += toString_(value[i]);
		if (value.size() - 1 == i)
		{
			str += "]";
		}
		else
		{
			str += ", ";
		}
	}

	return str;
}

#endif // !ASSERT_HELPERS_H
