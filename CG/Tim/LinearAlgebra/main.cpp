#include <iostream>
#include "Vector.h"

using namespace std;

int main() {
    cout << "Hello, World!" << endl;

    Vector* l = new Vector(2, 3);
    Vector* m = new Vector(1, 2);

    Vector* n = *l + *m;
    cout << n << endl;
    n->Print();

    return 0;
}