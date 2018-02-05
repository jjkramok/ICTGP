cmake_minimum_required(VERSION 3.6)
project(Tim)

set(CMAKE_CXX_STANDARD 11)

add_executable(Tim main.cpp catch.hpp Week1/DisjointSet.cpp Week1/DisjointSet.h Week1/Program.cpp Week1/Program.h Week1/Maze.cpp Week1/Maze.h Week1/NQueens.cpp Week1/NQueens.h)