//
// Created by Tim on 2018-04-12.
//

#include "MyMacros.h"

void MyMacros::GLClearErrors() {
    while (glGetError() != GL_NO_ERROR);
}

bool MyMacros::GLLogCall(const char* function, const char* file, int line) {
    while (GLenum error = glGetError()) {
        std::cout    << " [OpenGL Error] ("
                     << error << " => "
                     << gluErrorString(error) << ") " << std::endl << "\t"
                     << function << std::endl << "\t"
                     << file << ":" << line << std::endl;
        return false;
    }
    return true;
}