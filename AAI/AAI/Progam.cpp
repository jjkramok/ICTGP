#include "Program.h"

#if UnitTest == 0 

int main() {
	queen();
	return 0;
}

void maze() {
	system("cls");
	clock_t t1, t2;
	t1 = clock();

	Maze* maze = new Maze(80, 20);
	maze->Create();
	std::cout << maze->ToString();
	maze->Solve(50);

	t2 = clock();
	float diff = ((float)t2 - (float)t1) / CLOCKS_PER_SEC;
	std::cout << diff << std::endl;
	delete maze;
}

void queen() {
	clock_t t1, t2;
	t1 = clock();

	const int size = 15;
	NQueens *n = new NQueens(size);
	int* solution = n->Solve();
	
	t2 = clock();
	float diff = ((float)t2 - (float)t1) / CLOCKS_PER_SEC;
	std::cout << diff << std::endl;


	//nice string
	char* str = (char*)calloc(size*(size + 1) + 1, sizeof(char));
	for (int i = 0; i < size*(size + 1); i++) {
		if (i % (size + 1) == size) {
			str[i] = '\n';
		}
		else {
			str[i] = '.';
		}
	}
	for (int i = 0; i < size; i++) {
		str[solution[i] + solution[i] / size] = 'Q';
	}
	std::cout << str;
}

#endif