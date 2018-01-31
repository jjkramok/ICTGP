//
// Created by tim on 31-1-18.
//

#ifndef TIM_DISJOINTSET_H
#define TIM_DISJOINTSET_H

/**
 * A Simple Disjoint set implemented using an up-tree using an array.
 */
class DisjointSet {
public:
    int amountOfSets;

    /**
     * Generates an identity disjointset of given size.
     * @param size
     */
    DisjointSet(int size);
    ~DisjointSet();

    /**
     * Merges two disjoint sets
     * @param elem1
     * @param elem2
     * @return status (0 success) (1 same parent)
     */
    int Union(int elem1, int elem2);

    /**
     * Finds the root node of the given node identifier.
     * @param elem node identifier of which the root is requested to be found.
     * @return root identifier
     */
    int Find(int elem);

    /**
     * Builds a string representation of this disjoint set
     * @return string representation of the disjoint set
     */
    char *ToString();

protected:
    int *setArray;
    int setArraySize;
};


#endif //TIM_DISJOINTSET_H
