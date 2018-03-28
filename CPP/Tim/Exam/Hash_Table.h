//
// Created by tim on 27-3-18.
//

#ifndef CPPEXAM_HASH_TABLE_H
#define CPPEXAM_HASH_TABLE_H


#include "linked_list.h"

class Hash_Table {
public:
    linked_list** the_table;
    explicit Hash_Table(int size);
    int hash_value(int value);
    void add(int value);
    void print();

private:
    int table_size;

};


#endif //CPPEXAM_HASH_TABLE_H
