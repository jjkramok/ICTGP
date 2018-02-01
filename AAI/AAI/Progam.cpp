#include "Program.h"

#if UnitTest == 0 

int main() {
	system("cls");
	clock_t t1, t2;
	t1 = clock();

	Maze maze(1000,1000);
	maze.Create();
	// maze->ToString();

	t2 = clock();
	float diff = ((float)t2 - (float)t1) / CLOCKS_PER_SEC;
	std::cout << diff << std::endl;
	//maze->Solve(0);

	// delete maze;
	return 0;
}

#endif