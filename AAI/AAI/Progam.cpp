#include "Program.h"

#if UnitTest == 0 

#define WRONGINPUT "use args: \nq\tnqueens\nm\tmaze"

int main(int argc, char **argv) {

	std::cout << argv[1];

	if (argc <= 1) {
		std::cout << WRONGINPUT;
		return 0;
	}

	switch (argv[1][0])
	{
	case 'q':
		queen();
		break;
	case 'm':
		maze();
		break;
	default:
		std::cout << WRONGINPUT;
		break;
	}
	return 0;
}

void maze() {
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
	delete maze;
}

void queen() {
	clock_t t1, t2;
	t1 = clock();

	const int size = 12;
	NQueens *n = new NQueens(size);
	n->Solve();

	t2 = clock();
	float diff = ((float)t2 - (float)t1) / CLOCKS_PER_SEC;
	std::cout << "time: " << diff << std::endl;
	std::cout << "total:" << n->solutionsFound << std::endl;

}

#endif