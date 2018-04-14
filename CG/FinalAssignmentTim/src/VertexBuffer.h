//
// Created by Tim on 2018-04-01.
//

#ifndef FINALASSIGNMENTTIM_VERTEXBUFFER_H
#define FINALASSIGNMENTTIM_VERTEXBUFFER_H

#include <GL/glew.h>
#include "MyMacros.h"

class VertexBuffer {
public:
    VertexBuffer(const void *data, unsigned int size);
    ~VertexBuffer();

    void Bind() const;
    void Unbind() const;

private:
    unsigned int m_RendererID;
};


#endif //FINALASSIGNMENTTIM_VERTEXBUFFER_H
