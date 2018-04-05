//
// Created by Tim on 2018-04-01.
//

#include "Renderer.h"

void GLClearErrors() {
    while (glGetError() != GL_NO_ERROR);
}

bool GLLogCall(const char* function, const char* file, int line) {
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

void Renderer::Draw(const VertexArray &vao, const IndexBuffer &ibo, const Shader &shader) const {
    shader.Bind();
    ibo.Bind();
    vao.Bind();

    GLCall(glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, nullptr));
}

void Renderer::Clear() const {
    GLCall(glClear(GL_COLOR_BUFFER_BIT));
}