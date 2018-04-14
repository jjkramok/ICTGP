//
// Created by Tim on 2018-04-14.
//

#ifndef FINALASSIGNMENTTIM_BASICSHADER_H
#define FINALASSIGNMENTTIM_BASICSHADER_H

#include <string>
#include "Shader.h"

class BasicShader  : public Shader {
private:
    static const std::string ShaderPath;
public:
    BasicShader();
    ~BasicShader();

    void SetPreRenderUniforms(const Object &object, const glm::mat4 &view, const glm::mat4 &projection) override;
};


#endif //FINALASSIGNMENTTIM_BASICSHADER_H
