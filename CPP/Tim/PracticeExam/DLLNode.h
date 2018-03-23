//
// Created by tim on 23-3-18.
//

#ifndef TIM_DLLNODE_H
#define TIM_DLLNODE_H


class DLLNode {
public:
    DLLNode* prev;
    DLLNode* next;
    int content;

    DLLNode(int value);
    DLLNode();
};


#endif //TIM_DLLNODE_H
