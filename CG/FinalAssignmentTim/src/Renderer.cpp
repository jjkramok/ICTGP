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

void Renderer::Draw(const Object &object, glm::mat4 &view, glm::mat4 &proj) {
    object.Bind();
    object.CalculateRenderModelMatrix();
    object.GetShader()->SetPreRenderUniforms(object, view, proj);

    GLCall(glDrawArrays(GL_TRIANGLES, 0, object.GetCount()));
}

void Renderer::Clear() const {
    GLCall(glClear(GL_COLOR_BUFFER_BIT));
}