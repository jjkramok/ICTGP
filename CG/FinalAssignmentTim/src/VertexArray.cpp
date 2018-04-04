//
// Created by Tim on 2018-04-02.
//

#include "VertexArray.h"
#include "VertexBufferLayout.h"

VertexArray::VertexArray() {
    GLCall(glGenVertexArrays(1, &m_RendererID));
    GLCall(glBindVertexArray(m_RendererID));
}

VertexArray::~VertexArray() {
    GLCall(glDeleteVertexArrays(1, &m_RendererID));
}

void VertexArray::AddBuffer(const VertexBuffer &vb, const VertexBufferLayout &layout) {
    // Set the GL state machine up for the correct VertexBuffer (vbo) and VertexArray (vao)
    Bind();
    vb.Bind();

    // Set the layout of the buffer
    const std::vector<VertexBufferElement>& elements = layout.GetElements();
    unsigned int offset = 0;
    for (int i = 0; i < elements.size(); i++) {
        //
        const VertexBufferElement& element = elements[i];
        GLCall(glEnableVertexAttribArray((GLuint) i));
        GLCall(glVertexAttribPointer(i, element.count, element.type, element.normalized, layout.GetStride(), (const void*) offset));
        offset += element.count * GLSizeOf(element.type);
    }
}

void VertexArray::Bind() const {
    GLCall(glBindVertexArray(m_RendererID));
}

void VertexArray::Unbind() const {
    GLCall(glBindVertexArray(0));
}
