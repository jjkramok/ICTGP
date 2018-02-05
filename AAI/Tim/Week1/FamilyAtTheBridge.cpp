//
// Created by Kramok on 5-2-2018.
//

#include "FamilyAtTheBridge.h"

FamilyAtTheBridge::FamilyAtTheBridge(int lightsourceTime) {
    this->lightsourceTime = lightsourceTime;

    // TODO determine max size and use that as length for calloc
    states = (State*) calloc(lightsourceTime, sizeof(State));

    startSide = (Person**) calloc(AMOUNT_OF_PERSONS, sizeof(Person*));
    action = (Person**) calloc(ACTION_PERSON_SIZE, sizeof(Person*));
    endSide = (Person**) calloc(AMOUNT_OF_PERSONS, sizeof(Person*));

    if (AMOUNT_OF_PERSONS != 5) {
        throw "Unsupported amount of persons!";
    }

    startSide[0] = new Person(1, 'A');
    startSide[1] = new Person(3, 'B');
    startSide[2] = new Person(6, 'C');
    startSide[3] = new Person(8, 'D');
    startSide[4] = new Person(12, 'E');
}

FamilyAtTheBridge::~FamilyAtTheBridge() {
    free(startSide);
    free(action);
    free(endSide);
    free(states);
}

void FamilyAtTheBridge::SolveWithBacktracking() {
    // backtrack with person 0 and person 1, and alternate on lightsource returner
    SolveWithBacktracking(*startSide[0], *startSide[1], *startSide[0], 1);
    SolveWithBacktracking(*startSide[0], *startSide[1], *startSide[1], 1);
}

void FamilyAtTheBridge::SolveWithBacktracking(Person nextTraveler0, Person nextTraveler1, Person returning, int step) {
    if (IsValidMove(nextTraveler0, nextTraveler1)) {


        if (SolutionFound()) {
            PrintSolution();
            return;
        } else {

            // TODO keep returner at StartSide, Move other person to EndSide
            // TODO it must also be possible to select a person at EndSide as returner

            // foreach combination of 2 from startSide --> backtrack
            for (int i = 0; i < AMOUNT_OF_PERSONS - 1; i++) {
                for (int j = i + 1; j < AMOUNT_OF_PERSONS; j++) {
                    // backtrack with person i and person j, and alternate on lightsource returner
                    SolveWithBacktracking(*startSide[i], *startSide[j], *startSide[i], step + 1);
                    SolveWithBacktracking(*startSide[i], *startSide[j], *startSide[j], step + 1);
                    // TODO reset person i and person j to StartSide
                }
            }
        }
    }
}

bool FamilyAtTheBridge::IsValidMove(Person a, Person b) {
    int timeCost = max(a.speed, b.speed);
    return lightsourceTime - timeCost >= 0;
}

bool FamilyAtTheBridge::SolutionFound() {
    for (int i = 0; i < AMOUNT_OF_PERSONS; i++) {
        if (startSide[i] == nullptr)
            return false;
    }
    return true;
}

void FamilyAtTheBridge::PrintSolution() {
    cout << "NOT IMPLEMENTED YET" << endl;
}