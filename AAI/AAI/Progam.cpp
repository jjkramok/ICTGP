#include "Program.h"

int main() {
	Maze maze;
	maze.Create(2, 2);


	std::cout << maze.ToString();

	//std::getchar();

	return 0;
}