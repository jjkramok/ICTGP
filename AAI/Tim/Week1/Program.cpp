//
// Created by tim on 31-1-18.
//

#include <chrono>
#include "Program.h"

using namespace std;

void maze() {
    cout << ":MAZE:" << endl;
    auto start = chrono::system_clock::now();
    Maze *maze = new Maze(50, 50);
    maze->ToString();
    auto end = chrono::system_clock::now();
    chrono::duration<double> elapsed_seconds = end - start;
    cout << "elapsed time: " << elapsed_seconds.count() << "s\n";
    cout << maze->ToString();

    cout << ":END MAZE:" << endl;
    free(maze);
}

void nqueens() {
    cout << ":NQUEENS:" << endl;
    NQueens *nQueens = new NQueens(5);
    cout << nQueens->ToString() << endl;
    cout << ":END NQUEENS:" << endl;
}

int week1() {
    cout << ":WEEK1:" << endl;
    maze();
    nqueens();
    cout << ":END WEEK1:" << endl;
    return 0;
}
