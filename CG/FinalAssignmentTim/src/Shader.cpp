//
// Created by Tim on 2018-04-02.
//

#include <malloc.h>
#include "Shader.h"

Shader::Shader(const std::string &filepath)
    : m_FilePath(filepath), m_RendererID(0) {
    ShaderProgramSource sources = ParseShader(filepath);
    m_RendererID = CreateShader(sources.VertexSource, sources.FragmentSource);
}

Shader::~Shader() {
    GLCall(glDeleteProgram(m_RendererID));
}

void Shader::Bind() const {
    GLCall(glUseProgram(m_RendererID));
}

void Shader::Unbind() const {
    GLCall(glUseProgram(0));
}

void Shader::SetUniform4f(const std::string &name, glm::vec4 values) {
    GLCall(glUniform4f(GetUniformLocation(name), values[0], values[1], values[2], values[3]));
}

int Shader::GetUniformLocation(const std::string &name) {
    GLCall(int location = glGetUniformLocation(m_RendererID, name.c_str()));
    if (location == -1) {
        std::cout << "Warning: uniform '" << name << "' doesn't exist!" << std::endl;
    }
    return location;
}

/**
 * Creates a OpenGL Program.
 * @param vs Vertex Shader code.
 * @param fs fragment Shader code.
 * @return program id.
 */
unsigned int Shader::CreateShader(const std::string& vs, const std::string& fs)  {
    GLCall(unsigned int program = glCreateProgram());

    unsigned int vsp = CompileShader(GL_VERTEX_SHADER, vs);
    unsigned int fsp = CompileShader(GL_FRAGMENT_SHADER, fs);

    GLCall(glAttachShader(program, vsp));
    GLCall(glAttachShader(program, fsp));
    GLCall(glLinkProgram(program));
    GLCall(glValidateProgram(program));

    GLCall(glDeleteShader(vsp));
    GLCall(glDeleteShader(fsp));

    return program;
}

/**
 * Compiles a shader.
 * @param type type of the shader, see Renderer.h.
 * @param source the source code for the shader.
 * @return shader id.
 */
unsigned int Shader::CompileShader(unsigned int type, const std::string& source)  {
    GLCall(unsigned int id = glCreateShader(type));
    const char *src = source.c_str();
    GLCall(glShaderSource(id, 1, &src, nullptr));
    GLCall(glCompileShader(id));

    int res;
    GLCall(glGetShaderiv(id, GL_COMPILE_STATUS, &res));
    if (res == GL_FALSE) {
        int len;
        GLCall(glGetShaderiv(id, GL_INFO_LOG_LENGTH, &len));
        auto *msg = (char *) alloca(len);
        GLCall(glGetShaderInfoLog(id, len, &len, msg));
        std::cout << "Error! "
                  << (type == GL_VERTEX_SHADER ? "Vertex" : "Fragment")
                  << " shader failed to compile!"
                  << std::endl;
        std::cout << msg << std::endl;
        GLCall(glDeleteShader(id));

        return 0;
    }

    return id;
}

/**
 * Parses shader source code.
 * @param filepath path to the source code of the shader.
 * @return All parsed shaders.
 */
ShaderProgramSource Shader::ParseShader(const std::string& filepath) {
    std::ifstream stream(filepath);
    ASSERT(stream.is_open());
    std::string line;
    std::stringstream ss[2];
    ShaderType type = ShaderType::NONE;

    while (getline(stream, line)) {
        if (line.find("#shader") != std::string::npos) {
            if (line.find("vertex") != std::string::npos) {
                type = ShaderType::VERTEX;
            } else if (line.find("fragment") != std::string::npos) {
                type = ShaderType::FRAGMENT;
            }
        } else {
            ss[(int) type] << line << '\n';
        }
    }
    return (ShaderProgramSource) {ss[(int) ShaderType::VERTEX].str(), ss[(int) ShaderType::FRAGMENT].str()};
}