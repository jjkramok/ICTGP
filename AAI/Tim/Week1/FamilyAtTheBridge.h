//
// Created by Kramok on 5-2-2018.
//

#ifndef TIM_FAMILYATTHEBRIDGE_H
#define TIM_FAMILYATTHEBRIDGE_H

#include "Person.h"
#include <cstdlib>
#include <iostream>
using namespace std;

#define AMOUNT_OF_PERSONS 5
#define ACTION_PERSON_SIZE 2

// variable for all states ever crossed
struct State {
    Person *StartSide;
    Person *Action;
    Person *EndSide;
    int lightsourceTime;
};

class FamilyAtTheBridge {
public:
    explicit FamilyAtTheBridge(int lightsourceTime);
    FamilyAtTheBridge() : FamilyAtTheBridge(30) {}; // Call other constructor with fixed lightsourceTime value
    ~FamilyAtTheBridge();
    void SolveWithBacktracking();
    void PrintSolution();

private:
    // variables for current state
    Person **startSide;
    Person **action;
    Person **endSide;
    int lightsourceMaxTime;

    State *states;
};


#endif //TIM_FAMILYATTHEBRIDGE_H
