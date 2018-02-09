cmake_minimum_required(VERSION 3.9)
project(Tim)

set(CMAKE_CXX_STANDARD 17)

# Static library test
add_library(MyStaticLibrary STATIC Week2/MyStaticLibrary.cpp)

set(APP_EXE MyStaticLibraryTest)
add_executable(${APP_EXE} Week2/MyLibraryTest.cpp)
target_link_libraries(${APP_EXE} MyStaticLibrary ${CMAKE_DL_LIBS})

# Actual executables
add_executable(Tim main.cpp Week1/Week1.cpp Week1/Week1.h Week2/Program.cpp Week2/Program.h)
add_executable(Week2 Week2/Program.cpp Week2/Program.h)