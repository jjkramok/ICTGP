#ifndef LINKED_LIST_H
#define LINKED_LIST_H

#include <iostream>
#include <string>

struct List_Node {
	int data;
	List_Node *next = NULL;
};


class Linked_List
{
public:
	Linked_List();
	~Linked_List();

	void add(int value);
	void print();
	void remove(int value);

private:
	List_Node *head;
	List_Node *tail;
};



#endif // !LINKED_LIST_H
