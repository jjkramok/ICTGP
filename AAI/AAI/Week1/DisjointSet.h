#include <stdlib.h>
#include <iostream>

#pragma once

class DisjointSet
{
public:
	DisjointSet(int size);
	~DisjointSet();
	int Union(int element1, int element2);
	int Find2(int element);
	int Find(int element);
	char *ToString();
	int setCount;
	
	int *setArray;
	int setArraySize;
private:
	int *setArrayOptimizer;
};

