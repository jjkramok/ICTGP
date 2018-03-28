#ifndef BINARYSEARCHTREE_H
#define BINARYSEARCHTREE_H

#include <stdlib.h>
#include <iostream>
#include <iostream>

template <class T>
struct Node {
	T info;
	Node* left = NULL;
	Node* right = NULL;
};

template <class T>
class BinarySearchTree
{
public:
	BinarySearchTree();
	~BinarySearchTree();

	void insert(T x);
	void traverse();
	void remove(T x);

private:
	Node<T> *top;
	void traverseNode(Node<T> * node);
	void insert(T x, Node<T> * node);
	void remove(T x, Node<T> *node);
};

#include "BinarySearchTree.cc"

#endif