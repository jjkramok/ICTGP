//
// Created by tim on 31-1-18.
//

#include <cstdlib>
#include <cstdio>
#include "DisjointSet.h"

DisjointSet::DisjointSet(int size) {
    setArraySize = size;
    setArray = (int*) calloc(size, sizeof(int));
    amountOfSets = size;

    for (int i = 0; i < size; i++) {
        setArray[i] = -1;
    }
}

DisjointSet::~DisjointSet() {
    free(this->setArray);
}

int DisjointSet::Union(int elem1, int elem2) {
    int parent1 = this->Find(elem1);
    int parent2 = this->Find(elem2);

    if (parent1 == parent2) {
        // Both elements already belong to the same set.
        return 1;
    }

    if (setArray[parent1] > setArray[parent2]) {
        // Merge smaller tree (parent2) into bigger tree (parent1).
        setArray[parent1] += setArray[parent2]; // Increase size of the bigger tree by size of the smaller tree.
        setArray[parent2] = parent1; // Merge the smaller tree by marking parent1 as parent of the smaller tree.
    } else {
        // Merge smaller tree (parent1) into bigger tree (parent2).
        setArray[parent2] += setArray[parent1]; // Increase size of the bigger tree by size of the smaller tree.
        setArray[parent1] = parent2; // Merge the smaller tree by marking parent2 as parent of the smaller tree.
    }
    amountOfSets--;
    return 0;
}

int DisjointSet::Find(int elem) {
    int parent = setArray[elem];

    if (parent < 0) {
        // Element is also parent of his own set, return itself.
        return elem;
    } else {
        int parentOfParent = Find(parent);
        setArray[elem] = parentOfParent;
        return parentOfParent;
    }
}

char *DisjointSet::ToString() {
    char *res = (char*) calloc(setArraySize * 5 + 1 , sizeof(char));

    for (int i = 0; i < setArraySize; i++) {
        sprintf(&res[i * 5], "%d", setArray[i]);
        for (int j = i * 5; j < 5 * i + 5; j++) {
            if (res[j] == '\0') {
                res[j] = ' ';
            }
        }
    }
    res[setArraySize * 5 + 1] = '\0';
    return res;
}
