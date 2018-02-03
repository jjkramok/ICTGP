#include <stdlib.h>
#include <iostream>

#pragma once
class NQueens
{
public:
	NQueens(int size);
	~NQueens();
	void Solve();
	int solutionsFound;
private:
	void BackTrack(int *start, int attempt, int size);
	void BackTrack2();
	void BackTrack2(int row, int *solution);
	void Print(int *solution);
	bool PromisingSolution(int *solution, int solutionSize);
	int boardSize;
};

