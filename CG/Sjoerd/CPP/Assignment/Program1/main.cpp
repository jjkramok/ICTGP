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
int objectCount = 13;

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

glm::vec3 eye = glm::vec3(1, 1.75, 10);
glm::vec2 lookdirection = glm::vec2(glm::radians(-90.0f), 0);

unsigned char keysPressing = 0;
const unsigned char modeWander = 'w';
const unsigned char modeOverview = 'o';
unsigned char mode = modeWander;

unsigned char swingState = 1;
float swingSpeed = 0.024;

float bouncyBallSpeed = 0;
float bouncyBallState = 0;
//--------------------------------------------------------------------------------
// Keyboard handling
//--------------------------------------------------------------------------------
const unsigned char keys[] = { 'w', 1 << 0, 'a',1 << 1, 's',1 << 2, 'd', 1 << 3, 'z', 1 << 4, 'x', 1 << 5 };
void keyDown(unsigned char key, int a, int b)
{
	if (key == 27)
		glutExit();

	if (key == 'c') { // change mode.
		if (mode == modeOverview) {
			mode = modeWander;
			eye.x = 1;
			eye.y = 1.75;
			eye.z = 10;
			lookdirection.x = glm::radians(-90.0f);
			lookdirection.y = 0;
		}
		else {
			mode = modeOverview;
			eye.x = -15;
			eye.y = 15;
			eye.z = 10;
			lookdirection.x = glm::radians(-30.0f);
			lookdirection.y = glm::radians(-35.0f);
		}
	}

	for (int i = 0; i < 12; i += 2) {
		if (key == keys[i]) {
			keysPressing |= keys[i + 1];
		}
	}
}

void keyUp(unsigned char key, int a, int b) {
	for (int i = 0; i < 12; i += 2) {
		if (key == keys[i]) {
			keysPressing &= ~keys[i + 1];
		}
	}
}

#pragma region Render

glm::vec3 getCenter()
{
	glm::vec3 center = glm::vec3(0, 1, 0);

	center.x = cos(lookdirection.x) * (glm::radians(90.0) - abs(lookdirection.y)) + eye.x;
	center.z = sin(lookdirection.x) * (glm::radians(90.0) - abs(lookdirection.y)) + eye.z;
	float hd = sqrt(((center.x - eye.x) * (center.x - eye.x)) + ((center.z - eye.z) * (center.z - eye.z)));
	center.y = tan(lookdirection.y) * hd + eye.y;

	return center;
}

//--------------------------------------------------------------------------------
// Rendering
//--------------------------------------------------------------------------------
void updateObjects();
void Render()
{
	glClearColor(0.0, 0.0, 0.0, 1.0);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	updateObjects();

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
void UpdateCamera();
void Render(int n)
{
	UpdateCamera();
	Render();
	glutTimerFunc(DELTA, Render, 0);
}

void mouseUpdate(int x, int y) {
	const float mouseSpeed = 0.01;
	lookdirection.x += (x - WIDTH / 2) * mouseSpeed;
	lookdirection.y -= (y - HEIGHT / 2) * mouseSpeed;

	if (lookdirection.y > glm::radians(89.99)) {
		lookdirection.y = glm::radians(89.99);
	}
	if (lookdirection.y < -glm::radians(89.99)) {
		lookdirection.y = -glm::radians(89.99);
	}

	glutWarpPointer(WIDTH / 2, HEIGHT / 2);
}

void updateCameraPosition() {
	if (mode == modeOverview) return;

	const float flyspeed = 0.02;
	const float walkspeed = 0.05;
	if (keysPressing & keys[1]) { // w
		eye.x += cos(lookdirection.x) * walkspeed;
		eye.z += sin(lookdirection.x) * walkspeed;
	}

	if (keysPressing & keys[3]) { // a
		eye.x += cos(glm::radians(-90.0) + lookdirection.x) * walkspeed;
		eye.z += sin(glm::radians(-90.0) + lookdirection.x) * walkspeed;
	}

	if (keysPressing & keys[5]) { // s
		eye.x += cos(glm::radians(180.0) + lookdirection.x) * walkspeed;
		eye.z += sin(glm::radians(180.0) + lookdirection.x) * walkspeed;
	}

	if (keysPressing & keys[7]) { // d
		eye.x += cos(glm::radians(90.0) + lookdirection.x) * walkspeed;
		eye.z += sin(glm::radians(90.0) + lookdirection.x) * walkspeed;
	}

	if (keysPressing & keys[9]) { // z
		eye.y += flyspeed;
	}

	if (keysPressing & keys[11]) { // x
		eye.y -= flyspeed;
	}
}


void UpdateCamera() {
	// mouse
	glutSetCursor(GLUT_CURSOR_NONE);
	glutPassiveMotionFunc(mouseUpdate);

	updateCameraPosition();

	view = glm::lookAt(
		eye,
		getCenter(),
		glm::vec3(0.0, 1.0, 0.0));

	for (int i = 0; i < objectCount; i++) {
		objects[i].mv = view * objects[i].model;
	}
}

void updateObjects()
{
	if (swingState)
		swingSpeed += 0.0003;
	else
		swingSpeed -= 0.0003;

	if (swingSpeed > 0.025 || swingSpeed < -0.025)
		swingState = !swingState;

	objects[3].model = glm::rotate(objects[3].model, swingSpeed, glm::vec3(0.0f, 0.0f, 1.0f));
	objects[4].model = glm::rotate(objects[4].model, -swingSpeed, glm::vec3(0.0f, 0.0f, 1.0f));

	if (bouncyBallSpeed <= 0 || bouncyBallSpeed > 0.1)
		bouncyBallState = !bouncyBallState;

	if (bouncyBallState)
		bouncyBallSpeed += 0.001;
	else
		bouncyBallSpeed -= 0.001;

	objects[12].model = glm::translate(objects[12].model, glm::vec3(0.0, bouncyBallState ? -bouncyBallSpeed : bouncyBallSpeed, 0.0));
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
	glutKeyboardFunc(keyDown);

	glutKeyboardUpFunc(keyUp);

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
	//house
	objects[0].model = glm::scale(glm::translate(glm::mat4(), glm::vec3(5.0, 0.0, 0.0)), glm::vec3(5, 5, 5));
	objects[1].model = glm::scale(glm::rotate(glm::translate(glm::mat4(), glm::vec3(5.0, 3.5, 2.5)), glm::radians(90.0f), glm::vec3(1.0, 0.0, 0.0)), glm::vec3(0.6, 0.6, 0.6));
	objects[11].model = glm::scale(glm::rotate(glm::translate(glm::mat4(), glm::vec3(5.0, 3.5, 2.5)), glm::radians(90.0f), glm::vec3(1.0, 0.0, 0.0)), glm::vec3(0.6, 0.1, 0.6));
	//swing
	objects[2].model = glm::translate(glm::mat4(), glm::vec3(-3.0, 0.5, 0.0));
	objects[3].model = glm::translate(glm::mat4(), glm::vec3(-3.0, 2.22, -0.4));
	objects[4].model = glm::translate(glm::mat4(), glm::vec3(-3.0, 2.22, 0.4));
	// skybox
	objects[5].model = glm::rotate(glm::rotate(glm::translate(glm::mat4(), glm::vec3(-50.0, 0.0, 0.0)), glm::radians(-90.0f), glm::vec3(0.0, 0.0, 1.0)), glm::radians(-90.0f), glm::vec3(0.0, 1.0, 0.0));
	objects[6].model = glm::rotate(glm::translate(glm::mat4(), glm::vec3(0.0, -50.0, 0.0)), glm::radians(0.0f), glm::vec3(1.0, 0.0, 0.0));
	objects[7].model = glm::rotate(glm::rotate(glm::translate(glm::mat4(), glm::vec3(0.0, 0.0, -50.0)), glm::radians(90.0f), glm::vec3(1.0, 0.0, 0.0)), glm::radians(180.0f), glm::vec3(0.0, 1.0, 0.0));
	objects[8].model = glm::rotate(glm::rotate(glm::translate(glm::mat4(), glm::vec3(50.0, 0.0, 0.0)), glm::radians(90.0f), glm::vec3(0.0, 0.0, 1.0)), glm::radians(90.0f), glm::vec3(0.0, 1.0, 0.0));
	objects[9].model = glm::rotate(glm::translate(glm::mat4(), glm::vec3(0.0, 50.0, 0.0)), glm::radians(180.0f), glm::vec3(1.0, 0.0, 0.0));
	objects[10].model = glm::rotate(glm::translate(glm::mat4(), glm::vec3(0.0, 0.0, 50.0)), glm::radians(-90.0f), glm::vec3(1.0, 0.0, 0.0));

	// bouncy ball
	objects[12].model = glm::translate(glm::mat4(), glm::vec3(-3, 6.6, -4));

	view = glm::lookAt(
		eye,
		getCenter(),
		glm::vec3(0.0, 1.0, 0.0));
	projection = glm::perspective(
		glm::radians(45.0f),
		1.0f * WIDTH / HEIGHT, 0.1f,
		2000.0f);

	for (int i = 0; i < objectCount; i++) {
		objects[i].mv = view * objects[i].model;
	}
}

//------------------------------------------------------------
// void InitObjects()
//------------------------------------------------------------

void InitObjects()
{
	loadOBJ("Objects/box.obj", objects[0].vertices, objects[0].uvs, objects[0].normals);
	loadOBJ("Objects/torus.obj", objects[1].vertices, objects[1].uvs, objects[1].normals);
	loadOBJ("Objects/swing1.obj", objects[2].vertices, objects[2].uvs, objects[2].normals);
	loadOBJ("Objects/swing2.obj", objects[3].vertices, objects[3].uvs, objects[3].normals);
	loadOBJ("Objects/swing2.obj", objects[4].vertices, objects[4].uvs, objects[4].normals);
	loadOBJ("Objects/skyboxplane.obj", objects[5].vertices, objects[5].uvs, objects[5].normals);
	loadOBJ("Objects/skyboxplane.obj", objects[6].vertices, objects[6].uvs, objects[6].normals);
	loadOBJ("Objects/skyboxplane.obj", objects[7].vertices, objects[7].uvs, objects[7].normals);
	loadOBJ("Objects/skyboxplane.obj", objects[8].vertices, objects[8].uvs, objects[8].normals);
	loadOBJ("Objects/skyboxplane.obj", objects[9].vertices, objects[9].uvs, objects[9].normals);
	loadOBJ("Objects/skyboxplane.obj", objects[10].vertices, objects[10].uvs, objects[10].normals);
	loadOBJ("Objects/cylinder18.obj", objects[11].vertices, objects[11].uvs, objects[11].normals);
	loadOBJ("Objects/sphere.obj", objects[12].vertices, objects[12].uvs, objects[12].normals);


	objects[0].textureID = loadBMP("Textures/Yellobrk.bmp");
	objects[2].textureID = loadBMP("Textures/swing.bmp");
	objects[3].textureID = loadBMP("Textures/swing.bmp");
	objects[4].textureID = loadBMP("Textures/swing.bmp");
	objects[5].textureID = loadBMP("Textures/negx.bmp");
	objects[6].textureID = loadBMP("Textures/negy.bmp");
	objects[7].textureID = loadBMP("Textures/negz.bmp");
	objects[8].textureID = loadBMP("Textures/posx.bmp");
	objects[9].textureID = loadBMP("Textures/posy.bmp");
	objects[10].textureID = loadBMP("Textures/posz.bmp");
}


//------------------------------------------------------------
// void InitMaterialsLight()
//------------------------------------------------------------

void InitMaterialsLight()
{
	light.position = glm::vec3(40.0, 40.0, 40.0);

	for (int i = 0; i < objectCount; i++) {
		objects[i].material.ambientColor = glm::vec3(1, 1, 1);
		objects[i].material.diffuseColor = glm::vec3(1, 1, 1);
		objects[i].material.specular = glm::vec3(1.0);
		objects[i].material.power = 128;
		objects[i].apply_texture = true;
	}

	objects[0].material.specular = glm::vec3(0);
	objects[1].material.ambientColor = glm::vec3(0.9, 0.1, 0.0);
	objects[1].material.diffuseColor = glm::vec3(0.9, 0.1, 0.0);
	objects[1].apply_texture = false;
	objects[11].material.ambientColor = glm::vec3(0.1, 0.1, 0.9);
	objects[11].material.diffuseColor = glm::vec3(0.1, 0.1, 0.9);
	objects[11].apply_texture = false;

	objects[12].material.ambientColor = glm::vec3(0.1, 0.7, 0.1);
	objects[12].material.diffuseColor = glm::vec3(0.1, 0.8, 0.1);
	objects[12].apply_texture = false;

	objects[2].material.specular = glm::vec3(0.3);
	objects[3].material.specular = glm::vec3(0.3);
	objects[4].material.specular = glm::vec3(0.3);

	for (int i = 5; i <= 10; i++) {
		objects[i].material.specular = glm::vec3(0.1);
	}
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

	glutWarpPointer(WIDTH / 2, HEIGHT / 2);

	glutMainLoop();

	return 0;
}
