#include "hash_table.h"



Hash_Table::Hash_Table(int table_size)
{
	this->table_size = table_size;
	the_table = new Linked_List[table_size];
}

Hash_Table::~Hash_Table()
{
	delete[] the_table;
}	

int Hash_Table::hash_value(int value)
{
	return value % table_size;
}

void Hash_Table::add(int value)
{
	int hash = hash_value(value);
	the_table[hash].add(value);
}

void Hash_Table::print()
{
	for (int i = 0; i < table_size; i++) {
		std::cout << i << " : ";
		the_table[i].print();
	}
}
