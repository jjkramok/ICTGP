//
// Created by Tim on 2018-04-09.
//

#ifndef FINALASSIGNMENTTIM_OBJECT_H
#define FINALASSIGNMENTTIM_OBJECT_H


#include <glm.hpp>
#include <gtc/matrix_transform.hpp>
#include <vector>
#include <string>

#include "Shader/Shader.h"
#include "VertexArray.h"
#include "Texture.h"
#include "Model.h"

class Shader; // Forward declaration
class Texture; // Forward declaration
class Model; // Forward declaration

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

    /* Matrix operations */
    void Scale(float x, float y, float z);

    void Translate(float x, float y, float z);

    void Rotate(float x, float y, float z);

    void Bind() const;

private:
    /* All components that make up an object. */
    Model *m_Model;
    Material *m_Material;
    Texture *m_Texture;
    Shader *m_Shader;

    /* The original model matrix */
    mutable glm::mat4 m_ModelMatrix;

    /* Contains the result of all matrix operations on the original model matrix */
    mutable glm::mat4 m_RenderModelMatrix;

    /* Variables that contain temporary result of matrix operations on the original model matrix */
    glm::vec3 m_ScaleVector;
    glm::vec3 m_TranslateVector;
    float m_RotateX;
    float m_RotateY;
    float m_RotateZ;

};


#endif //FINALASSIGNMENTTIM_OBJECT_H
