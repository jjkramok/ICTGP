//
// Created by Tim on 2018-04-14.
//

#include "BasicShader.h"

BasicShader::BasicShader() : Shader("../res/shaders/Basic.glsl") {}

BasicShader::~BasicShader() {}

void BasicShader::SetPreRenderUniforms(const Object &object, const glm::mat4 &view, const glm::mat4 &projection) {
    SetUniformMat4f("u_MVP", projection * view * object.GetRenderModelMatrix());
}