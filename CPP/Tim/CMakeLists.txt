cmake_minimum_required(VERSION 3.9)
project(Tim C CXX)

set(CMAKE_CXX_STANDARD 17)

# Static library test
add_library(MyStaticLibrary STATIC Week2/MyStaticLibrary.cpp)

set(APP_EXE MyStaticLibraryTest)
add_executable(${APP_EXE} Week2/MyLibraryTest.cpp)
target_link_libraries(${APP_EXE} MyStaticLibrary ${CMAKE_DL_LIBS})

# Actual executables
add_executable(Tim main.cpp Week1/Week1.cpp Week1/Week1.h Week2/Program.cpp Week2/Program.h)
add_executable(WeekTwo Week2/Program.cpp Week2/Program.h)
add_executable(PracticeExame PracticeExam/Program.cpp PracticeExam/BST.cpp PracticeExam/BST.h PracticeExam/DoublyLinkedList.cpp PracticeExam/DoublyLinkedList.h PracticeExam/DLLNode.cpp PracticeExam/DLLNode.h PracticeExam/BigInt.cpp PracticeExam/BigInt.h)

#set(GL_EXE Week4)
#add_executable(${GL_EXE} Week4/Program.cpp)
#target_link_libraries(${GL_EXE} ${OPENGL_LIBRARIES} ${GLUT_LIBRARY})