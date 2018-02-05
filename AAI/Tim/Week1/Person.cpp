//
// Created by Kramok on 5-2-2018.
//

#include "Person.h"

Person::Person(int speed, char name) {
    this->speed = speed;
    this->name = name;
}

char* Person::ToString() {
    char *res = (char*) calloc(8, sizeof(char)); // assumes speed will never be bigger than two characters
    sprintf(res, "<%c, %d>", name, speed);
    return res;
}