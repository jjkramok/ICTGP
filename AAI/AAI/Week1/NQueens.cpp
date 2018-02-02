#include "NQueens.h"



NQueens::NQueens(int size)
{
	boardSize = size;
}


NQueens::~NQueens()
{
}

int *NQueens::Solve() {
	if (boardSize < 4) {
		return nullptr;
	}

	int *solution = (int*)calloc(boardSize, sizeof(int));
	for (int i = 0; i < boardSize; i++) {
		int queenCount = BackTrack(solution, i, 0);
		if (queenCount == boardSize) {
			continue;
		}
	}
	return solution;
}

int NQueens::BackTrack(int *start, int attempt, int size) {
	start[size] = attempt;
	size++;
	if (PromisingSolution(start, size)) {
		if (size == boardSize) {
			return size;
		}
		for (int i = attempt + 1; i < boardSize*boardSize; i++) {
			if (BackTrack(start, i, size) == boardSize) {
				return boardSize;
			}
		}
	}
	return 0;
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
			if ((solution[i] / boardSize) + (solution[i] % boardSize) == x) {
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
