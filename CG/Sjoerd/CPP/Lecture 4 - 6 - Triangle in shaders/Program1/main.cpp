#include <iostream>
#include <GL/glew.h>
#include <GL/freeglut.h>
#include <glm/glm.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "glsl.h"

using namespace std;

void keyboardHandler(unsigned char key, int a, int b)
{
	if (key == 27)
		glutExit();
}

const char * fragshader_name = "fragmentshader.fsh";
const char * vertexshader_name = "vertexshader.vsh";

GLuint shaderID;

GLuint vao, positionID, colorID;

const GLfloat vertices[]
{
	0.5, -0.5, 0.0, 1.0,
	-0.5, -0.5, 0.0, 1.0,
	0.0, 0.5, 0.0, 1.0
};

const GLfloat colors[]
{
	1.0f, 0.0f, 0.0f, 1.0f,
	0.0f, 1.0f, 0.0f, 1.0f,
	0.0f, 0.0f, 1.0f, 1.0f
};


void Render()
{
	const glm::vec4 blue = glm::vec4(1.0f, 0.5f, 0.5f, 1.0f);
	glClearBufferfv(GL_COLOR, 0, glm::value_ptr(blue));

	glUseProgram(shaderID);

	glBindVertexArray(vao);

	//glDrawArrays(GL_TRIANGLES, 0, 3);
	glDrawArrays(GL_TRIANGLES, 0, 3);

	glBindVertexArray(0);

	// Disable array
	glDisableVertexAttribArray(positionID);
	glDisableVertexAttribArray(colorID);

	glutSwapBuffers();
}

int main(int argc, char ** argv)
{
#pragma region << * * * * * * Setup GLut & GLew * * * * * >>

	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
	glutInitWindowSize(800, 600);
	glutCreateWindow("Hello OpenGL");
	glutDisplayFunc(Render);
	glutKeyboardFunc(keyboardHandler);

	glewInit();

#pragma endregion

#pragma region << * * * * * * Shader Stuff * * * * * >>
	char * fragshader = glsl::readFile(fragshader_name);
	GLuint fshID = glsl::makeFragmentShader(fragshader);

	char * vertexshader = glsl::readFile(vertexshader_name);
	GLuint vshID = glsl::makeVertexShader(vertexshader);

	shaderID = glsl::makeShaderProgram(vshID, fshID);

#pragma endregion

	GLuint vbo1, vbo2;

	// Generate buffer object names (in this case 1)
	glGenBuffers(1, &vbo1);

	// Bind named buffer object
	glBindBuffer(GL_ARRAY_BUFFER, vbo1);

	// Create and initialize buffer object’s data store
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices),
		vertices, GL_STATIC_DRAW);

	// Unbind
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	// Generate buffer object names (in this case 1)
	glGenBuffers(1, &vbo2);

	// Bind named buffer object
	glBindBuffer(GL_ARRAY_BUFFER, vbo2);

	// Create and initialize buffer object’s data store
	glBufferData(GL_ARRAY_BUFFER, sizeof(colors),
		colors, GL_STATIC_DRAW);

	// Unbind
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	// Location of attribute variable
	positionID = glGetAttribLocation(shaderID, "position");
	colorID = glGetAttribLocation(shaderID, "color");

	glGenVertexArrays(1, &vao);
	glBindVertexArray(vao);

	glBindBuffer(GL_ARRAY_BUFFER, vbo1);
	// Define an array of generic vertex attribute data
	// ID, size, type, normalized, stride, offset first component
	glVertexAttribPointer(positionID, 4, GL_FLOAT, GL_FALSE, 0, 0);
	// Enable array
	glEnableVertexAttribArray(positionID);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glBindBuffer(GL_ARRAY_BUFFER, vbo2);
	// Define an array of generic vertex attribute data
	// ID, size, type, normalized, stride, offset first component
	glVertexAttribPointer(colorID, 4, GL_FLOAT, GL_FALSE, 0, 0);
	// Enable array
	glEnableVertexAttribArray(colorID);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glBindVertexArray(0);

	HWND hWnd = GetConsoleWindow();
	ShowWindow(hWnd, SW_HIDE);

	glutMainLoop();

	return 0;
}


