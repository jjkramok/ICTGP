//
// Created by Tim on 2018-04-01.
//

#include "Renderer.h"

void Renderer::Draw(const VertexArray &vao, const IndexBuffer &ibo, const Shader &shader) const {
    shader.Bind();
    ibo.Bind();
    vao.Bind();

    GLCall(glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, nullptr));
}

void GLClearErrors() {
    while (glGetError() != GL_NO_ERROR);
}

bool GLLogCall(const char* function, const char* file, int line) {
    while (GLenum error = glGetError()) {
        cout    << " [OpenGL Error] ("
                << error << " => "
                << gluErrorString(error) << ") " << endl << "\t"
                << function << endl << "\t"
                << file << ":" << line << endl;
        return false;
    }
    return true;
}
