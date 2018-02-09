//
// Created by tim on 8-2-18.
//

#include "Vector.h"

Vector::Vector() {

}

Vector::Vector(float x, float y) {
    this->x = x;
    this->y = y;
}

Vector* Vector::operator+(Vector& rhs) {
    return new Vector(x + rhs.x, y + rhs.y);
}

char* Vector::ToString() {
    char* res = (char*) malloc(sizeof(char) * 24);
    sprintf(res, "(%3f %3f)", x, y);
    return res;
}

void Vector::Print() {
    cout << "(" << x << ")\n" << "(" << y << ")" << endl;
}