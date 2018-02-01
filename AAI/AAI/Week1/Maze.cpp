#include "Maze.h"

Maze::Maze(int width, int height)
{
	Width = width;
	Height = height;
}

Maze::~Maze()
{
	free(Edges);
}

void Maze::Create()
{
	CreateEdges();
	RemoveEdges();
}

void Maze::CreateEdges() {
	EdgesCount = ((Width - 1) * Height) + ((Height - 1) * Width);
	EdgesCapacity = EdgesCount;

	Edges = (std::tuple<int, int>*)calloc(EdgesCount, sizeof(int) * 2);

	int index = 0;
	for (int i = 0; i < Width*Height; i++) {
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
	DisjointSet* set = new DisjointSet(Width * Height);

	std::srand(std::time(nullptr));

	std::tuple<int, int> *fixedEdges = (std::tuple<int, int>*)calloc(EdgesCount, sizeof(int) * 2);

	int fixedEdgesIndex = 0;
	while (set->setCount > 1) {
		int randomIndex = std::rand() % EdgesCount;

		if (set->Union(std::get<0>(Edges[randomIndex]), std::get<1>(Edges[randomIndex]))) {
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

	// todo: realloc the edges array to reduce size.
	// todo: check if disjointset needs to be freed here.

	free(fixedEdges);
	delete set;
}

char *Maze::ToString() {
	char outerWall = '1';
	char staticInnerwall = '2';
	char edgeWall = '3';
	char empty = ' ';
	char entrance = '4';
	char exit = '5';


	int gridWidth = Width * 2 + 2;
	int gridHeight = Height * 2 + 1;

	int resultLength = gridWidth*gridHeight + 1;
	char *result = (char*)calloc(resultLength, sizeof(char));


	for (int i = 0; i < resultLength; i++) {
		result[i] = empty;
	}

	// Border
	for (int x = 0; x < gridWidth - 1; x++) {
		result[x] = outerWall;
		result[x + gridWidth*(gridHeight - 1)] = outerWall;
	}
	for (int y = 0; y < gridHeight; y++) {
		result[y * gridWidth] = outerWall;
		result[y * gridWidth + gridWidth - 2] = outerWall;
		result[y * gridWidth + gridWidth - 1] = '\n';
	}

	// Static inside edge corner.
	for (int x = 2; x < gridWidth - 2; x += 2) {
		for (int y = 2; y < gridHeight - 2; y += 2) {
			result[x + y*gridWidth] = staticInnerwall;
		}
	}

	// Draw inner edges.
	for (int i = 0; i < EdgesCount; i++) {
		int location1 = GetDrawingLocation(std::get<0>(Edges[i]), gridWidth);
		int location2 = GetDrawingLocation(std::get<1>(Edges[i]), gridWidth);

		int item = (location1 + location2) / 2;
		result[item] = edgeWall;
	}

	result[1] = entrance;
	result[resultLength - 4] = exit;
	result[resultLength - 1] = '\0';
	return result;
}

void Maze::Solve(int delay)
{
	const char up = 1;
	const char right = 2;
	const char left = 0;
	const char down = 3;

	int location = 0;
	int previousLocation = 0;
	char direction = right;
	while (location != Height * Width - 1) {
		PrintAtLocation(previousLocation, ' ');
		previousLocation = location;

		direction--;
		if (direction < 0) {
			direction = 3;
		}

		while (true) {
			location = TryGoDirection(direction, location);
			if (location != previousLocation) {
				break;
			}
			direction++;
			if (direction > 3) {
				direction = 0;
			}
		}

		std::this_thread::sleep_for(std::chrono::milliseconds(delay));
		PrintAtLocation(location, 'X');
	}
	PrintAtLocation(previousLocation, ' ');
}

int Maze::TryGoDirection(char direction, int location) {
	const char up = 1;
	const char right = 2;
	const char left = 0;
	const char down = 3;

	switch (direction)
	{
	case up:

		if (location - Width >= 0 && !EdgeAtLocation(location, location - Width)) {
			location -= Width;
		}
		break;
	case right:
		if (location % Width != Width - 1 && !EdgeAtLocation(location, location + 1)) {
			location++;
		}
		break;
	case left:
		if (location != 0 && !EdgeAtLocation(location, location - 1)) {
			location--;
		}
		break;
	case down:
		if (location + Width < Height*Width && !EdgeAtLocation(location, location + Width)) {
			location += Width;
		}
		break;
	}
	return location;
}

bool Maze::EdgeAtLocation(int location1, int location2) {
	for (int i = 0; i < EdgesCount; i++) {
		if (std::get<0>(Edges[i]) == location1 && std::get<1>(Edges[i]) == location2 ||
			std::get<0>(Edges[i]) == location2 && std::get<1>(Edges[i]) == location1) {
			return true;
		}
	}
	return false;
}

int Maze::GetDrawingLocation(int element, int gridWidth)
{
	int heightOffset = ((element / Width) * 2 + 1) * gridWidth;
	int widthOffset = ((element % Width) * 2 + 1);

	return heightOffset + widthOffset;
}

void Maze::PrintAtLocation(int location, char character)
{
	int x = (location % Width) * 2 + 1;
	int y = (location / Width) * 2 + 1;

	COORD coord;
	coord.X = x;
	coord.Y = y;
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);

	std::cout << character;

	coord.X = 0;
	coord.Y = Height * 2 + 1;
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);
}
