cmake_minimum_required(VERSION 3.6.2)
project(LinearAlgebra)

set(CMAKE_CXX_STANDARD 11)

add_executable(LinearAlgebra main.cpp Vector.cpp Vector.h Matrix.cpp Matrix.h)