//
// Created by tim on 23-3-18.
//

#include "BigInt.h"

BigInt::BigInt() {
    this->head = nullptr;
    this->tail = nullptr;
}

BigInt::~BigInt() {
    // TODO
}

void BigInt::print() {
    if (this->head == nullptr) {
        cout << "BigInt is empty." << endl;
        return;
    }

    Digit* curr = this->head;
    while(curr != nullptr) {
        cout << curr->digit << " <- ";
        curr = curr->next;
    }
    cout << endl;

    curr = this->head;
    while(curr != nullptr) {
        cout << curr->digit;
        curr = curr->next;
    }
    cout << endl;
}

void BigInt::read_number(istream &stream) {
    int in = stream.get();
    Digit* curr = new Digit(in);
    Digit* prev = nullptr;
    this->head = curr;

    while (!stream.eof()) {
        break;
    }
}

bool BigInt::is_empty() {
    return this->head == nullptr;
}

void BigInt::add_to_front(int digit) {
    Digit* node = new Digit(digit);

    // Empty DLL, new node should become both tail and head.
    if (this->tail == nullptr && this->head == nullptr) {
        this->tail = node;
        this->head = node;
        node->prev = nullptr;
        node->next = nullptr;
        return;
    }

    Digit* prevTail = this->tail;
    this->tail = node;
    prevTail->prev = this->tail;
    this->tail->next = prevTail;
}

void BigInt::add_to_rear(int digit) {
    Digit* node = new Digit(digit);

    // Empty DLL, new node should become both tail and head.
    if (this->tail == nullptr && this->head == nullptr) {
        this->tail = node;
        this->head = node;
        node->prev = nullptr;
        node->next = nullptr;
        return;
    }

    Digit* prevHead = this->head;
    this->head = node;
    prevHead->next = this->head;
    this->head->prev = prevHead;
}

void BigInt::copy(BigInt& original) {
    this->clear();

    if (original.is_empty()) {
        this->head = nullptr;
        this->tail = nullptr;
        return;
    }

    Digit* orgCurr = original.head;
    Digit* orgPrev = nullptr;
    Digit* newCurr = new Digit(orgCurr->digit);
    Digit* newPrev = nullptr;

    this->head = newCurr;

    while (orgCurr != nullptr) {

        newPrev =
        orgPrev = orgCurr;
        orgCurr = orgCurr->prev;
    }


}

