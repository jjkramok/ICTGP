//
// Created by tim on 2-2-18.
//

#include "NQueens.h"

NQueens::NQueens(int n) {
    queens = (bool**) calloc(n, sizeof(bool*));
    for (int i = 0; i < n; i++) {
        queens[i] = (bool*) calloc(n, sizeof(bool));
    }
    this->n = n;
}

NQueens::~NQueens() {
    free(this->queens);
}

void NQueens::SolveBacktracking() {
    SolveBacktracking(0);
}

void NQueens::SolveBacktracking(int row) {
    // TODO implement
}

/********** Helper methods **********/

int NQueens::CountQueens() {
    int amountOfQueens = 0;
    for (int x = 0; x < n; x++) {
        for (int y = 0; y < n; y++) {
            if (queens[x][y])
                amountOfQueens++;
        }
    }
    return amountOfQueens;
}

bool NQueens::CheckBoard() {
    //TODO
}

char *NQueens::ToString() {
    char queen = 'Q';
    char empty = '-';

    // make temporary (in-between) board matrix
    char** board = (char**) calloc(n, sizeof(char*));
    for (int i = 0; i < n; i++) {
        board[i] = (char*) calloc(n, sizeof(char));
    }

    // store true string representation of the board
    int resLenght = (n + 1) * n;
    char *res = (char*) calloc(resLenght, sizeof(char));

    for (int x = 0; x < n; x++) {
        for (int y = 0; y < n; y++) {
            if (queens[x][y])
                board[x][y] = queen;
            else
                board[x][y] = empty;
        }
    }

    for (int y = 0; y < n; y++) {
        for (int x = 0; x < n; x++) {
            res[(y * n) + x] = board[x][y];
            //std::cout << "adding :" << board[x][y] << ":" << std::endl;
        }
        res[y + n - 1] = '\n';
    }
    res[resLenght - 1] = '\0';

    free(board);
    return res;
}

