#include <GL/glew.h>
#include <GLFW/glfw3.h>
#include <iostream>
#include <fstream>
#include <sstream>

#include "MyMacros.h"
#include "Renderer.h"

using namespace std;

int main() {
    GLFWwindow *window;

    // Initialize the glfw window library
    if (!glfwInit()) {
        return -1;
    }

    // Set OpenGL version and profile.
    glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
    glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
    glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

    // Create the main window.
    window = glfwCreateWindow(860, 720, "Final Assignment", nullptr, nullptr);
    if (!window) {
        glfwTerminate();
        return -1;
    }

    // Bind the window as current context.
    glfwMakeContextCurrent(window);
    glfwSwapInterval(1);

    // Initialize OpenGL rendering context.
    if (glewInit() != GLEW_OK) {
        cout << "Glew couldn't initialize!" << endl;
    }

    printf("OpenGL version supported by this platform (%s): \n",
           glGetString(GL_VERSION));

    while (!glfwWindowShouldClose(window)) {
        glfwSwapBuffers(window);
        glfwPollEvents();
    }

    glfwTerminate();
    return 0;
}