#ifndef STACK_H
#define STACK_H

template<typename T>
struct StackItem {
	T value;
	StackItem *next;
};

template<typename T>
class Stack<T>
{
public:
	Stack();
	~Stack();

	void add(T item);
	T peek();
	T pop();
private:
	StackItem<T> *tos;
};


#endif