cmake_minimum_required(VERSION 3.9)
set(PROJECTNAME OpenGLTest)
project(${PROJECTNAME})

set(CMAKE_CXX_STANDARD 17)

add_executable(OpenGLTest main.cpp)

#set(INCL_DIR /home/tim/Documents/CGLibraries/)
#set(DFLT_INCL include/GL)
#add_library(freeglut STATIC ${INCL_DIR}/freeglut/${DFLT_INCL})
#add_library(glew STATIC IMPORTED ./../glew/lib/libGLEW.so)
#add_library(glm STATIC ${INCL_DIR}/glm/glm)

#SET_TARGET_PROPERTIES(freeglut PROPERTIES LINKER_LANGUAGE C)
#SET_TARGET_PROPERTIES(glew PROPERTIES LINKER_LANGUAGE C)
#SET_TARGET_PROPERTIES(glm PROPERTIES LINKER_LANGUAGE C)

find_package(OpenGL REQUIRED)
if(NOT OPENGL_FOUND)
    message(ERROR " OPENGL not found!")
endif(NOT OPENGL_FOUND)
find_package(GLUT REQUIRED)
find_package(X11 REQUIRED)
#find_package(GLEW REQUIRED)
#find_package(glsl)
include_directories( ${OPENGL_INCLUDE_DIR} ${GLUT_INCLUDE_DIR} ${X11_INCLUDE_DIR})

target_link_libraries(${PROJECTNAME} ${X11_LIBRARIES} ${OPENGL_LIBRARIES} ${GLUT_LIBRARY} GLEW)