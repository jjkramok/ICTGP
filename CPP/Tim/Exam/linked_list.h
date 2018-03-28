//
// Created by tim on 27-3-18.
//

#ifndef CPPEXAM_LINKED_LIST_H
#define CPPEXAM_LINKED_LIST_H

#include <iostream>

using namespace std;

struct List_Node {
    int data;
    List_Node *next;
    explicit List_Node(int value) {
        next = nullptr;
        data = value;
    }
};

class linked_list {
public:
    linked_list();
    void add(int value);
    void print();
    void remove(int value);

private:
    List_Node *head;
    List_Node *tail;
    bool is_empty();
};


#endif //CPPEXAM_LINKED_LIST_H
