#ifndef STACK_H
#define STACK_H

template<class T>
struct StackItem {
	T value;
	StackItem *next;
};

template<class T>
class Stack
{
public:
	Stack();
	~Stack();

	void add(T item);
	T peek();
	T pop();
private:
	StackItem<T> *tos;
	void deletestack();
};


template<class T>
Stack<T>::Stack()
{
	tos = NULL;
}


template<class T>
Stack<T>::~Stack()
{
	deletestack();
}

template<class T>
void Stack<T>::add(T item)
{
	StackItem<T> *newItem = new StackItem<T>;
	newItem->value = item;
	newItem->next = (StackItem<T>*)tos;

	tos = newItem;
}

template<class T>
T Stack<T>::peek()
{
	if (tos == NULL)
		return NULL;

	return tos->value;
}

template<class T>
T Stack<T>::pop()
{
	if (tos == NULL)
		return NULL;

	StackItem<T> *result = tos;
	tos = tos->next;

	T value = result->value;
	delete result;

	return value;
}


template<class T>
void Stack<T>::deletestack()
{
	while (pop() != NULL);
}

#endif