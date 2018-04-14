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
#include "Object.h"

using namespace std;

// Forward declaratuib
class IndexBuffer;
class Shader;
class VertexArray;
class Object;

enum class ShaderType {
    NONE = -1, VERTEX = 0, FRAGMENT = 1,
};

class Renderer {
public:
    void Clear() const;
    void Draw(const VertexArray &vao, const IndexBuffer &ibo, const Shader &shader) const;
    void Draw(const Object &object, glm::mat4 &view, glm::mat4 &proj);

private:

};



#endif //FINALASSIGNMENTTIM_RENDERER_H
