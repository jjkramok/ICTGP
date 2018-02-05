//
// Created by Kramok on 5-2-2018.
//

#include "FamilyAtTheBridge.h"

FamilyAtTheBridge::FamilyAtTheBridge(int lightsourceTime) {
    this->lightsourceMaxTime = lightsourceTime;

    // TODO determine max size and use that as length for calloc
    states = (State*) calloc(lightsourceTime, sizeof(State));

    startSide = (Person**) calloc(AMOUNT_OF_PERSONS, sizeof(Person*));
    action = (Person**) calloc(ACTION_PERSON_SIZE, sizeof(Person*));
    endSide = (Person**) calloc(AMOUNT_OF_PERSONS, sizeof(Person*));

    if (AMOUNT_OF_PERSONS != 5) {
        throw "Unsupported amount of persons!";
    }

    startSide[0] = new Person(1);
    startSide[1] = new Person(3);
    startSide[2] = new Person(6);
    startSide[3] = new Person(8);
    startSide[4] = new Person(12);
}

FamilyAtTheBridge::~FamilyAtTheBridge() {
    free(startSide);
    free(action);
    free(endSide);
    free(states);
}

void PrintSolution() {
    cout << "NOT IMPLEMENTED YET" << endl;
}