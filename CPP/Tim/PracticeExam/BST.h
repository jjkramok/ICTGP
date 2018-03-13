//
// Created by tim on 9-3-18.
//

#ifndef TIM_BST_H
#define TIM_BST_H

#include <iostream>

using namespace std;

// Node.info -1 is used as null value for a node.

struct Node {
    int info;
    Node* left;
    Node* right;
    ~Node() {
        // Post-order traversal
        delete left;
        delete right;
        //self
    }
    void traverse() {
        // LNR traversal to achieve printing the tree from smallest to biggest info.
        if (left != nullptr) {
            left->traverse();
        }
        cout << info << " " << endl;
        if (right != nullptr) {
            right->traverse();
        }
    }

};

class BST {
public:
    //Node* root; // only here for debug
    BST(int x);
    ~BST();
    bool insert(int x);
    void traverse();

private:
    Node* root;
};


#endif //TIM_BST_H
