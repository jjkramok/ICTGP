#include "OperatorOverlaod.h"



OperatorOverlaod::OperatorOverlaod()
{
}


OperatorOverlaod::~OperatorOverlaod()
{
}

OperatorOverlaod OperatorOverlaod::operator*(const double & v)
{
	OperatorOverlaod result;
	result.value = value * v;
	return result;
}

std::ostream & operator<<(std::ostream& os, const OperatorOverlaod & o)
{
	os << o.value;
	return os;
}
