//
// Created by Tim on 2018-04-11.
//

#include "Camera.h"

Camera::Camera(glm::vec3 pos, glm::vec3 dir, glm::vec3 front, glm::vec3 up) :
    m_Pos(pos),
    m_Direction(dir),
    m_Front(front),
    m_Up(up) {

}

Camera::Camera(glm::vec3 pos, glm::vec3 dir) :
        m_Pos(pos),
        m_Direction(dir) {

}

Camera::~Camera() {}

/**
 * Updates a view matrix to match the new camera state.
 */
void Camera::UpdateCamera(glm::mat4 &view) {
    // Update view matrix based on camera position
    view = glm::lookAt(m_Pos, m_Pos + m_Front, m_Up);
}

/**
 * Calculates the view matrix that matches the new camera state.
 */
glm::mat4 Camera::UpdateView() {
    // Update view matrix based on camera position
    return glm::lookAt(m_Pos, m_Pos + m_Front, m_Up);
}