#include <iostream>
#include <vector>

#include <GL/glew.h>
#include <GL/freeglut.h>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "glsl.h"
#include "objloader.hpp"

using namespace std;

//--------------------------------------------------------------------------------
// Consts
//--------------------------------------------------------------------------------

const char * fragshader_name = "fragmentshader.fsh";
const char * vertexshader_name = "vertexshader.vsh";
const int WIDTH = 800, HEIGHT = 600;
const int DELTA = 10;


//--------------------------------------------------------------------------------
// Variables
//--------------------------------------------------------------------------------

GLuint shaderID;
GLuint vao;
GLuint uniform_mvp;

glm::mat4 model, view, projection;
glm::mat4 mvp;


//--------------------------------------------------------------------------------
// Mesh variables
//--------------------------------------------------------------------------------


vector<glm::vec3> vertices;
vector<glm::vec3> normals;
vector<glm::vec2> uvs;



//--------------------------------------------------------------------------------
// Keyboard handling
//--------------------------------------------------------------------------------

void keyboardHandler(unsigned char key, int a, int b)
{
    if (key == 27)
        glutExit();
}


//--------------------------------------------------------------------------------
// Rendering
//--------------------------------------------------------------------------------

void Render()
{
    glClearColor(0.0, 0.0, 0.0, 1.0);
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

    glUseProgram(shaderID);

    glBindVertexArray(vao);
    glDrawArrays(GL_TRIANGLES, 0, vertices.size());
    glBindVertexArray(0);

    model = glm::rotate(model, 0.01f, glm::vec3(0.0f, 1.0f, 0.0f));

    mvp = projection * view * model;

    glUniformMatrix4fv(uniform_mvp, 1, GL_FALSE, glm::value_ptr(mvp));

    glutSwapBuffers();
}



//------------------------------------------------------------
// void Render(int n)
// Render method that is called by the timer function
//------------------------------------------------------------

void Render(int n)
{
    Render();
    glutTimerFunc(DELTA, Render, 0);
}


//------------------------------------------------------------
// void InitGlutGlew(int argc, char **argv)
// Initializes Glut and Glew
//------------------------------------------------------------

void InitGlutGlew(int argc, char **argv)
{
    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA | GLUT_DEPTH);
    glutInitWindowSize(WIDTH, HEIGHT);
    glutCreateWindow("Hello OpenGL");
    glutDisplayFunc(Render);
    glutKeyboardFunc(keyboardHandler);
    glutTimerFunc(DELTA, Render, 0);

    glewInit();
}


//------------------------------------------------------------
// void InitShaders()
// Initialized the fragmentshader and vertexshader
//------------------------------------------------------------

void InitShaders()
{
    char * fragshader = glsl::readFile(fragshader_name);
    GLuint fshID = glsl::makeFragmentShader(fragshader);

    char * vertexshader = glsl::readFile(vertexshader_name);
    GLuint vshID = glsl::makeVertexShader(vertexshader);

    shaderID = glsl::makeShaderProgram(vshID, fshID);
}


//------------------------------------------------------------
// void InitMatrices()
//------------------------------------------------------------

void InitMatrices()
{
    model = glm::mat4();
    view = glm::lookAt(
        glm::vec3(0.0, 2.0, 4.0),
        glm::vec3(0.0, 0.5, 0.0),
        glm::vec3(0.0, 1.0, 0.0));
    projection = glm::perspective(
        glm::radians(45.0f),
        1.0f * WIDTH / HEIGHT, 0.1f,
        20.0f);
    mvp = projection * view * model;

}

//------------------------------------------------------------
// void InitObjects()
//------------------------------------------------------------

void InitObjects()
{
    bool res = loadOBJ("teapot.obj", vertices, uvs, normals);
}


//------------------------------------------------------------
// void InitBuffers()
// Allocates and fills buffers
//------------------------------------------------------------

void InitBuffers()
{
    GLuint vbo_vertices;

    // vbo for vertices
    glGenBuffers(1, &vbo_vertices);
    glBindBuffer(GL_ARRAY_BUFFER, vbo_vertices);
    glBufferData(GL_ARRAY_BUFFER, vertices.size() * sizeof(glm::vec3),
        &vertices[0], GL_STATIC_DRAW);
    glBindBuffer(GL_ARRAY_BUFFER, 0);

    GLuint positionID = glGetAttribLocation(shaderID, "position");

    glGenVertexArrays(1, &vao);
    glBindVertexArray(vao);

    // Bind vertices to vao
    glBindBuffer(GL_ARRAY_BUFFER, vbo_vertices);
    glVertexAttribPointer(positionID, 3, GL_FLOAT, GL_FALSE, 0, 0);
    glEnableVertexAttribArray(positionID);
    glBindBuffer(GL_ARRAY_BUFFER, 0);

    // Attach to program (needed to send uniform vars)
    glUseProgram(shaderID);

    // Make uniform var
    uniform_mvp = glGetUniformLocation(shaderID, "mvp");

    // Fill uniform var
    glUniformMatrix4fv(uniform_mvp, 1, GL_FALSE, glm::value_ptr(mvp));

    glBindVertexArray(0);
}


int main(int argc, char ** argv)
{
    InitGlutGlew(argc, argv);
    InitShaders();
    InitMatrices();
    InitObjects();
    InitBuffers();

    HWND hWnd = GetConsoleWindow();
    ShowWindow(hWnd, SW_HIDE);

    glutMainLoop();

    return 0;
}
