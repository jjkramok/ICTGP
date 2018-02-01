//
// Created by tim on 31-1-18.
//

#include "Maze.h"

Maze::Maze(int height, int width) {
    if (height < 5)
        height = 5;
    if (width < 5)
        width = 5;
    this->height = height;
    this->width = width;
    this->amountOfEdges = (this->height - 1) * this->width + (this->width - 1) * this->height;
    this->edges = (std::tuple<int, int>*) calloc(amountOfEdges, sizeof(std::tuple<int, int>));

    int topOfEdges = 0;
    // loop over Maze
    for (int i = 0; i < this->width * this->height; i++) {
        // Add right and lower edge to edges if applicable
        // Check if vertical edge is inside the maze and add it to edges.
        if (i % width != width - 1) {
            edges[topOfEdges] = std::make_tuple(i, i + 1);
            topOfEdges++;
        }
        // Check if horizontal edge is inside the maze and add it to edges.
        if (i / height != 4) {
            edges[topOfEdges] = std::make_tuple(i, i + 5);
            topOfEdges++;
        }
    }

    DisjointSet mazeState = DisjointSet(height * width);

    std::srand(std::time(nullptr));
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

    free(checkedEdges);
}

Maze::~Maze() {
    free(this->edges);
}

char *Maze::ToString() {
    char edge = '1';
    char empty = ' ';
    int boardWidth = (width * 2 + 2); // or amount of columns
    int boardHeight = (height * 2 + 1); // or amount of rows
    int boardSize = boardWidth * boardHeight; // +1 for newline space

    // Initialize an empty string representation of the maze
    char* maze = (char*) malloc((boardSize + 1) * sizeof(char)); // + 1 string terminator

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

    // TODO draw inside edges and draw entrance/exit
    for (int x = 0; x < boardWidth - 1; x++) {
        for (int y = 0; y < boardHeight; y++) {
            // do stuff
        }
    }

    return maze;
}