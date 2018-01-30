#include "Program.h"

int main() {
	Maze maze;
	maze.Create(80, 20);


	std::cout << maze.ToString();

	std::getchar();

	return 0;
}