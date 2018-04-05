#include <GL/glew.h>
#include <GLFW/glfw3.h>

#include "MyMacros.h"
#include "Renderer.h"
#include "VertexBuffer.h"
#include "VertexBufferLayout.h"
#include "IndexBuffer.h"
#include "VertexArray.h"
#include "Shader.h"
#include "Texture.h"

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

    /*
     *  Scope that ends just before destroying the GL rendering context.
     *  This way any stack allocated objects will be destroyed before the context will be.
     **/
    {
        float pos[] = {
                -0.5f, -0.5f, 0.0f, 0.0f, /* bottom left*/
                0.5f, -0.5f, 1.0f, 0.0f, /* bottom right */
                0.5f, 0.5f, 1.0f, 1.0f, /* top right */
                -0.5f, 0.5f, 0.0f, 1.0f, /* top left */
        };
        unsigned int is[6] = {
                0, 1, 2,
                2, 3, 0,
        };

        VertexArray vao;
        VertexBuffer vbo(pos, 4 * 4 * sizeof(float));
        VertexBufferLayout layout;
        layout.PushFloat(2);
        layout.PushFloat(2);
        vao.AddBuffer(vbo, layout);

        IndexBuffer ibo(is, 6);

        Shader shader("./../res/shaders/Basic.glsl");
        shader.Bind();
        shader.SetUniform4f("u_Color", glm::vec4(0.8f, 0.3f, 0.8f, 1.0f));

        Texture texture("./../res/textures/blindguardian.png");
        unsigned char textureSlot = 0;
        texture.Bind(textureSlot);
        shader.SetUniform1i("u_Texture", textureSlot);

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
            renderer.Clear();

            shader.Bind();
            shader.SetUniform4f("u_Color", glm::vec4(r, 0.3f, 0.8f, 1.0f));

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

    glfwTerminate();
    return 0;
}