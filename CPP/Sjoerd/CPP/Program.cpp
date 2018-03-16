#include <stdlib.h>
#include <iostream>
#include "operator\testoperator.h"
#include <string>
#include "file/fileio.h"
#include "templates/stack.h"
#include "testtentatmen\BinarySearchTree.h"
#include "testtentatmen\file.h"

void testFile();
void testTemplate();
void testTentamen();

int main() {

	std::cout << "hello World" << std::endl;
	testTentamen();

	std::string s;
	std::cin >> s;

	return 0;

	testoverload();

	testFile();

	testTemplate();

	
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

void testTentamen() {
	BinarySearchTree<int> bst;
	bst.insert(3);
	bst.insert(5);
	bst.insert(5);
	bst.insert(1);
	bst.traverse();

	std::cout << "file ------------------" << std::endl;

	std::cout << "total: " << readFile("sample.txt") << std::endl;
}