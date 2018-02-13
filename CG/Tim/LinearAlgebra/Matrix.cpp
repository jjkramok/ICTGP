//
// Created by tim on 8-2-18.
//

#include "Matrix.h"

void Matrix::matrixInitializer() {
    row = 2;
    col = 2;
    matrix = (float*) calloc(size(), sizeof(float));
}

Matrix::Matrix() {
    matrixInitializer();
}

Matrix::Matrix(float* data) {
    matrixInitializer();

}

Matrix::Matrix(Vector v) {
    matrixInitializer();

}

Vector* Matrix::ToVector() {
    return nullptr;
}

Matrix* Matrix::Identity(Matrix m) {
    return nullptr;
}

Matrix* Matrix::operator+(Matrix& rhs) {
    return nullptr;
}

Matrix* Matrix::operator-(Matrix& rhs) {
    return nullptr;
}

Matrix* Matrix::operator*(float rhs) {
    return nullptr;
}

Matrix* operator*(float lhs, Matrix& rhs) {
    return nullptr;
}

Vector* Matrix::operator*(Vector& rhs) {
    return nullptr;
}

char* Matrix::ToString() {
    return nullptr;
}

int Matrix::size() {
    return row * col;
}
