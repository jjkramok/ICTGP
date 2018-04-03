#include <iostream>
#include <vector>

#include <GL/glew.h>
#include <GL/freeglut.h>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "glsl.h"
#include "objloader.hpp"
#include "texture.hpp"


using namespace std;

//--------------------------------------------------------------------------------
// Consts
//--------------------------------------------------------------------------------

const char * fragshader_name = "fragmentshader.fsh";
const char * vertexshader_name = "vertexshader.vsh";
const int WIDTH = 800, HEIGHT = 600;
const int DELTA = 10;
int objectCount = 2;

#pragma region TypeDef

//--------------------------------------------------------------------------------
// Typedefs
//--------------------------------------------------------------------------------
struct LightSource
{
	glm::vec3 position;
};

struct Material
{
	glm::vec3 ambientColor;
	glm::vec3 diffuseColor;
	glm::vec3 specular;
	float power;
};

struct SingleObject 
{
	GLuint vao;
	GLuint textureID;
	glm::mat4 model;
	glm::mat4 outputModel;
	glm::mat4 mv;
	Material material;
	bool apply_texture;
	vector<glm::vec3> vertices;
	vector<glm::vec3> normals;
	vector<glm::vec2> uvs;

};

#pragma endregion

//--------------------------------------------------------------------------------
// Variables
//--------------------------------------------------------------------------------

GLuint shaderID;

SingleObject* objects = new SingleObject[objectCount];

glm::mat4 view, projection;

GLuint uniform_mv;
GLuint uniform_apply_texture;
GLuint uniform_material_ambient;
GLuint uniform_material_diffuse;
GLuint uniform_material_specular;
GLuint uniform_material_power;

LightSource light;

//--------------------------------------------------------------------------------
// Keyboard handling
//--------------------------------------------------------------------------------

void keyboardHandler(unsigned char key, int a, int b)
{
	if (key == 27)
		glutExit();
}

#pragma region Render
//--------------------------------------------------------------------------------
// Rendering
//--------------------------------------------------------------------------------

void Render()
{
	glClearColor(0.0, 0.0, 0.0, 1.0);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	objects[0].model = glm::rotate(objects[0].model, 0.01f, glm::vec3(0.0f, 1.0f, 0.0f));
	objects[1].model = glm::rotate(objects[1].model, 0.05f, glm::vec3(1.0f, 0.0f, 0.5f));

	glUseProgram(shaderID);

	for (int i = 0; i < objectCount; i++)
	{
		objects[i].mv = view * objects[i].model;

		if (objects[i].apply_texture)
		{
			glUniform1i(uniform_apply_texture, 1);
			glBindTexture(GL_TEXTURE_2D, objects[i].textureID);
		}
		else
			glUniform1i(uniform_apply_texture, 0);

		glUniformMatrix4fv(uniform_mv, 1, GL_FALSE, glm::value_ptr(objects[i].mv));
		glUniform3fv(uniform_material_ambient, 1, glm::value_ptr(objects[i].material.ambientColor));
		glUniform3fv(uniform_material_diffuse, 1, glm::value_ptr(objects[i].material.diffuseColor));
		glUniform3fv(uniform_material_specular, 1, glm::value_ptr(objects[i].material.specular));
		glUniform1f(uniform_material_power, objects[i].material.power);

		glBindVertexArray(objects[i].vao);
		glDrawArrays(GL_TRIANGLES, 0, objects[i].vertices.size());
		glBindVertexArray(0);
	}

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

#pragma endregion

#pragma region Init


//------------------------------------------------------------
// void InitGlutGlew(int argc, char **argv)
// Initializes Glut and Glew
//------------------------------------------------------------

void InitGlutGlew(int argc, char **argv)
{
	glutInit(&argc, argv);

	glutSetOption(GLUT_MULTISAMPLE, 8);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA | GLUT_DEPTH);
	glutInitWindowSize(WIDTH, HEIGHT);
	glutCreateWindow("Hello OpenGL");
	glutDisplayFunc(Render);
	glutKeyboardFunc(keyboardHandler);
	glutTimerFunc(DELTA, Render, 0);

	glEnable(GL_MULTISAMPLE);
	glEnable(GL_DEPTH_TEST);
	glClear(GL_DEPTH_BUFFER_BIT);

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
	objects[0].model = glm::mat4();
	objects[1].model = glm::translate(glm::mat4(), glm::vec3(3.0, 0.5, 0.0));;

	view = glm::lookAt(
		glm::vec3(0.0, 2.0, 6.0),
		glm::vec3(1.5, 0.5, 0.0),
		glm::vec3(0.0, 1.0, 0.0));
	projection = glm::perspective(
		glm::radians(45.0f),
		1.0f * WIDTH / HEIGHT, 0.1f,
		20.0f);

	for (int i = 0; i < objectCount; i++) {
		objects[i].mv = view * objects[i].model;
	}
}

//------------------------------------------------------------
// void InitObjects()
//------------------------------------------------------------

void InitObjects()
{
	bool res = loadOBJ("Objects/teapot.obj", objects[0].vertices, objects[0].uvs, objects[0].normals);
	res = loadOBJ("Objects/box.obj", objects[1].vertices, objects[1].uvs, objects[1].normals);

	objects[0].textureID = loadBMP("Textures/Yellobrk.bmp");
	objects[1].textureID= loadBMP("Textures/uvtemplate.bmp");
}


//------------------------------------------------------------
// void InitMaterialsLight()
//------------------------------------------------------------

void InitMaterialsLight()
{
	light.position = glm::vec3(4.0, 4.0, 4.0);

	objects[0].material.ambientColor = glm::vec3(0.0, 0.0, 0.0);
	objects[0].material.diffuseColor = glm::vec3(0.0, 0.0, 0.0);
	objects[0].material.specular = glm::vec3(1.0);
	objects[0].material.power = 128;
	objects[0].apply_texture = true;

	objects[1].material.ambientColor = glm::vec3(0.3, 0.3, 0.0);
	objects[1].material.diffuseColor = glm::vec3(0.5, 0.5, 0.0);
	objects[1].material.specular = glm::vec3(1.0);
	objects[1].material.power = 128;
	objects[1].apply_texture = false;
}


//------------------------------------------------------------
// void InitBuffers()
// Allocates and fills buffers
//------------------------------------------------------------

void InitBuffers()
{
	GLuint positionID = glGetAttribLocation(shaderID, "position");
	GLuint normalID = glGetAttribLocation(shaderID, "normal");
	GLuint uvID = glGetAttribLocation(shaderID, "uv");

	// Attach to program (needed to send uniform vars)
	glUseProgram(shaderID);

	// Make uniform vars
	uniform_mv = glGetUniformLocation(shaderID, "mv");
	GLuint uniform_proj = glGetUniformLocation(shaderID, "projection");
	GLuint uniform_light_pos = glGetUniformLocation(shaderID, "lightPos");
	uniform_apply_texture = glGetUniformLocation(shaderID, "applyTexture");
	uniform_material_ambient = glGetUniformLocation(shaderID, "matAmbient");
	uniform_material_diffuse = glGetUniformLocation(shaderID, "matDiffuse");
	uniform_material_specular = glGetUniformLocation(shaderID, "matSpecular");
	uniform_material_power = glGetUniformLocation(shaderID, "matPower");

	// Fill uniform vars
	glUniformMatrix4fv(uniform_proj, 1, GL_FALSE, glm::value_ptr(projection));
	glUniform3fv(uniform_light_pos, 1, glm::value_ptr(light.position));

	for (int i = 0; i < objectCount; i++)
	{
		GLuint vbo_vertices, vbo_normals, vbo_uvs;

		// vbo for vertices
		glGenBuffers(1, &vbo_vertices);
		glBindBuffer(GL_ARRAY_BUFFER, vbo_vertices);
		glBufferData(GL_ARRAY_BUFFER, objects[i].vertices.size() * sizeof(glm::vec3),
			&objects[i].vertices[0], GL_STATIC_DRAW);
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		// vbo for normals
		glGenBuffers(1, &vbo_normals);
		glBindBuffer(GL_ARRAY_BUFFER, vbo_normals);
		glBufferData(GL_ARRAY_BUFFER, objects[i].normals.size() * sizeof(glm::vec3),
			&objects[i].normals[0], GL_STATIC_DRAW);
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		// vbo for uvs
		glGenBuffers(1, &vbo_uvs);
		glBindBuffer(GL_ARRAY_BUFFER, vbo_uvs);
		glBufferData(GL_ARRAY_BUFFER, objects[i].uvs.size() * sizeof(glm::vec2),
			&objects[i].uvs[0], GL_STATIC_DRAW);
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		glGenVertexArrays(1, &(objects[i].vao));
		glBindVertexArray(objects[i].vao);

		// Bind vertices to vao
		glBindBuffer(GL_ARRAY_BUFFER, vbo_vertices);
		glVertexAttribPointer(positionID, 3, GL_FLOAT, GL_FALSE, 0, 0);
		glEnableVertexAttribArray(positionID);
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		glBindBuffer(GL_ARRAY_BUFFER, vbo_normals);
		glVertexAttribPointer(normalID, 3, GL_FLOAT, GL_FALSE, 0, 0);
		glEnableVertexAttribArray(normalID);
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		glBindBuffer(GL_ARRAY_BUFFER, vbo_uvs);
		glVertexAttribPointer(uvID, 2, GL_FLOAT, GL_FALSE, 0, 0);
		glEnableVertexAttribArray(uvID);
		glBindBuffer(GL_ARRAY_BUFFER, 0);

		glBindVertexArray(0);
	}
}

#pragma endregion

int main(int argc, char ** argv)
{
	InitGlutGlew(argc, argv);
	InitShaders();
	InitMatrices();
	InitObjects();
	InitMaterialsLight();
	InitBuffers();

	HWND hWnd = GetConsoleWindow();
	ShowWindow(hWnd, SW_HIDE);

	glutMainLoop();

	return 0;
}
