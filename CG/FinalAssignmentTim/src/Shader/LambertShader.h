#ifndef FINALASSIGNMENTTIM_LAMBERTSHADER_HPP
#define FINALASSIGNMENTTIM_LAMBERTSHADER_HPP

#include <string>

#include "Shader.h"
#include "../Model.h"
#include "../Object.h"

class LambertShader : public Shader
{
private:
    static const std::string ShaderPath;
public:
    LambertShader();

    void SetUniformMV(glm::mat4 mvs);

    void SetUniformProjection(glm::mat4 proj);

    void SetUniformLightPos(glm::vec3 lightPos);

    void SetUniformApplyTexture(bool useTexture);

    void SetUniformMatAmbient(glm::vec3 ambient);

    void SetUniformMatDiffuse(glm::vec3 diffuse);

    void SetUniformMatSpecular(glm::vec3 specular);

    void SetUniformMatPower(float power);

    void SetPreRenderUniforms(const Object &d3Object, const glm::mat4 &view, const glm::mat4 &projection) override;
};


#endif //FINALASSIGNMENTTIM_LAMBERTSHADER_HPP