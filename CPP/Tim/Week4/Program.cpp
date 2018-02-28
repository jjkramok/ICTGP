//
// Created by tim on 20-2-18.
//

#include <GL/gl.h>
#include <GL/freeglut.h>
#include "Program.h"

void Renderer() {
    static const GLfloat blue[] = { 0.0, 0.0, 0.4, 1.0};
    //glClearBufferfv(GL_COLOR, 0, blue);
    glutSwapBuffers();
}

int main() {


    return 0;
}