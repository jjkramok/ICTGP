#include "Program.h"

int main() {
	DisjointSet set;
	set.Init(5);
	set.Union(1, 2);
	set.Union(2, 3);
	set.Union(1, 3);

	printf(set.ToString());

	std::getchar();

	return 0;
}