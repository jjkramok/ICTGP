//
// Created by tim on 6-2-18.
//

#include "Program.h"

// Call by reference so we can change/update the value of *p to the memory address given by the malloc function
void Ex2(int* &p, int n) {
    p = (int*) malloc(sizeof(int) * n); // 10 ints long
    for (int i = 1; i < n + 1; i++) { // from 1 till 11 (10 long)
        *(p + i - 1) = i;
    }
}

int main() {
    cout << "Ex. 1" << endl;
    int *p, *q, *r;
    p = (int*) calloc(1, sizeof(int));
    q = (int*) calloc(1, sizeof(int));
    *p = 2;
    *q = 3;
    cout << "p=" << *p << " q=" << *q << endl;

    r = q;
    q = p;
    p = r;
    cout << "p=" << *p << " q=" << *q << endl;

    free(p);
    free(q);
    cout << "End Ex. 1" << endl;

    cout << "Ex. 2" << endl;
    int* array;
    int size = 10;
    Ex2(array, size);
    for (int i = 0; i < size; i++) {
        cout << array[i] << " ";
    }
    cout << endl;
    cout << "End Ex. 2" << endl;
}