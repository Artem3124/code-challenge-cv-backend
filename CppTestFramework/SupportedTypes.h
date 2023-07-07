#include "pch.h"

#include <string>
#include <typeinfo>
#include <vector>

#ifndef SUPPORTED_TYPES_H
#define SUPPORTED_TYPES_H

const std::string _integer = typeid(int).name();
const std::string _float = typeid(float).name();
const std::string _double = typeid(double).name();
const std::string _char = typeid(char).name();
const std::string _string = typeid(std::string).name();
const std::string _bool = typeid(bool).name();
const std::string _integerVector = typeid(std::vector<int>).name();
const std::string _floatVector = typeid(std::vector<float>).name();
const std::string _doubleVector = typeid(std::vector<double>).name();
const std::string _charVector = typeid(std::vector<char>).name();
const std::string _stringVector = typeid(std::vector<std::string>).name();
const std::string _boolVector = typeid(std::vector<bool>).name();

#endif // !SUPPORTED_TYPES_H
