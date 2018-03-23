//
// Created by tim on 23-3-18.
//

#ifndef TIM_BIGINT_H
#define TIM_BIGINT_H


#include <iostream>
using namespace std;

struct Digit
{
    Digit* prev;
    int digit;
    Digit* next;

    explicit Digit(int value) {
        prev = nullptr;
        next = nullptr;
        digit = value;
    }
};



class BigInt
{
public:
    BigInt();
    ~BigInt();
    void print();
    void read_number(istream& stream);
    void add_to_self(BigInt& summand);
    void add(BigInt& summand1, BigInt& summand2);
    void scale(int scale);

private:
    Digit* head;
    Digit* tail;

    void clear();
    bool is_empty();
    void add_to_front(int digit);
    void add_to_rear(int digit);
    void copy(BigInt& original);
    void do_add_to_self(BigInt& summand);
    void do_add(BigInt& summand1, BigInt& summand2);
};


#endif //TIM_BIGINT_H
