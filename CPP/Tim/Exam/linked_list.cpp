//
// Created by tim on 27-3-18.
//

#include "linked_list.h"

linked_list::linked_list() {
    this->head = nullptr;
    this->tail = nullptr;
}


void linked_list::add(int value) {
    List_Node* newNode = new List_Node(value);
    // If list is empty update the primary pointers.
    if (is_empty()) {
        this->tail = newNode;
        this->head = newNode;
        return;
    }

    // Find the penultimate node.
    List_Node* curr = this->head;
    List_Node* prev = nullptr;
    while(curr != nullptr) {
        prev = curr;
        curr = curr->next;
    }

    // Add node and update tail.
    if (prev == nullptr) {
        curr->next = newNode;
    } else {
        prev->next = newNode;
    }
    this->tail = newNode;
}

void linked_list::print() {
    if (is_empty()) {
        cout << "-" << endl;
        return;
    }

    // Loop through the datastructure.
    List_Node* curr = this->head;
    while(curr->next != nullptr) {
        cout << curr->data << " -> ";
        curr = curr->next;
    }
    cout << curr->data << endl;
}

void linked_list::remove(int value) {
    if (is_empty()) {
        return;
    }

    // Find the node containing value.
    List_Node* curr = this->head;
    List_Node* prev = nullptr;
    while(curr != nullptr) {
        if (curr->data == value) {
            // Node found, remove all references
            if (prev != nullptr) {
                // Update previous if removed node is not first occurance.
                prev->next = curr->next;
            }
            if (curr->next == nullptr) {
                // Update tail if necessary.
                this->tail = prev;
            }
            if (curr == this->head && curr->next != nullptr) {
                // Update head if removed node is first occurance.
                this->head = curr->next;
            }
            if (curr == this->head && curr->next == nullptr) {
                // If this is the only element, we need to change both primary pointers.
                this->head = nullptr;
                this->tail = nullptr;
            }
            delete curr;
            return; // Only delete first occurance, we can safely return now.
        }
        prev = curr;
        curr = curr->next;
    }
    // Node with value not found.
}

// -------- private methods --------

bool linked_list::is_empty() {
    return this->head == nullptr;
}