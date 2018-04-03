//
// Created by Tim on 2018-04-01.
//

#ifndef FINALASSIGNMENTTIM_INDEXBUFFER_H
#define FINALASSIGNMENTTIM_INDEXBUFFER_H

#include <GL/glew.h>
#include "Renderer.h"
#include "MyMacros.h"

class IndexBuffer {
public:
    IndexBuffer(const unsigned int *data, unsigned int count);
    ~IndexBuffer();

    void Bind() const;
    void Unbind() const;

    inline unsigned int GetCount() const { return m_Count; }

private:
    unsigned int m_RendererID;
    unsigned int m_Count;
};

#endif //FINALASSIGNMENTTIM_INDEXBUFFER_H
