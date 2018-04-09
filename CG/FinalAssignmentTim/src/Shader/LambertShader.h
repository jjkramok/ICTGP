#ifndef FINALASSIGNMENTTIM_LAMBERTSHADER_HPP
#define FINALASSIGNMENTTIM_LAMBERTSHADER_HPP

#include <string>

#include "Shader.h"
#include "../Model.h"
#include "../worldObjects/D3Object.hpp"

class LambertShader : public Shader
{
private:
    static const std::string ShaderPath;
public:
    LambertShader();

    void SetUMV(glm::mat4 mat4);

    void SetUProjection(glm::mat4 mat4);

    void SetULightPos(glm::vec3 vec3);

    void SetUApplyTexture(bool b);

    void SetUMatAmbient(glm::vec3 vec3);

    void SetUMatDiffuse(glm::vec3 vec3);

    void SetUMatSpecular(glm::vec3 vec3);

    void SetUMatPower(float f);

    void SetPreRenderUniforms(const D3Object &d3Object, const glm::mat4 &view, const glm::mat4 &projection) override;
};


#endif //FINALASSIGNMENTTIM_LAMBERTSHADER_HPP