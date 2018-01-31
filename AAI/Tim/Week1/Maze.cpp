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
        // Add left and lower edge to edges if applicable
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

char* Maze::ToString() {

}