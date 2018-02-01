#include "DisjointSet.h"
#include <stdlib.h>
#include <tuple>
#include <ctime>
#include <Windows.h>
#include <chrono>
#include <thread>

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
	void Solve(int delay);
	bool EdgeAtLocation(int location1, int location2);
private:
	int TryGoDirection(char direction, int location);
	void CreateEdges();
	void RemoveEdges();
	int GetDrawingLocation(int element, int gridWidth);
	void PrintAtLocation(int location, char character);
};

