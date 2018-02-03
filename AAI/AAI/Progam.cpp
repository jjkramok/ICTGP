#define CATCH_CONFIG_RUNNER

#include "Program.h"

#define WRONGINPUT "use args: \nq\tnqueens\nm\tmaze\nu\tUnittest\n"

int main(int argc, char **argv) {
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
	case 'u':
		return Catch::Session().run();
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

	Maze* maze = new Maze(20, 20);
	maze->Create();
	

	std::cout << maze->ToString();
	maze->Solve(0);

	t2 = clock();
	
	float diff = ((float)t2 - (float)t1) / CLOCKS_PER_SEC;
	std::cout << diff << std::endl;
	delete maze;
}

void queen() {
	clock_t t1, t2;
	t1 = clock();

	const int size = 10;
	NQueens *n = new NQueens(size);
	n->Solve();

	t2 = clock();
	double diff = ((double)t2 - (double)t1) / CLOCKS_PER_SEC;
	std::cout << "nqueens: " << size << 'x' << size << '\n';
	std::cout << "time: " << diff << "s \n";
	std::cout << "solutions: " << n->solutionsFound << '\n';
	delete n;
}
