cmake_minimum_required(VERSION 3.9)
project(LinearAlgebra)

set(CMAKE_CXX_STANDARD 17)

add_executable(LinearAlgebra main.cpp Vector.cpp Vector.h Matrix.cpp Matrix.h)