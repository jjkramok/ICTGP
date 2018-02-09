//
// Created by tim on 8-2-18.
//

#ifndef LINEARALGEBRA_VECTOR_H
#define LINEARALGEBRA_VECTOR_H

#include <cstdlib>
#include <cstdio>
#include <iostream>

using namespace std;

class Vector {
public:
    explicit Vector();
    Vector(float x, float y);
    Vector* operator+(Vector& rhs);
    float x;
    float y;
    char* ToString();
    void Print();
private:

};


#endif //LINEARALGEBRA_VECTOR_H
