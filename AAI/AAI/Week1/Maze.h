#include "DisjointSet.h"
#include <stdlib.h>
#include <tuple>
#include <ctime>

#pragma once
class Maze
{
public:
	Maze(int width, int height);
	~Maze();
	void Create();

	int Width;
	int Height;
	std::tuple<int, int> *Edges;
	int EdgesCount;
	int EdgesCapacity;
	char *ToString();
private:
	void CreateEdges();
	void RemoveEdges();
	int GetDrawingLocation(int element, int gridWidth);
};

