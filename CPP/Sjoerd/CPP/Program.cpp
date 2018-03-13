#include <stdlib.h>
#include <iostream>
#include "operator\testoperator.h"
#include <string>
#include "file/fileio.h"
#include "templates/stack.h"

void testFile();
void testTemplate();

int main() {

	std::cout << "hello World";

	testoverload();

	testFile();

	testTemplate();

	std::string s;
	std::cin >> s;

	return 0;
}


void testFile() {
	std::cout << "file ----------------------------------------------------" << std::endl;
	fileio f;
	f.write();
	f.read();
}

void testTemplate() {
	std::cout << "template ----------------------------------------------------" << std::endl;
	Stack<int> stack;
	stack.add(1);
	std::cout << stack.pop() << std::endl;


	stack.add(2);
	stack.add(3);
	std::cout << stack.pop() << std::endl;
	std::cout << stack.pop() << std::endl;
}