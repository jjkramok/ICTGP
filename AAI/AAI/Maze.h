#include "DisjointSet.h"

#pragma once
class Maze
{
public:
	Maze();
	~Maze();
	void Create(int width, int height);

protected:
	long *Egdes;
};

