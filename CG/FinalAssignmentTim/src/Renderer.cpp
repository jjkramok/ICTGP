//
// Created by Tim on 2018-04-01.
//

#include "Renderer.h"

void GLClearErrors() {
    while (glGetError() != GL_NO_ERROR);
}

bool GLLogCall(const char* function, const char* file, int line) {
    while (GLenum error = glGetError()) {
        cout << " [OpenGL Error] (" << error << "); " << function <<
             " " << file << ":" << line << endl;
        return false;
    }
    return true;
}
