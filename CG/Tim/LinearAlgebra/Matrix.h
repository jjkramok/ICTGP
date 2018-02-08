//
// Created by tim on 8-2-18.
//

#ifndef LINEARALGEBRA_MATRIX_H
#define LINEARALGEBRA_MATRIX_H

#include "Vector.h"

class Matrix {
public:
    explicit Matrix();
    Matrix(float* data);
    Matrix(Vector v);

    Vector* ToVector();
    Matrix* Identity(Matrix m);
    Matrix* operator+(Matrix& rhs);
    Matrix* operator-(Matrix& rhs);
    Matrix* operator*(float rhs);
    Matrix* operator*(float lhs, Matrix& rhs);
    Vector* operator*(Vector& rhs);
    char* ToString();
private:
    float *matrix;
};


#endif //LINEARALGEBRA_MATRIX_H
