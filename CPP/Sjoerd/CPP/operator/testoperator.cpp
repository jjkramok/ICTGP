#include "testoperator.h"

void testoverload() {
	OperatorOverlaod x;
	x.value = 3;
	OperatorOverlaod y;
	y = x * 3;

	std::cout << "x(3):" << x << std::endl;
	std::cout << "y(x*3):" << y << std::endl;


}