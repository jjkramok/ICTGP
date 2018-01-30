#include "Program.h"

int main() {
	Maze maze;
	maze.Create(2, 2);


	printf(maze.ToString());

	std::getchar();

	return 0;
}