//
// Created by Tim on 2018-04-02.
//

#ifndef FINALASSIGNMENTTIM_VERTEXARRAY_H
#define FINALASSIGNMENTTIM_VERTEXARRAY_H

#include "VertexBuffer.h"

// Forward declaration
class VertexBufferLayout;
class VertexBuffer;

class VertexArray {
public:
    VertexArray();
    ~VertexArray();

    void AddBuffer(const VertexBuffer& vb, const VertexBufferLayout& layout);
    void Bind() const;
    void Unbind() const;

private:
    unsigned int m_RendererID;

};


#endif //FINALASSIGNMENTTIM_VERTEXARRAY_H
