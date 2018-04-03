//
// Created by Tim on 2018-04-02.
//

#ifndef FINALASSIGNMENTTIM_VERTEXBUFFERLAYOUT_H
#define FINALASSIGNMENTTIM_VERTEXBUFFERLAYOUT_H

#include <vector>
#include <GL/glew.h>
#include "MyMacros.h"

struct VertexBufferElement {
    unsigned int type;
    unsigned int count;
    unsigned char normalized;
};

/**
 * Function that determines the size of GLtypes based on their enum/defined value.
 * @param glType unsigned int defined value
 * @return size in bytes of the type
 */
static size_t GLSizeOf(unsigned int glType) {
    switch (glType) {
        case GL_FLOAT:          return sizeof(GLfloat);
        case GL_UNSIGNED_INT:   return sizeof(GLuint);
        case GL_UNSIGNED_BYTE:  return sizeof(GLubyte);
        default:
            ASSERT(false);
    }
    return 0;
}

class VertexBufferLayout {
public:
    VertexBufferLayout()
            : m_Stride(0) {

    }

    // deprecated. Specialized templates work wonky with the 'normal' C++ linker and compiler.
    template<typename  T>
    void Push(int count) {
        ASSERT(false);
    }

    inline void PushFloat(unsigned int count) {
        m_Elements.push_back((VertexBufferElement) { GL_FLOAT, count, GL_FALSE });
        m_Stride += count * GLSizeOf(GL_FLOAT);
    }

    inline void PushUInt(unsigned int count) {
        m_Elements.push_back((VertexBufferElement) { GL_UNSIGNED_INT, count, GL_FALSE });
        m_Stride += count * GLSizeOf(GL_UNSIGNED_INT);
    }

    inline void PushUChar(unsigned int count) {
        m_Elements.push_back((VertexBufferElement) { GL_UNSIGNED_BYTE, count, GL_TRUE });
        m_Stride += count * GLSizeOf(GL_UNSIGNED_BYTE);
    }

    inline const std::vector<VertexBufferElement>& GetElements() const { return m_Elements; }
    inline unsigned int GetStride() const { return m_Stride; }

private:
    std::vector<VertexBufferElement> m_Elements;
    unsigned int m_Stride;
};


#endif //FINALASSIGNMENTTIM_VERTEXBUFFERLAYOUT_H
