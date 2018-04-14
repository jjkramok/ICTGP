//
// Created by Tim on 2018-04-09.
//

#include "Object.h"

Object::Object(Model *model, Texture *texture, Shader *shader, Material *material)
        : m_Model(model),
          m_Texture(texture),
          m_Shader(shader),
          m_Material(material),
          m_ModelMatrix(glm::mat4()),
          m_RenderModelMatrix(glm::mat4()),
          m_ScaleVector(glm::vec3(1.f)),
          m_TranslateVector(glm::vec3(0.f)),
          m_RotateX(0.f),
          m_RotateY(0.f),
          m_RotateZ(0.f)
{
}

Object::~Object() {}

void Object::Bind() const {
    /*
     * An object is just a collection of components needed to display a single object.
     * So 'binding an object' means: bind all its components.
     * */
    m_Model->Bind();
    m_Shader->Bind();

    if (m_Texture != nullptr)
        m_Texture->Bind();
}

void Object::CalculateRenderModelMatrix() const {
    m_RenderModelMatrix = m_ModelMatrix;
    //m_RenderModelMatrix = glm::translate(m_RenderModelMatrix, m_TranslateVector);
    //m_RenderModelMatrix = glm::scale(m_RenderModelMatrix, m_ScaleVector);
    //m_RenderModelMatrix = glm::rotate(m_RenderModelMatrix, glm::radians(m_RotateX), glm::vec3(1.f, 0.f, 0.f));
    //m_RenderModelMatrix = glm::rotate(m_RenderModelMatrix, glm::radians(m_RotateY), glm::vec3(0.f, 1.f, 0.f));
    //m_RenderModelMatrix = glm::rotate(m_RenderModelMatrix, glm::radians(m_RotateZ), glm::vec3(0.f, 0.f, 1.f));
}

void Object::Scale(float x, float y, float z) {
    m_ModelMatrix = glm::scale(m_ModelMatrix, glm::vec3(x, y, z));
    //m_ScaleVector = glm::vec3(x, y, z);
}

void Object::Translate(float x, float y, float z) {
    m_ModelMatrix = glm::translate(m_ModelMatrix, glm::vec3(x, y, z));
    //m_TranslateVector = glm::vec3(x, y, z);
}

void Object::Rotate(float x, float y, float z) {
    cout << " Rotate currently not supported!! " << endl;
    // TODO: angle
    //m_ModelMatrix = glm::rotate(m_ModelMatrix, 45, glm::vec3(x, y, z));
    //m_RotateX = x;
    //m_RotateY = y;
    //m_RotateZ = z;
}