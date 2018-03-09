//
// Created by tim on 9-3-18.
//

#ifndef TIM_BST_H
#define TIM_BST_H

struct Node {
    int info;
    Node* left;
    Node* right;
};

class BST {
public:
    Node* root;
    BST();
    ~BST();
};


#endif //TIM_BST_H
