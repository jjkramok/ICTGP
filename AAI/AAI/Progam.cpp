#include "Program.h"

#if UnitTest == 0 

int main() {
	// todo: create instance pointer instead of on stack.
	system("cls");

	Maze* maze = new Maze(80, 20);
	maze->Create();


	std::cout << maze->ToString();
	maze->Solve(1);
	
	delete maze;
	return 0;
}

#endif