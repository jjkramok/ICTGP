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
	//RemoveEdges();
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
	int gridWidth = Width * 2 + 2;
	int gridHeight = Height * 2 + 1;

	int resultLength = gridWidth*gridHeight + 1;
	char *result = (char*)calloc(resultLength * 2, sizeof(char));


	for (int i = 0; i < resultLength; i++) {
		result[i] = ' ';
	}

	// Border
	for (int x = 0; x < gridWidth - 1; x++) {
		result[x] = '#';
		result[x + gridWidth*(gridHeight - 1)] = '#';
	}
	for (int y = 0; y < gridHeight; y++) {
		result[y * gridWidth] = '#';
		result[y * gridWidth + gridWidth - 2] = '#';
		result[y * gridWidth + gridWidth - 1] = '\n';
	}

	// Static inside edge corner.
	for (int x = 2; x < gridWidth - 2; x += 2) {
		for (int y = 2; y < gridHeight - 2; y += 2) {
			result[x + y*gridWidth] = '#';
		}
	}

	// todo draw inner edges
	for (int i = 0; i < EdgesCount; i++) {
		int location1 = GetDrawingLocation(std::get<0>(Edges[i]));
		int location2 = GetDrawingLocation(std::get<1>(Edges[i]));

		int item = (location1 + location2) / 2;
		result[item] = 'Q';
	}


	result[resultLength - 1] = '\0';
	return result;
}

int Maze::GetDrawingLocation(int element)
{
	int location = (element % Width * 2 + 1) + ((element / Height) * 2 + 1 + (Width * 2)) + 2;
	std::cout << location;
	return location;
}