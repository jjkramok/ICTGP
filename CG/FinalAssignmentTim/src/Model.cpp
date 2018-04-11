//
// Created by Tim on 2018-04-09.
//

#include "Model.h"

Model::Model(const std::string &modelPath) :
    m_VBOVertices(nullptr),
    m_VBONormals(nullptr),
    m_VBOUVs(nullptr),
    m_VAO(nullptr)
{
    bool IsLoaded = loadOBJ(modelPath.c_str(), m_Vertices, m_UVs, m_Normals);
    if (!IsLoaded) {
        cout << "Could not load model '" << modelPath << "'!" << endl;
        return;
    }

    m_VBOVertices = new VertexBuffer(&m_Vertices[0], m_Vertices.size() * sizeof(glm::vec3));
    VertexBufferLayout bufferVerticesLayout;
    bufferVerticesLayout.PushFloat(3);

    m_VBONormals = new VertexBuffer(&m_Normals[0], m_Normals.size() * sizeof(glm::vec3));
    VertexBufferLayout bufferNormalsLayout;
    bufferNormalsLayout.PushFloat(3);

    m_VBOUVs = new VertexBuffer(&m_UVs[0], m_UVs.size() * sizeof(glm::vec2));
    VertexBufferLayout bufferUVsLayout;
    bufferUVsLayout.PushFloat(2);

    m_VAO = new VertexArray();

    m_VAO->AddBuffer(*m_VBOVertices, bufferVerticesLayout);
    m_VAO->AddBuffer(*m_VBONormals, bufferNormalsLayout);
    m_VAO->AddBuffer(*m_VBOUVs, bufferUVsLayout);
}

Model::~Model() {
    delete m_VBOVertices;
    delete m_VBONormals;
    delete m_VBOUVs;
    delete m_VAO;
}

void Model::Bind() const {
    m_VAO->Bind();
}