//
// Created by tim on 31-1-18.
//

#include <chrono>
#include "Program.h"

using namespace std;

int week1() {
    auto start = chrono::system_clock::now();
    Maze *maze = new Maze(10, 10);
    maze->ToString();
    auto end = chrono::system_clock::now();
    chrono::duration<double> elapsed_seconds = end - start;
    cout << "elapsed time: " << elapsed_seconds.count() << "s\n";

    cout << maze->ToString();

    free(maze);
    return 0;
}
