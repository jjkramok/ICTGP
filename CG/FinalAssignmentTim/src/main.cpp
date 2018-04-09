#include <GL/glew.h>
#include <GLFW/glfw3.h>
#include <glm.hpp>
#include <gtc/matrix_transform.hpp>
#include <stdio.h>
#include "MyMacros.h"
#include "Renderer.h"
#include "VertexBuffer.h"
#include "VertexBufferLayout.h"
#include "IndexBuffer.h"
#include "VertexArray.h"
#include "Shader/Shader.h"
#include "Texture.h"

using namespace std;

/* Typedefs*/
struct Object {
    VertexArray vao;

    glm::mat4 model;
    glm::mat4 model_view;
};

enum ViewMode {None = -1, Walking = 0, BirdsEye = 1};
struct Camera {
    glm::vec3 pos;
    glm::vec3 direction;
    glm::vec3 front = glm::vec3(0.0f, 0.0f, -1.0f);
    glm::vec3 up = glm::vec3(0.0f, 1.0f, 0.0f);
};

/* Constants and global variables */
const int Width = 960, Height = 540;
ViewMode mode = Walking;
Camera camera = {glm::vec3(0.0f, 0.0f, 3.0f), glm::vec3(0.0f, 0.0f, 0.0f)};

float deltaTime = 0.0f; // Time between current frame and last frame
float lastFrame = 0.0f; // Time of last frame

glm::mat4 mvp;
glm::mat4 proj = glm::mat4();//glm::perspective(glm::radians(45.0f), (float) Width / (float) Height, 0.1f, 100.0f); /* fov / zoom, aspect ratio, near clipping plane, far clipping plane*/
//glm::mat4 proj = glm::ortho(0.0f, Width * 1.0f, 0.0f, Height * 1.0f, -1.0f, 1.0f); /* left edge, right edge, bottom edge, top edge, near plane, far plane. Everything outside will be culled */
glm::mat4 view = glm::mat4();//glm::translate(glm::mat4(1.0f), glm::vec3(-100, 0, 0));
glm::mat4 model = glm::mat4();//glm::translate(glm::mat4(1.0f), glm::vec3(200, 200, 0));

const unsigned int amountOfObjects = 10;
Object *objects;

float speed = 0.05f;

/* Main functions */
void HandleKeyPress(GLFWwindow* window, int key, int scancode, int action, int mods) {
    speed = 25.0f * deltaTime;
    if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS) {
        glfwDestroyWindow(window);
    } else if (key == GLFW_KEY_C && action == GLFW_RELEASE) {
        if (mode == Walking) {
            mode = BirdsEye;
            camera.pos = glm::vec3(0.0f, 0.0f, 0.0f);
            camera.direction.x = glm::radians(-45.0f);
            camera.direction.y = glm::radians(-45.0f);
        } else {
            mode = Walking;
            camera.pos = glm::vec3(0.0f, 0.0f, 0.0f);
            camera.direction.x = glm::radians(-90.0f);
            camera.direction.y = 0;
        }
        cout << "changed mode to: " << (mode ? "Walking" : "Birds Eye") << endl;
    }

    if (action == GLFW_PRESS) {
        switch (key) {
            case GLFW_KEY_W:
                camera.pos += speed * camera.front;
                break;
            case GLFW_KEY_A:
                camera.pos -= glm::normalize(glm::cross(camera.front, camera.up)) * speed;
                break;
            case GLFW_KEY_S:
                camera.pos -= speed * camera.front;
                break;
            case GLFW_KEY_D:
                camera.pos += glm::normalize(glm::cross(camera.front, camera.up)) * speed;
                break;
        }
    }

    cout << "camera at (x, y, z): " << camera.pos.x << ", " <<  camera.pos.y << ", " <<  camera.pos.z << endl;
}

/**
 * Updates the global view matrix and the the model view matrix of all objects to match the new camera state.
 */
void UpdateCamera() {
    // Update view matrix based on camera position
    view = glm::lookAt(camera.pos, camera.pos + camera.front, camera.up);

//    for (int i = 0; i < amountOfObjects; i++) {
//        objects[i] .model_view = view * objects[i].model;
//    }
}

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
    window = glfwCreateWindow(Width, Height, "Final Assignment", nullptr, nullptr);
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

    glfwSetKeyCallback(window, HandleKeyPress);

    objects = new Object[amountOfObjects];

    /*
     *  Scope that ends just before destroying the GL rendering context.
     *  This way any stack allocated objects will be destroyed before the context will be.
     **/
    {
        // Enable alpha rendering / blending
        GLCall(glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA)); // Setup basic transparency rendering.
        GLCall(glBlendEquation(GL_FUNC_ADD)); // Specify how to combine / handle overwriting on the target buffer.
        GLCall(glEnable(GL_BLEND)); // Enable transparency rendering.

        // Specify culling method
        GLCall(glEnable(GL_CULL_FACE));
        GLCall(glFrontFace(GL_CCW));

        Renderer renderer;

        UpdateCamera(); // update view matrix

        proj = glm::perspective(glm::radians(45.0f), // fov
                                (float) Width / (float) Height, // aspect ratio
                                0.1f, // near cull plane
                                100.0f // far cull plane
        );

        Texture *yellowbrk = Texture::CreateBMP("./../res/textures/Yellobrk.bmp");


        // Loop until window is closed by the user.
        while (!glfwWindowShouldClose(window)) {
            glfwPollEvents();
            renderer.Clear();

            /* Update a delta time variable to average out movement on different setups. */
            float currentFrame = glfwGetTime();
            deltaTime = currentFrame - lastFrame;
            lastFrame = currentFrame;

            UpdateCamera(); // Update view matrix
            mvp = proj * view * model;

            glfwSwapBuffers(window);
        }
    }

    /* Destroy all (non-stack) allocated memory */
    delete[] objects;

    glfwTerminate();
    return 0;
}