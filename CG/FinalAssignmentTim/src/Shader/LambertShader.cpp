//
// Created by Tim on 2018-04-09.
//

#include "LambertShader.h"

LambertShader::LambertShader() : Shader("../res/shaders/Lambert.glsl") {}

void LambertShader::SetUniformMV(glm::mat4 mvs) {
    SetUniformMat4f("mv", mvs);
}

void LambertShader::SetUniformProjection(glm::mat4 proj) {
    SetUniformMat4f("projection", proj);
}

void LambertShader::SetUniformLightPos(glm::vec3 lightPos) {
    SetUniform3f("lightPos", lightPos);
}

void LambertShader::SetUniformApplyTexture(bool useTexture) {
    SetUniform1i("u_UseTexture", useTexture ? 1 : 0);
}

void LambertShader::SetUniformMatAmbient(glm::vec3 ambient) {
    SetUniform3f("matAmbient", ambient);
}

void LambertShader::SetUniformMatDiffuse(glm::vec3 diffuse) {
    SetUniform3f("matDiffuse", diffuse);
}

void LambertShader::SetUniformMatSpecular(glm::vec3 specular) {
    SetUniform3f("matSpecular", specular);
}

void LambertShader::SetUniformMatPower(float power) {
    SetUniform1f("matPower", power);
}

void LambertShader::SetPreRenderUniforms(
        const Object &object,
        const glm::mat4 &view,
        const glm::mat4 &projection) {
    /* Set projection and define lighting position */
    SetUniformProjection(projection);
    SetUniformLightPos(glm::vec3(0.0f, 0.0f, 0.0f));
    /* Set lighting models */
    SetUniformMatAmbient(object.GetMaterial()->ambientColor);
    SetUniformMatDiffuse(object.GetMaterial()->diffuseColor);
    SetUniformMatSpecular(object.GetMaterial()->specular);
    SetUniformMatPower(object.GetMaterial()->power);
    /* Enable textures is available */
    SetUniformApplyTexture(object.HasTexture());
    /* Calculate model view matrix */
    SetUniformMV(view * object.GetRenderModelMatrix());
}
