//
// Created by Tim on 2018-04-01.
//

#ifndef FINALASSIGNMENTTIM_RENDERER_H
#define FINALASSIGNMENTTIM_RENDERER_H

#include <GL/glew.h>
#include <iostream>
#include "MyMacros.h"
#include "VertexArray.h"
#include "IndexBuffer.h"
#include "VertexBufferLayout.h"
#include "Shader/Shader.h"

using namespace std;

// Forward declaratuib
class IndexBuffer;
class Shader;
class VertexArray;

enum class ShaderType {
    NONE = -1, VERTEX = 0, FRAGMENT = 1,
};


void GLClearErrors();
bool GLLogCall(const char* function, const char* file, int line);

class Renderer {
public:
    void Clear() const;
    void Draw(const VertexArray &vao, const IndexBuffer &ibo, const Shader &shader) const;

private:

};



#endif //FINALASSIGNMENTTIM_RENDERER_H
