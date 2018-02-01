#include "Program.h"

#if UnitTest == 0 

int main() {
	system("cls");

	Maze* maze = new Maze(80, 20);
	maze->Create();


	std::cout << maze->ToString();
	maze->Solve(10);
	
	delete maze;
	return 0;
}

#endif