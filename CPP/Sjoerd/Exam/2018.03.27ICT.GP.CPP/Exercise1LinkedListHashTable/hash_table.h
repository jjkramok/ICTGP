#ifndef HASH_TABLE_H
#define HASH_TABLE_H

#include "linked_list.h"
#include <iostream>
#include <string>

class Hash_Table
{
public:
	Hash_Table(int table_size);
	~Hash_Table();
	int hash_value(int value);
	void add(int value);
	void print();
private:
	int table_size;
	Linked_List *the_table;
};

#endif // !HASH_TABLE_H