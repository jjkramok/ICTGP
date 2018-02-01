#include <iostream>
#include <cmath>

using namespace std;

void bubblesort(int *array, int length);
int greatestCommonDivisor(int a, int b);
int leastCommonMultiple(int a, int b);

int main() {
    cout << "Hello, World!" << endl;
    cout << "LCM of 3 and 47: " << leastCommonMultiple(3, 47) << endl;
    return 0;
}

// Ex. 1
int sum(int a, int b) {
    return a + b;
}

// Ex. 2
void printEven(int a) {
    if (a % 2 == 0)
        cout << a << endl;
}

// Ex. 3
int power(int base, int exponent) {
    if (exponent == 0)
        return 1;
    if (exponent == 1)
        return base;
    return base + power(base, exponent - 1);
}

// Ex. 4
int isPrime(int a) {
    if (a <= 1) return 0;
    if (a % 2 == 0 && a > 2) return 0;
    for(int i = 3; i < a / 2; i+= 2)
    {
        if (a % i == 0)
            return 0;
    }
    return 1;
}

// Ex. 5
// after index 100 the function breaks for the size of the array is not known.
int getValueFromArray(int *a, int value) {
    int i = 0;
    while (*(a + i) != value) {
        i++;
        if (i > 100) return -1;
    }
    return i - 1;
}

// helper method for bubblesort
int isSortedAsc(int *array, int length) {
    for (int i = 0; i < length - 1; i++) {
        if (array[i] > array[i + 1]) return 0;
    }
    return 1;
}

// Ex. 6
void bubblesort(int *array, int length) {
    while (!isSortedAsc(array, length)) {
        for (int i = 0; i < length - 1; i++) {
            if (array[i] > array[i + 1]) {
                int temp = array[i];
                array[i] = array[i + 1];
                array[i + 1] = temp;
            }
        }
    }
}

// Ex. 7 - recursive solution
int greatestCommonDivisor(int a, int b) {
    if (b == 0)
        return a;
    else
        return greatestCommonDivisor(b, a % b);
}

// Ex. 8
int leastCommonMultiple(int a, int b) {
    return abs(a * b) / greatestCommonDivisor(a, b);
}