//
// Created by Tim on 2018-04-09.
//

#ifndef FINALASSIGNMENTTIM_OBJECT_H
#define FINALASSIGNMENTTIM_OBJECT_H


#include <glm.hpp>
#include <vector>
#include <string>
#include <functional>

#include "Shader/Shader.h"
#include "VertexArray.h"
#include "Texture.h"
#include "Model.h"

struct Material {
    glm::vec3 ambientColor;
    glm::vec3 diffuseColor;
    glm::vec3 specular;
    float power;
};

class Object
{
public:
    Object(Model *model, Texture *texture, Shader *shader, Material *material);

    ~Object();

    void CalculateRenderModelMatrix() const;

    inline glm::mat4 GetModel() const { return m_ModelMatrix; }

    inline void SetModel(glm::mat4 model) { m_ModelMatrix = model; }

    inline glm::mat4 GetRenderModelMatrix() const { return m_RenderModelMatrix; }

    inline Shader *GetShader() const { return m_Shader; }

    inline unsigned int GetCount() const { return m_Model->GetCount(); }

    inline Material *GetMaterial() const { return m_Material; }

    inline bool HasTexture() const { return m_Texture != nullptr; };

    inline bool HasRenderModelMatrixChanged() const { return m_RenderModelMatrixChanged; }

    void Scale(float x, float y, float z, bool editBase);

    void Translate(float x, float y, float z, bool editBase);

    void Rotate(float x, float y, float z, bool editBase);

    void Bind() const;

private:
    Model *m_Model;
    Material *m_Material;
    Texture *m_Texture;
    Shader *m_Shader;

    glm::mat4 m_ModelMatrix;
    mutable glm::mat4 m_RenderModelMatrix;
    mutable bool m_RenderModelMatrixChanged;

    glm::vec3 m_ScaleVector;
    glm::vec3 m_TranslateVector;
    float m_RotateX;
    float m_RotateY;
    float m_RotateZ;

};


#endif //FINALASSIGNMENTTIM_OBJECT_H
