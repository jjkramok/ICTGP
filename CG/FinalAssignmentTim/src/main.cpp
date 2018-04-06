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
#include "Shader.h"
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
    glm::vec2 direction;
};

/* Constants and global variables */
const int Width = 960, Height = 540;
ViewMode mode = Walking;
Camera camera = {glm::vec3(0.0f, 0.0f, 0.0f), glm::vec2(glm::radians(45.0f), 0.0f)};

glm::mat4 mvp;
glm::mat4 proj = glm::perspective(glm::radians(45.0f), (float) Width / (float) Height, 0.1f, 100.0f); /* fov / zoom, aspect ratio, near clipping plane, far clipping plane*/
//glm::mat4 proj = glm::ortho(0.0f, Width * 1.0f, 0.0f, Height * 1.0f, -1.0f, 1.0f); /* left edge, right edge, bottom edge, top edge, near plane, far plane. Everything outside will be culled */
glm::mat4 view = glm::translate(glm::mat4(1.0f), glm::vec3(-100, 0, 0));
glm::mat4 model = glm::translate(glm::mat4(1.0f), glm::vec3(200, 200, 0));

const unsigned int amountOfObjects = 10;
Object *objects;

const float speed = 1.0f;

/* Main functions */
void HandleKeyPress(GLFWwindow* window, int key, int scancode, int action, int mods) {
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

    if (action == GLFW_REPEAT) {
        switch (key) {
            case GLFW_KEY_W:
                camera.pos.x += cos(camera.direction.x) * speed;
                camera.pos.z += sin(camera.direction.x) * speed;
                break;
            case GLFW_KEY_A:
                camera.pos.x += cos(camera.direction.x + glm::radians(-90.0f)) * speed;
                camera.pos.z += sin(camera.direction.x + glm::radians(-90.0f)) * speed;
                break;
            case GLFW_KEY_S:
                camera.pos.x += cos(camera.direction.x + glm::radians(180.0f)) * speed;
                camera.pos.z += sin(camera.direction.x + glm::radians(180.0f)) * speed;
                break;
            case GLFW_KEY_D:
                camera.pos.x += cos(camera.direction.x + glm::radians(90.0f)) * speed;
                camera.pos.z += sin(camera.direction.x + glm::radians(90.0f)) * speed;
                break;
        }
    }

    cout << "camera at (x, y, z): " << camera.pos.x << ", " <<  camera.pos.y << ", " <<  camera.pos.z << endl;
}

/**
 * Updates the global view matrix and the the model view matrix of all objects to match the new camera state.
 */
void UpdateCamera() {
    glm::vec3 target = glm::vec3(0.0f, 0.0f, 0.0f);
    target.x = cos(camera.direction.x) * (glm::radians(90.0f) - abs(camera.direction.y)) + camera.pos.x;
    target.z = sin(camera.direction.x) * (glm::radians(90.0f) - abs(camera.direction.y)) + camera.pos.z;
    float d = (float) sqrt(pow(target.x - camera.pos.x, 2) + pow(target.z - camera.pos.z, 2));
    target.y = tan(camera.direction.y) * d + camera.pos.y;

    view = glm::lookAt(camera.pos, glm::vec3(0.0f, 0.0f, 0.0f), glm::vec3(0.0f, 1.0f, 0.0f));

    for (int i = 0; i < amountOfObjects; i++) {
        objects[i] .model_view = view * objects[i].model;
    }
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
        float pos[] = {
                100.0f, 100.0f, 0.0f, 0.0f, /* bottom left*/
                200.0f, 100.0f, 1.0f, 0.0f, /* bottom right */
                200.0f, 200.0f, 1.0f, 1.0f, /* top right */
                100.0f, 200.0f, 0.0f, 1.0f, /* top left */
        };
        unsigned int is[6] = {
                0, 1, 2,
                2, 3, 0,
        };

        GLCall(glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA)); // Setup basic transparency rendering.
        GLCall(glBlendEquation(GL_FUNC_ADD)); // Specify how to combine / handle overwriting on the target buffer.
        GLCall(glEnable(GL_BLEND)); // Enable transparency rendering.

        VertexArray vao;
        VertexBuffer vbo(pos, 4 * 4 * sizeof(float));
        VertexBufferLayout layout;
        layout.PushFloat(2);
        layout.PushFloat(2);
        vao.AddBuffer(vbo, layout);

        IndexBuffer ibo(is, 6);

        mvp = proj * view * model;

        Shader shader("./../res/shaders/Basic.glsl");
        shader.Bind();
        shader.SetUniform4f("u_Color", glm::vec4(0.8f, 0.3f, 0.8f, 1.0f));

        Texture texture("./../res/textures/blindguardian.png");
        unsigned char textureSlot = 0;
        texture.Bind(textureSlot);
        shader.SetUniform1i("u_Texture", textureSlot);
        shader.SetUniformMat4f("u_MVP", mvp);

        vao.Unbind();
        vbo.Unbind();
        ibo.Unbind();
        shader.Unbind();

        Renderer renderer;

        // Animation variables
        float r = 1.0f;
        float increment = 0.05f;

        // Loop until window is closed by the user.
        while (!glfwWindowShouldClose(window)) {
            glfwPollEvents();
            renderer.Clear();

            //view = glm::lookAt(glm::vec3(0.0f, 0.0f, 0.0f), glm::vec3(0.0f, 0.0f, 0.0f), glm::vec3(0, 1, 0)); /* camera position, camera target, upVector */
            //proj = glm::perspective(glm::radians(45.0f), (float) Width / (float) Height, 0.1f, 100.0f); /* fov / zoom, aspect ratio, near clipping plane, far clipping plane*/
            mvp = proj * view * model;

            shader.Bind();
            shader.SetUniform4f("u_Color", glm::vec4(r, 0.3f, 0.8f, 1.0f));
            shader.SetUniformMat4f("u_MVP", mvp);

            renderer.Draw(vao, ibo, shader);


            if (r > 1.0f) {
                increment = -increment;
            } else if (r < 0.0f) {
                increment = -increment;
            }
            r += increment;

            glfwSwapBuffers(window);
            glfwPollEvents();
        }
    }

    /* Destroy all (non-stack) allocated memory */
    delete[] objects;

    glfwTerminate();
    return 0;
}