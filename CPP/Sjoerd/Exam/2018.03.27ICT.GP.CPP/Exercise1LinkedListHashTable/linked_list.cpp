#include "Linked_List.h"



Linked_List::Linked_List()
{
	head = NULL;
	tail = NULL;
}


Linked_List::~Linked_List()
{
	while (head != NULL) {
		List_Node *old_head = head;
		head = old_head->next;
		delete old_head;
	}
	head = NULL;
	tail = NULL;
}

void Linked_List::add(int value)
{
	List_Node *item = new List_Node();
	item->data = value;
	item->next = NULL;

	if (head == NULL)
	{
		head = item;
		tail = item;
	}
	else
	{
		tail->next = item;
		tail = item;
	}
}

void Linked_List::print()
{
	if (head == NULL) {
		std::cout << "-" << std::endl;
		return;
	}

	List_Node *current = head;
	bool first = true;

	while (current != NULL) {
		if (first) {
			first = false;
			std::cout << current->data;
		}
		else {
			std::cout << " -> " << current->data;
		}

		current = current->next;
	}
	std::cout << std::endl;
}

void Linked_List::remove(int value)
{
	if (head == NULL) {
		// no items in list.
		return;
	}

	if (head == tail) {
		// only one item in list.
		if (head->data == value) {
			delete head;
			head = NULL;
			tail = NULL;
		}
		return;
	}

	List_Node *current = head->next;
	List_Node *previous = head;
	while (current != NULL) 
	{
		if (current->data == value) {
			previous->next = current->next;
			delete current;
			return;
		}
		previous = current;
		current = current->next;
	}
}



