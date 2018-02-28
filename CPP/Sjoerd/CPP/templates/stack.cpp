#include "stack.h"

template<typename T>
Stack::Stack()
{
	tos = NULL;
}


template<typename T>
Stack::~Stack()
{
}

template<typename T>
void Stack<T>::add(T item)
{
	StackItem* newItem = new StackItem();
	newItem->value = item;
	newItem->next = tos;
}

template<typename T>
T Stack<T>::peek()
{
	if (tos == NULL)
		return NULL;

	return tos->value;
}

template<typename T>
T Stack<T>::pop()
{
	if (tos == NULL)
		return NULL;

	StackItem result = tos;
	tos = tos->next;

	return result;
}


template<typename T>
void Stack<T>::deletestack()
{
	while (pop() != NULL);
}