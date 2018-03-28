//
// Created by tim on 27-3-18.
//

#include "excersize2.h"

bool direction(unsigned char engine_status) {
    return engine_status & (1 << 4); // 4 is direction bit
}

int speed(unsigned char engine_status) {
    return (engine_status & ((1 << 7) | (1 << 6) | (1 << 5))) >> 5;
}

unsigned char set_status(int ID, bool direction, int speed) {
    return (1 << ID) | (speed << 5) | (direction << 4);
}
