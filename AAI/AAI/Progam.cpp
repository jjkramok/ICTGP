#include "Program.h"

#ifndef _DEBUG

int main() {
	// todo: create instance pointer instead of on stack.

	Maze maze(5, 5);
	maze.Create();


	std::cout << maze.ToString();

	std::getchar();

	// todo: free maze if not on stack.

	return 0;
}

#endif // !_DEBUG