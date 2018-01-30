#include <stdlib.h>
#include <iostream>

#pragma once

class DisjointSet
{
public:
	DisjointSet(int size);
	~DisjointSet();
	int Union(int element1, int element2);
	int Find(int element);
	char *ToString();
	int SetCount;

protected:
	int *setArray;
	int setArraySize;
};

