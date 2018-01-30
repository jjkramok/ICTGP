#include <stdlib.h>
#include <iostream>

#pragma once

class DisjointSet
{
public:
	DisjointSet();
	~DisjointSet();
	void Init(int size);
	void Union(int element1, int element2);
	int Find(int element);
	char *ToString();

protected:
	int *setArray;
	int setArraySize;
};

