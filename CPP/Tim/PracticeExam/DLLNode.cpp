//
// Created by tim on 23-3-18.
//

#include "DLLNode.h"

DLLNode::DLLNode(int value) {
    this->content = value;
    this->next = nullptr;
    this->prev = nullptr;
}

DLLNode::DLLNode() : DLLNode(0) {}