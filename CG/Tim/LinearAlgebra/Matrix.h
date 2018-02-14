//
// Created by tim on 8-2-18.
//

#ifndef LINEARALGEBRA_MATRIX_H
#define LINEARALGEBRA_MATRIX_H

#include "Vector.h"
// 0 1      0 1 2   0  1  2  3
// 2 3      3 4 5   4  5  6  7
//          6 7 8   8  9  10 11
//                  12 13 14 15
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
    Vector* operator*(Vector& rhs);
    char* ToString();
private:
    float *matrix;
    int row;
    int col;
    void matrixInitializer();
    int size();
};

// Non-member function
Matrix* operator*(float lhs, Matrix& rhs);


#endif //LINEARALGEBRA_MATRIX_H
