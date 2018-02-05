//
// Created by Kramok on 5-2-2018.
//

#ifndef TIM_PERSON_H
#define TIM_PERSON_H

#include <malloc.h>
#include <cstring>
#include <cstdio>

class Person {
public:
    explicit Person(int speed, char name);
    char *ToString();
    int speed;
    char name;
};


#endif //TIM_PERSON_H
