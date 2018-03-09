cmake_minimum_required(VERSION 3.9)
project(Tim C CXX)

set(CMAKE_CXX_STANDARD 17)

# OpenGL library stuff
find_package(OpenGL REQUIRED)
find_package(GLUT REQUIRED)
#find_package(GLM REQUIRED) <-- problem

set(PE PracticeExam)

include_directories( ${OPENGL_INCLUDE_DIRS} ${GLUT_INCLUDE_DIRS} )

#set(GLPath /usr/include/GL)
#set(GLMPath /usr/lib/glm-0.9.9-a2/glm)
#add_library(OpenGL STATIC ${GLPath}/glew.h ${GLPath}/freeglut.h ${GLMPath}/glm.hpp)

#SET_TARGET_PROPERTIES(OpenGL PROPERTIES LINKER_LANGUAGE C)

# Static library test
add_library(MyStaticLibrary STATIC Week2/MyStaticLibrary.cpp)

set(APP_EXE MyStaticLibraryTest)
add_executable(${APP_EXE} Week2/MyLibraryTest.cpp)
target_link_libraries(${APP_EXE} MyStaticLibrary ${CMAKE_DL_LIBS})

# Actual executables
add_executable(Tim main.cpp Week1/Week1.cpp Week1/Week1.h Week2/Program.cpp Week2/Program.h)
add_executable(Week2 Week2/Program.cpp Week2/Program.h)
add_executable(PracticeExam ${PE}/Program.cpp PracticeExam/BST.cpp PracticeExam/BST.h)

#set(GL_EXE Week4)
#add_executable(${GL_EXE} Week4/Program.cpp)
#target_link_libraries(${GL_EXE} ${OPENGL_LIBRARIES} ${GLUT_LIBRARY})