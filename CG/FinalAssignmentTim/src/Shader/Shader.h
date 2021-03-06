//
// Created by Tim on 2018-04-02.
//

#ifndef FINALASSIGNMENTTIM_SHADER_H
#define FINALASSIGNMENTTIM_SHADER_H


#include <string>
#include <vec4.hpp>
#include <iostream>
#include <fstream>
#include <sstream>
#include <detail/type_mat.hpp>
#include "../Renderer.h"
#include "../Object.h"
#include <dirent.h>
#include <unordered_map>
#include <glm.hpp>

struct ShaderProgramSource {
    std::string VertexSource;
    std::string FragmentSource;
};

class Shader {
public:
    explicit Shader(const std::string& filepath);
    ~Shader();

    void Bind() const;
    void Unbind() const;

    // Set uniforms
    void SetUniform1f(const std::string &name, float value);
    void SetUniform4f(const std::string& name, glm::vec4 values);
    void SetUniform1i(const std::string& name, int v0);
    void SetUniform3f(const std::string& name, glm::vec3 values);
    void SetUniformMat4f(const std::string& name, const glm::mat4& matrix);

    virtual void SetPreRenderUniforms(
            const Object &object,
            const glm::mat4 &view,
            const glm::mat4 &projection
    ) = 0;

private:
    unsigned int m_RendererID;
    std::string m_FilePath;
    std::unordered_map<std::string, int> m_UniformLocationCache;

    int GetUniformLocation(const std::string& name);

    unsigned int CreateShader(const std::string& vs, const std::string& fs);
    unsigned int CompileShader(unsigned int type, const std::string& source);
    ShaderProgramSource ParseShader(const std::string& path);
};

#endif //FINALASSIGNMENTTIM_SHADER_H
