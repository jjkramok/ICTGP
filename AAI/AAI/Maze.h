#include "DisjointSet.h"
#include <stdlib.h>
#include <tuple>
#include <ctime>

#pragma once
class Maze
{
public:
	Maze();
	~Maze();
	void Create(int width, int height);

	int Width;
	int Height;
	std::tuple<int, int> *Edges;
	int EdgesCount;
	int EdgesCapacity;
	char *ToString();
private:
	void CreateEdges();
	void RemoveEdges();
};

