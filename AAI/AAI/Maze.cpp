#include "Maze.h"

Maze::Maze()
{

}


Maze::~Maze()
{
	free(Edges);
}

void Maze::Create(int width, int height)
{
	Width = width;
	Height = height;

	CreateEdges();
	RemoveEdges();
}

void Maze::CreateEdges() {
	EdgesCount = ((Width - 1) * Height) + ((Height - 1) * Width);
	EdgesCapacity = EdgesCount;

	Edges = (std::tuple<int, int>*)calloc(EdgesCount, sizeof(std::tuple_size<std::tuple<int, int>>::value));

	int index = 0;
	for (int i = 1; i < Width*Height; i++) {
		// Vertical edges.
		if (i % Width != 0) {
			Edges[index] = std::make_tuple(i - 1, i);
			index++;
		}

		// Horizontal edges.
		if (i >= Width) {
			Edges[index] = std::make_tuple(i - Width, i);
			index++;
		}
	}
}

void Maze::RemoveEdges() {
	DisjointSet set(EdgesCount);

	std::srand(std::time(nullptr));

	std::tuple<int, int> *fixedEdges = (std::tuple<int, int>*)calloc(EdgesCount, sizeof(std::tuple_size<std::tuple<int, int>>::value));

	int fixedEdgesIndex = 0;
	while (set.SetCount > 1) {
		int randomIndex = std::rand() % EdgesCount;

		if (!set.Union(std::get<0>(Edges[randomIndex]), std::get<1>(Edges[randomIndex]))) {
			// Union did not happen.
			// Add edge to fixed edges array.
			fixedEdges[fixedEdgesIndex] = Edges[randomIndex];
			fixedEdgesIndex++;
		}

		Edges[randomIndex] = Edges[EdgesCount - 1];
		EdgesCount--;
	}

	// Readd fixed edges.
	for (int i = 0; i < fixedEdgesIndex; i++) {
		Edges[EdgesCount] = fixedEdges[i];
		EdgesCount++;
	}

	free(fixedEdges);
	// delete &set;
}

char *Maze::ToString() {
	char *result = (char*)calloc((Width + 3)*(Height + 2) + 1, sizeof(char));
	for (int i = 0; i < (Width + 3)*(Height + 2) + 1; i++) {
		result[i] = ' ';
	}

	// Border
	for (int x = 0; x < Width + 2; x++) {
		result[x] = '#';
		result[x + (Width + 3)*(Height + 1)] = '#';
	}
	for (int y = 1; y < Height; y++) {
		result[y * (Width + 3)] = '#';
		result[y * (Width + 3) + Width + 1] = '#';
		result[y * (Width + 3) + Width + 2] = '\n';
	}

	return result;
}
