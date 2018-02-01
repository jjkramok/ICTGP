//
// Created by tim on 31-1-18.
//

#ifndef TIM_MAZE_H
#define TIM_MAZE_H

#include <tuple>
#include "DisjointSet.h"
#include <ctime>
#include <iostream>

class Maze {
public:
    Maze(int height, int width);
    ~Maze();
    int height;
    int width;
    std::tuple<int, int> *edges;
    int amountOfEdges;
    char *ToString();
};


#endif //TIM_MAZE_H
