//
// Created by Tim on 2018-04-11.
//

#ifndef FINALASSIGNMENTTIM_CAMERA_H
#define FINALASSIGNMENTTIM_CAMERA_H

#include <glm.hpp>
#include <gtc/matrix_transform.hpp>

class Camera {
public:
    explicit Camera(glm::vec3 pos, glm::vec3 dir, glm::vec3 front, glm::vec3 up);
    explicit Camera(glm::vec3 pos, glm::vec3 dir);
    ~Camera();
    void UpdateCamera(glm::mat4 &view);
    glm::mat4 UpdateView();

    inline glm::vec3 GetPosition() const { return m_Pos; }
    inline glm::vec3 GetDirection() const { return m_Direction; }
    inline glm::vec3 GetFrontVector() const { return m_Front; }
    inline glm::vec3 GetUpVector() const { return m_Up; }

    inline void SetPosition(const glm::vec3 pos) { m_Pos = pos; }
    inline void SetDirection(const glm::vec3 dir) { m_Direction = dir; }


private:
    glm::vec3 m_Pos;
    glm::vec3 m_Direction;
    glm::vec3 m_Front = glm::vec3(0.0f, 0.0f, -1.0f);
    glm::vec3 m_Up = glm::vec3(0.0f, 1.0f, 0.0f);

};


#endif //FINALASSIGNMENTTIM_CAMERA_H
