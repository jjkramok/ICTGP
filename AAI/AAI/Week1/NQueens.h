#include <stdlib.h>

#pragma once
class NQueens
{
public:
	NQueens(int size);
	~NQueens();
	int* Solve();
private:
	int BackTrack(int *start, int attempt, int size);
	bool PromisingSolution(int *solution, int solutionSize);
	int boardSize;
};

