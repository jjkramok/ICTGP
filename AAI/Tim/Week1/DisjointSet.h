//
// Created by tim on 31-1-18.
//

#ifndef TIM_DISJOINTSET_H
#define TIM_DISJOINTSET_H


class DisjointSet {
public:
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
     * @return success status (0 success)
     */
    int Union(int elem1, int elem2);
    int Find(int elem);
    char *ToString();

protected:
    int *setArray;
    int setArraySize;
    int amountOfSets;
};


#endif //TIM_DISJOINTSET_H
