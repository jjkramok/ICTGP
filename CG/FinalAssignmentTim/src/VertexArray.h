//
// Created by Tim on 2018-04-02.
//

#ifndef FINALASSIGNMENTTIM_VERTEXARRAY_H
#define FINALASSIGNMENTTIM_VERTEXARRAY_H

#include "VertexBuffer.h"
#include "VertexBufferLayout.h"
#include "Renderer.h"

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
