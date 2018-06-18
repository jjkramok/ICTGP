#include <GL/glew.h>
#include <GLFW/glfw3.h>
#include <glm.hpp>
#include <gtc/matrix_transform.hpp>
#include <gtc/matrix_inverse.hpp>
#include <gtc/matrix_access.hpp>
#include <gtc/matrix_integer.hpp>
#include <ext.hpp>
#include <stdio.h>

#include "Renderer.h"
#include "Texture.h"
#include "Camera.h"
#include "MyMacros.h"
#include "VertexBuffer.h"
#include "VertexBufferLayout.h"
#include "IndexBuffer.h"
#include "VertexArray.h"
#include "Shader/Shader.h"
#include "Object.h"
#include "Shader/LambertShader.h"
#include "Shader/BasicShader.h"

using namespace std;

/* Typedefs*/
enum ViewMode {None = -1, Walking = 0, BirdsEye = 1};

/* Constants and global variables */
const int Width = 960, Height = 540;
ViewMode mode = Walking;
Camera *camera = new Camera(glm::vec3(0.0f, 0.0f, 10.0f), glm::vec3(0.0f, 0.0f, 0.0f));

float deltaTime = 0.0f; // Time between current frame and last frame
float lastFrame = 0.0f; // Time of last frame

glm::mat4 proj = glm::mat4();//glm::perspective(glm::radians(45.0f), (float) Width / (float) Height, 0.1f, 100.0f); /* fov / zoom, aspect ratio, near clipping plane, far clipping plane*/
//glm::mat4 proj = glm::ortho(0.0f, Width * 1.0f, 0.0f, Height * 1.0f, -1.0f, 1.0f); /* left edge, right edge, bottom edge, top edge, near plane, far plane. Everything outside will be culled */
glm::mat4 view = glm::mat4();//glm::translate(glm::mat4(1.0f), glm::vec3(-100, 0, 0));
glm::mat4 model = glm::mat4();//glm::translate(glm::mat4(1.0f), glm::vec3(200, 200, 0));

const unsigned int amountOfObjects = 10;

float speed = 0.05f;

/* Main functions */
void HandleKeyPress(GLFWwindow* window, int key, int scancode, int action, int mods) {
    speed = 150.0f * deltaTime;
    if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS) {
        glfwDestroyWindow(window);
    } else if (key == GLFW_KEY_C && action == GLFW_RELEASE) {
        if (mode == Walking) {
            mode = BirdsEye;
            camera->SetPosition(glm::vec3(0.0f, 25.0f, 0.0f));
            camera->SetDirection(glm::vec3(glm::radians(-45.0f), glm::radians(-45.0f), 0));
        } else {
            mode = Walking;
            camera->SetPosition(glm::vec3(0.0f, 0.0f, 10.0f));
            camera->SetDirection(glm::vec3(glm::radians(-90.0f), 0, 0));
        }
        cout << "changed mode to: " << (mode ? "Walking" : "Birds Eye") << endl;
    }

    if (action == GLFW_PRESS) {
        switch (key) {
            case GLFW_KEY_W:
                camera->SetPosition(camera->GetPosition() + (speed * camera->GetFrontVector()));
                break;
            case GLFW_KEY_A:
                camera->SetPosition(camera->GetPosition() - (glm::normalize( glm::cross(camera->GetFrontVector(), camera->GetUpVector()) ) * speed));
                //camera.pos -= glm::normalize(glm::cross(camera.front, camera.up)) * speed;
                break;
            case GLFW_KEY_S:
                camera->SetPosition(camera->GetPosition() - (speed * camera->GetFrontVector()));
                break;
            case GLFW_KEY_D:
                camera->SetPosition(camera->GetPosition() + (glm::normalize( glm::cross(camera->GetFrontVector(), camera->GetUpVector()) ) * speed));
                break;
        }
    }

    cout << "camera at (x, y, z): " << camera->GetPosition().x << ", " <<  camera->GetPosition().y << ", " <<  camera->GetPosition().z << endl;
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

    GLCall(glEnable(GL_DEPTH_TEST));
    GLCall(glClear(GL_DEPTH_BUFFER_BIT));

    // Initialize OpenGL rendering context.
    if (glewInit() != GLEW_OK) {
        cout << "Glew couldn't initialize!" << endl;
    }

    printf("OpenGL version supported by this platform (%s): \n",
           glGetString(GL_VERSION));

    glfwSetKeyCallback(window, HandleKeyPress);

    /*
     *  Scope that ends just before destroying the GL rendering context.
     *  This way any stack allocated objects will be destroyed before the context will be.
     **/
    {
        // Enable alpha rendering / blending
        GLCall(glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA)); // Setup basic transparency rendering.
        //GLCall(glBlendEquation(GL_FUNC_ADD)); // Specify how to combine / handle overwriting on the target buffer.
        GLCall(glEnable(GL_BLEND)); // Enable transparency rendering.

        // Specify culling method
        GLCall(glEnable(GL_CULL_FACE));
        GLCall(glFrontFace(GL_CCW));

        Renderer renderer;

        view = camera->UpdateView(); // instantiate the view matrix

        proj = glm::perspective(glm::radians(45.0f), // fov
                                (float) Width / (float) Height, // aspect ratio
                                0.1f, // near cull plane
                                100.0f // far cull plane
        );

        Texture *yellowbrk = Texture::CreateBMP("./../res/textures/Yellobrk.bmp");

        Model *boxModel = new Model("../res/objs/box.obj");
        //Model *teaModel = new Model("../res/objs/teapot.obj");
        Model *planeModel = new Model("../res/objs/MyPlane.obj");

        Material *material = new Material;
        material->ambientColor = glm::vec3(0.3f, 0.3f, 0.2f);
        material->diffuseColor = glm::vec3(0.4f, 0.4f, 0.0f);
        material->specular = glm::vec3(1.0f);
        material->power = 100;

        Shader *lambert = new LambertShader();
        Shader *basic = new BasicShader();

        Object *boxObj = new Object(boxModel, yellowbrk, basic, material);
        //Object *teaObj = new Object(teaModel, nullptr, basic, nullptr);
        Object *planeObj = new Object(planeModel, yellowbrk, basic, nullptr);
        planeObj->SetModel(glm::scale(glm::mat4(), glm::vec3(3, 1, 3)));

        vector<Object*> objs;
        objs.push_back(boxObj);
        //objs.push_back(teaObj);
        objs.push_back(planeObj);

        // TODO remove
        for (auto &&obj : objs) {
            obj->Translate(0, 0, 0);
        }

        int count = 0;
        // Loop until window is closed by the user.
        while (!glfwWindowShouldClose(window)) {
            renderer.Clear();

            /* Update a delta time variable to average out movement on different setups. */
            float currentFrame = glfwGetTime();
            deltaTime = currentFrame - lastFrame;
            lastFrame = currentFrame;

            count += 1;
            if (!(count % 80))
                cout << "rendering" << endl;

            view = camera->UpdateView(); // Update view matrix
            //mvp = proj * view * model; // OpenGL uses column major ordering, so multiple in reverse order.
            for (auto &&obj : objs) {
                renderer.Draw(*obj, view, proj);
            }

            glfwSwapBuffers(window);
            glfwPollEvents();
        }
    }

    /* Destroy all (non-stack) allocated memory */
    delete camera;

    glfwTerminate();
    return 0;
}