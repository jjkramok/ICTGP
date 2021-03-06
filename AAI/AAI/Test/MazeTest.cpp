#include "MazeTest.h"

TEST_CASE("Maze Constructor") {
	Maze maze(3, 5);

	REQUIRE(maze.Width == 3);
	REQUIRE(maze.Height == 5);
}

TEST_CASE("Maze Create EdgesCountCorrect") {
	Maze maze(2, 2);
	maze.Create();

	REQUIRE(maze.EdgesCount == 1);
	REQUIRE(maze.EdgesCapacity == 4);
}
