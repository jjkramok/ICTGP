//
// Created by tim on 2-2-18.
//

#ifndef TIM_NQUEENS_H
#define TIM_NQUEENS_H

#include <malloc.h>
#include <iostream>

class NQueens {
public:
    bool ShowSolutions;
    explicit NQueens(int n);
    ~NQueens();
    void SolveBacktracking();
    void SolveBacktracking(int row);
    char* ToString();

private:
    bool** queens;
    int n;
    int CountQueens();
    bool CheckBoard();
};


#endif //TIM_NQUEENS_H
