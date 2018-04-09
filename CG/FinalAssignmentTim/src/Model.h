//
// Created by Tim on 2018-04-09.
//

#ifndef FINALASSIGNMENTTIM_MODEL_H
#define FINALASSIGNMENTTIM_MODEL_H

#include <glm.hpp>
#include "VertexBuffer.h"
#include "VertexArray.h"

class Model
{
public:
    explicit Model(const std::string &modelPath);

    virtual ~Model();

    void Bind() const;

    inline unsigned int GetCount() const { return m_Vertices.size(); }

    private:
    std::vector<glm::vec3> m_Vertices;
    std::vector<glm::vec3> m_Normals;
    std::vector<glm::vec2> m_UVs;

    VertexBuffer *m_VBOVertices;
    VertexBuffer *m_VBONormals;
    VertexBuffer *m_VBOUVs;

    VertexArray *m_VAO;

};



#endif //FINALASSIGNMENTTIM_MODEL_H
