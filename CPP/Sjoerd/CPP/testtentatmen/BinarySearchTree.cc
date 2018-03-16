#include "BinarySearchTree.h"
#ifndef BINARYSEARCHTREE_CC
#define BINARYSEARCHTREE_CC


template <class T>
BinarySearchTree<T>::BinarySearchTree()
{
	top = NULL;
}

template <class T>
BinarySearchTree<T>::~BinarySearchTree()
{
}

template <class T>
void BinarySearchTree<T>::insert(T x)
{
	if (top == NULL) {
		top = new Node<T>;
		top->info = x;
		return;
	}
	insert(x, top);
}

template <class T>
void BinarySearchTree<T>::insert(T x, Node<T> *node) {
	if (node->info == x) {
		return;
	}
	if (node->info > x) {
		if (node->left == NULL) {
			node->left = new Node<T>;
			node->left->info = x;
		}
		else 
		{
			insert(x, node->left);
		}
	}
	else {
		if (node->right == NULL) {
			node->right = new Node<T>;
			node->right->info = x;
		}
		else 
		{
			insert(x, node->right);
		}
	}
}

template <class T>
void BinarySearchTree<T>::traverse()
{
	traverseNode(top);
}

template <class T>
void BinarySearchTree<T>::remove(T x)
{
	remove(x, top);
}

template <class T>
void BinarySearchTree<T>::remove(T x, Node<T> *node) {
	if (node == NULL) {
		return;
	}

	// remove
	if (node->info == x) {

		return;
	}

	// not there.
	if (node->info > x) {
		remove(x, node->left);
	}
	else 
	{
		remove(x, node->right);
	}

}

template <class T>
void BinarySearchTree<T>::traverseNode(Node<T> * node)
{
	if (node == NULL) {
		return;
	}
	traverseNode(node->left);
	std::cout << node->info << std::endl;
	traverseNode(node->right);

}


#endif // !BINARYSEARCHTREE_CC