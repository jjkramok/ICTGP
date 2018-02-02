cmake_minimum_required(VERSION 3.9)
project(Tim)

set(CMAKE_CXX_STANDARD 17)

add_executable(Tim main.cpp Week1/Week1.cpp Week1/Week1.h)
add_library(myWeek1Library STATICWeek1/Week1.cpp Week1/Week1.h)