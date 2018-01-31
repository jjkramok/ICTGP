#include "Program.h"

int main() {
	Maze maze;
	maze.Create(5, 5);


	std::cout << maze.ToString();

	std::getchar();

	return 0;
}