//
// Created by tim on 23-3-18.
//

#ifndef TIM_DOUBLYLINKEDLIST_H
#define TIM_DOUBLYLINKEDLIST_H

#include "DLLNode.h"

class DoublyLinkedList {
public:
    DLLNode* head;
    DLLNode* tail;
    DoublyLinkedList();
    void Append(DLLNode* node);
    void Push(DLLNode* node);
    void Insert(DLLNode* node, int offsetFromHead);
    int Size();
    char* ToString();

private:
    int size;

};


#endif //TIM_DOUBLYLINKEDLIST_H
