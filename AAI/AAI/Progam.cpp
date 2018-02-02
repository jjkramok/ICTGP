#include "Program.h"

#if UnitTest == 0 

int main() {
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

	// delete maze;
	return 0;
}

#endif