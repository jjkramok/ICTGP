#include "NQueens.h"



NQueens::NQueens(int size)
{
	boardSize = size;
	solutionsFound = 0;
}


NQueens::~NQueens()
{
}

void NQueens::Solve() {
	if (boardSize < 4) {
		return;
	}
	/*int solutionCount = 0;
	int *solution = (int*)calloc(boardSize, sizeof(int));
	for (int i = 0; i < boardSize / 2 + boardSize % 2; i++) {
		BackTrack(solution, i, 0);
	}*/
	BackTrack2();
}

void NQueens::BackTrack(int *start, int attempt, int size) {
	start[size] = attempt;
	size++;
	if (PromisingSolution(start, size)) {
		if (size == boardSize) {
			Print(start);
			return;
		}
		for (int i = attempt + 1; i < boardSize*boardSize; i++) {
			BackTrack(start, i, size);
		}
	}
}

void NQueens::BackTrack2() {
	int *solution = (int*)calloc(boardSize, sizeof(int));
	for (int i = 0; i < boardSize; i++) {
		solution[0] = i;
		BackTrack2(0, solution);
	}
	//free(solution);
}

void NQueens::BackTrack2(int row, int *solution) {
	if (row == boardSize - 1) {
		solutionsFound++;
		if (solutionsFound % 500 == 0)
			std::cout << "solution: " << solutionsFound << '\n';
		//Print(solution);
	}
	row++;
	for (int i = 0; i < boardSize; i++) {
		solution[row] = i + (row*boardSize);
		if (PromisingSolution(solution, row + 1)) {
			BackTrack2(row, solution);
		}
	}
}

void NQueens::Print(int*solution) {
	char* str = (char*)calloc(boardSize*(boardSize + 1) + 1, sizeof(char));
	for (int i = 0; i < boardSize*(boardSize + 1); i++) {
		if (i % (boardSize + 1) == boardSize) {
			str[i] = '\n';
		}
		else {
			str[i] = '.';
		}
	}
	for (int i = 0; i < boardSize; i++) {
		str[solution[i] + solution[i] / boardSize] = 'Q';
	}
	std::cout << str;
}

bool NQueens::PromisingSolution(int *solution, int solutionSize)
{
	// rows and columns
	for (int x = 0; x < boardSize; x++) {
		int queensInRow = 0;
		int queensInCol = 0;
		for (int i = 0; i < solutionSize; i++) {
			if (solution[i] % boardSize == x) {
				queensInRow++;
			}
			if (solution[i] / boardSize == x) {
				queensInCol++;
			}
		}

		if (queensInCol >= 2 || queensInRow >= 2) {
			return false;
		}
	}

	// diagonal
	for (int x = -boardSize + 2; x < boardSize - 1; x++) {
		int queenCount1 = 0;
		int queenCount2 = 0;

		for (int i = 0; i < solutionSize; i++) {
			if ((solution[i] / boardSize) + (solution[i] % boardSize) == x + boardSize - 1) {
				queenCount1++;
			}
			if ((solution[i] / boardSize) - (solution[i] % boardSize) == x) {
				queenCount2++;
			}
		}

		if (queenCount1 >= 2 || queenCount2 >= 2) {
			return false;
		}
	}

	return true;
}
