//
// Created by tim on 9-3-18.
//

#include "BST.h"

BST::BST(int x) {
    root = new Node();
    root->info = x;
}

BST::~BST() {
    // Let the Node deconstructor handle the rest
    delete root;
}

bool BST::insert(int x) {
    Node* prev = nullptr;
    Node* curr = root;
    // Traverse the tree LRN and find the correct position for the new integer
    while (curr != nullptr) {
        if (x < curr->info) {
            prev = curr;
            curr = curr->left;
        } else if (x > curr->info) {
            prev = curr;
            curr = curr->right;
        } else {
            return false;
        }
    }

    // New position found, create the new node and insert the integer.
    if (x < prev->info) {
        prev->left = new Node();
        prev->left->info = x;
    } else if (x > prev->info) {
        prev->right = new Node();
        prev->right->info = x;
    }
    return true;
}

void BST::traverse() {
    if (root != nullptr) {
        root->traverse();
    } else {
        cout << "No root element!" << endl;
    }

}