#ifndef OPERATOROVERLOAD_H
#define OPERATOROVERLOAD_H

#include <iostream>
#include <string>
#include <stdlib.h>
#include <stdio.h>

class OperatorOverlaod
{
public:
	double value;

	OperatorOverlaod();
	~OperatorOverlaod();

	OperatorOverlaod operator* (const double &v);
	friend std::ostream& operator<< (std::ostream &os, const OperatorOverlaod &o);
};

#endif