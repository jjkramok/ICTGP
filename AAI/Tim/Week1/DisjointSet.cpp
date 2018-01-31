//
// Created by tim on 31-1-18.
//

#include <cstdlib>
#include "DisjointSet.h"

class DisjointSets
{
    int *setArray;
    int setArraySize;
    int amountOfSets;



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

    }

    int DisjointSet::Find(int elem) {
        int parent = setArray[elem];
        if (parent == -1) {
            return elem;
        } else {
            Find(parent);
        }
    }

    char *DisjointSet::ToString() {

    }

};
