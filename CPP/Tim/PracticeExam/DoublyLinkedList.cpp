//
// Created by tim on 23-3-18.
//

#include "DoublyLinkedList.h"

DoublyLinkedList::DoublyLinkedList() {
    this->head = nullptr;
    this->tail = nullptr;
    size = 0;
}

void DoublyLinkedList::Append(DLLNode* node) {
    // Empty DLL, new node should become both tail and head.
    if (this->tail == nullptr && this->head == nullptr) {
        this->tail = node;
        this->head = node;
        node->prev = nullptr;
        node->next = nullptr;
        size++;
        return;
    }

    DLLNode* prevTail = this->tail;
    this->tail = node;
    prevTail->prev = this->tail;
    this->tail->next = prevTail;
}

void DoublyLinkedList::Push(DLLNode* node) {
    // Empty DLL, new node should become both tail and head.
    if (this->tail == nullptr && this->head == nullptr) {
        this->tail = node;
        this->head = node;
        node->prev = nullptr;
        node->next = nullptr;
        size++;
        return;
    }

    DLLNode* prevTail = this->tail;
    this->tail = node;
    prevTail->prev = this->tail;
    this->tail->next = prevTail;
}

//void DoublyLinkedList::Insert(DLLNode node, int offsetFromHead);
//int DoublyLinkedList::Size();
//char* DoublyLinkedList::ToString();
