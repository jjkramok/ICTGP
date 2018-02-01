//
// Created by tim on 31-1-18.
//

#include "Maze.h"

Maze::Maze(int Height, int Width) {
    height = Height;
    width = Width;

    if (height < 5)
        height = 5;
    if (width < 5)
        width = 5;

    amountOfEdges = (height - 1) * width + (width - 1) * height;
    edges = (std::tuple<int, int>*) calloc(amountOfEdges, sizeof(std::tuple<int, int>));

    int topOfEdges = 0;
    for (int i = 0; i < width * height; i++) {
        // Add right and lower edge to edges if applicable
        // Check if vertical edge is inside the maze and add it to edges.
        if (i % width != width - 1) {
            edges[topOfEdges] = std::make_tuple(i, i + 1);
            topOfEdges++;
        }
        // Check if horizontal edge is inside the maze and add it to edges.
        if (i / width < height - 1) {
            edges[topOfEdges] = std::make_tuple(i, i + width);
            topOfEdges++;
        }
    }

    DisjointSet mazeState = DisjointSet(height * width);

    int seed = std::time(nullptr);
    std::srand(seed);

    std::tuple<int, int> *checkedEdges = (std::tuple<int, int>*) calloc(amountOfEdges, sizeof(std::tuple<int, int>));

    int topOfCheckEdges = 0;
    // Keep removing edges until every tile of the maze has a path to every other tile of the maze.
    while (mazeState.amountOfSets > 1) {
        int randomEdgeIndex = std::rand() % amountOfEdges;

        if (mazeState.Union(std::get<0>(edges[randomEdgeIndex]), std::get<1>(edges[randomEdgeIndex]))) {
            // No union was made, both tiles are already connected to eachother.
            checkedEdges[topOfCheckEdges] = edges[randomEdgeIndex];
            topOfCheckEdges++;
        }

        edges[randomEdgeIndex] = edges[amountOfEdges - 1];
        amountOfEdges--;
    }

    // Add all checked edges back to the maze
    for (int i = 0; i < topOfCheckEdges; i++) {
        edges[amountOfEdges] = checkedEdges[i];
        amountOfEdges++;
    }

    // delete temporary set
    free(checkedEdges);
}

Maze::~Maze() {
    free(edges);
}

char *Maze::ToString() {
    char edge = 223;
    char empty = ' ';
    char corner = 223;
    int boardWidth = width * 2 + 2; // or amount of columns
    int boardHeight = height * 2 + 1; // or amount of rows
    int boardSize = boardWidth * boardHeight + 1; // +1 for newline space

    // Initialize an empty string representation of the maze
    char *maze = (char*) malloc(boardSize * sizeof(char)); // + 1 string terminator

    for (int i = 0; i < boardSize; i++) {
        maze[i] = empty;
    }

    // Vertical borders and newline chars
    for (int x = 0; x < boardHeight; x++) {
        maze[x * boardWidth] = edge;
        maze[x * boardWidth + boardWidth - 2] = edge;
        maze[x * boardWidth + boardWidth - 1] = '\n';
    }

    // Horizontal borders
    for (int y = 1; y < boardWidth - 1; y++) {
        maze[y] = edge;
        maze[y + boardWidth * (boardHeight - 1)] = edge;
    }

    // In-maze corners
    for (int x = 2; x < boardWidth - 2; x += 2) {
        for (int y = 2; y < boardHeight - 2; y += 2) {
            maze[x + y * boardWidth] = corner;
        }
    }

    // Draw non-erased edges
    for (int i = 0; i < amountOfEdges; i++) {
        int tile1 = GetDrawingLocation(std::get<0>(edges[i]), boardWidth);
        int tile2 = GetDrawingLocation(std::get<1>(edges[i]), boardWidth);
        maze[(tile1 + tile2) / 2] = edge;
    }

    maze[1] = empty; // Entrance / Exit
    maze[boardSize - 4] = empty; // Entrance / Exit
    maze[boardSize - 1] = '\0'; // Should already be null, but just in case
    return maze;
}

int Maze::GetDrawingLocation(int element, int boardWidth) {
    int heightOffset = ((element / width) * 2 + 1) * boardWidth;
    int widthOffset = ((element % width) * 2 + 1);

    return heightOffset + widthOffset;
}