//
// Created by Tim on 2018-04-01.
//

#include <GL/glew.h>
#include <iostream>
#include "MyMacros.h"

using namespace std;

#ifndef FINALASSIGNMENTTIM_RENDERER_H
#define FINALASSIGNMENTTIM_RENDERER_H

enum class ShaderType {
    NONE = -1, VERTEX = 0, FRAGMENT = 1,
};

void GLClearErrors();
bool GLLogCall(const char* function, const char* file, int line);

#endif //FINALASSIGNMENTTIM_RENDERER_H
