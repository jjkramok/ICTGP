//
// Created by tim on 9-3-18.
//

#include <iostream>
#include "BST.h"

using namespace std;

int main() {
    BST* bst = new BST(7);
    bst->insert(3);
    bst->insert(2);
    bst->insert(5);
    bst->insert(13);
    bst->insert(9);
    bst->insert(17);
    bst->traverse();
    delete bst;
    bst->traverse();

}