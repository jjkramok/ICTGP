//
// Created by tim on 27-3-18.
//

#include "Hash_Table.h"

Hash_Table::Hash_Table(int size) {
    this->table_size = size;
    this->the_table = (linked_list**) calloc(size, sizeof(linked_list*));
    for (int i = 0; i < size; i++) {
        this->the_table[i] = new linked_list();
    }
}

int Hash_Table::hash_value(int value) {
    return value % this->table_size;
}

void Hash_Table::add(int value) {
    int hash = hash_value(value);
    this->the_table[hash]->add(value);
}

void Hash_Table::print() {
    for (int i = 0; i < table_size; i++) {
        cout << i << " : ";
        this->the_table[i]->print();
    }
}