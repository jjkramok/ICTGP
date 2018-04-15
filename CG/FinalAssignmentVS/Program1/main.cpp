#include <iostream>
#include <vector>

#include <GL/glew.h>
#include <GL/freeglut.h>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#define _USE_MATH_DEFINES
#include <math.h>

#include "glsl.h"
#include "objloader.hpp"
#include "texture.hpp"
#include "MyMacros.h"
#include "vendor/stb_image.h"

using namespace std;

//--------------------------------------------------------------------------------
// Consts
//--------------------------------------------------------------------------------

const char * fragshader_name = "fragmentshader.fsh";
const char * basicFragShader_name = "basicfragmentshader.fsh";
const char * vertexshader_name = "vertexshader.vsh";
const char * basicVertexShader_name = "basicvertexshader.vsh";
const int WIDTH = 800, HEIGHT = 600;
const int DELTA = 10;
const unsigned int amountOfObjects = 6;

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

enum ViewMode
{
	Walking = 0, BirdsEye = 1,
};

int keysPressed; // Keeps track of keys that remain pressed during render cycles.
enum MyKeys
{
	W = 0, S = 1, A = 2, D = 3, I = 6, K = 7, J = 8, L = 9, Y = 4, H = 5, // keysPressed layout
};

struct Camera 
{
	glm::vec3 pos = glm::vec3(0, 1, 10.0);
	glm::vec3 front = glm::vec3(0, 0, -1.0);
	glm::vec3 up = glm::vec3(0.0, 1.0f, 0);
	ViewMode mode = Walking;
	float speed = 0.1f;
	float viewSpeed = 0.01f;
	float liftSpeed = 0.05f;

	glm::mat4 GetView() 
	{
		return glm::lookAt(pos, pos + front, up);
	}
	
	void UpdatePosition() {
		if (mode == BirdsEye)
			return;
		if (CHECK_BIT(keysPressed, W)) {
			pos = pos + (speed * front);
		}
		else if (CHECK_BIT(keysPressed, S)) {
			pos = pos - (speed * front);
		}
		else if (CHECK_BIT(keysPressed, A)) {
			pos = pos - (glm::normalize(glm::cross(front, up)) * speed);
		}
		else if (CHECK_BIT(keysPressed, D)) {
			pos = pos + (glm::normalize(glm::cross(front, up)) * speed);
		} 
		else if (CHECK_BIT(keysPressed, I)) {
			front = glm::normalize(front + glm::normalize(front + up) * viewSpeed);
		}
		else if (CHECK_BIT(keysPressed, K)) {
			front = glm::normalize(front + glm::normalize(front - up) * viewSpeed);
		}
		else if (CHECK_BIT(keysPressed, J)) {
			front = front - glm::normalize(glm::cross(front, up)) * viewSpeed;
		}
		else if (CHECK_BIT(keysPressed, L)) {
			cout << "L pressed!" << endl;
			front = front + glm::normalize(glm::cross(front, up)) * viewSpeed;
		}
		else if (CHECK_BIT(keysPressed, Y)) {
			pos = pos + (glm::vec3(0.0, 1.0, 0.0) * liftSpeed);
		}
		else if (CHECK_BIT(keysPressed, H)) {
			pos = pos - (glm::vec3(0.0, 1.0, 0.0) * liftSpeed);
		}
	}

	void SwapMode() {
		mode = mode ? Walking : BirdsEye;
		if (mode == Walking) {
			pos = glm::vec3(0.0f, 1.0f, 10.0f);
			front = glm::vec3(0, 0, -1.0);
		}
		else {
			pos = glm::vec3(-4.0f, 5.0f, 18.0f);
			front = glm::vec3(0.2, -0.4, -1.0); // bird eye angle vector
		}
	}

};

struct Object
{
	unsigned int vao;
	unsigned int textureID;
	glm::mat4 model;
	glm::mat4 mv;
	Material material;
	bool useTexture;
	glm::vec4 fallbackColour = glm::vec4(0.8, 0.4, 0.2, 1.0);
	vector<glm::vec3> vertices;
	vector<glm::vec3> normals;
	vector<glm::vec2> uvs;
};

//--------------------------------------------------------------------------------
// Variables
//--------------------------------------------------------------------------------

GLuint shaderID;
GLuint basicShaderID;

Object* objs = new Object[amountOfObjects];

GLuint uniform_mv;
GLuint uniform_apply_texture;
GLuint uniform_material_ambient;
GLuint uniform_material_diffuse;
GLuint uniform_material_specular;
GLuint uniform_material_power;

GLuint uniform_basic_mv;
GLuint uniform_basic_fallback_colour;

LightSource light;

glm::mat4 view, proj;

Camera camera;

//--------------------------------------------------------------------------------
// Misc. functions
//--------------------------------------------------------------------------------
void UpdateObjsMV() {
	for (int i = 0; i < amountOfObjects; i++) {
		objs[i].mv = view * objs[i].model;
	}
}

//--------------------------------------------------------------------------------
// Keyboard handling
//--------------------------------------------------------------------------------

void KeyDown(unsigned char key, int a, int b)
{
	if (key == 27)
		glutExit();

	if (key == 'c') {
		camera.SwapMode();
		cout << "Camera changed to new mode: " << (camera.mode ? "BirdsEye" : "Walking") << endl;
	}

	switch (key) {
		case 'w':
			keysPressed |= (1 << W);
			break;
		case 's':
			keysPressed |= (1 << S);
			break;
		case 'a':
			keysPressed |= (1 << A);
			break;
		case 'd':
			keysPressed |= (1 << D);
			break;
		case 'i':
			keysPressed |= (1 << I);
			break;
		case 'k':
			keysPressed |= (1 << K);
			break;
		case 'j':
			keysPressed |= (1 << J);
			break;
		case 'l':
			keysPressed |= (1 << L);
			break;
		case 'y':
			keysPressed |= (1 << Y);
			break;
		case 'h':
			keysPressed |= (1 << H);
			break;
	}
}

void KeyRelease(unsigned char key, int a, int b) {
	switch (key) {
	case 'w':
		keysPressed &= ~(1 << W);
		break;
	case 's':
		keysPressed &= ~(1 << S);
		break;
	case 'a':
		keysPressed &= ~(1 << A);
		break;
	case 'd':
		keysPressed &= ~(1 << D);
		break;
	case 'i':
		keysPressed &= ~(1 << I);
		break;
	case 'k':
		keysPressed &= ~(1 << K);
		break;
	case 'j':
		keysPressed &= ~(1 << J);
		break;
	case 'l':
		keysPressed &= ~(1 << L);
		break;
	case 'y':
		keysPressed &= ~(1 << Y);
		break;
	case 'h':
		keysPressed &= ~(1 << H);
		break;
	}
}

//--------------------------------------------------------------------------------
// Rendering
//--------------------------------------------------------------------------------
void Render()
{
    glClearColor(0.0, 0.0, 0.0, 1.0);
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	for (int i = 0; i < amountOfObjects; i++) {
		objs[i].mv = view * objs[i].model;

		if (objs[i].useTexture) 
		{
			glUniform1i(uniform_apply_texture, 1);
			glBindTexture(GL_TEXTURE_2D, objs[i].textureID);
		}
		else
		{
			glUniform1i(uniform_apply_texture, 0);
		}

		glUniformMatrix4fv(uniform_mv, 1, GL_FALSE, glm::value_ptr(objs[i].mv));
		glUniform3fv(uniform_material_ambient, 1, glm::value_ptr(objs[i].material.ambientColor));
		glUniform3fv(uniform_material_diffuse, 1, glm::value_ptr(objs[i].material.diffuseColor));
		glUniform3fv(uniform_material_specular, 1, glm::value_ptr(objs[i].material.specular));
		glUniform1f(uniform_material_power, objs[i].material.power);

		if (i == 0) { /*house*/
			// Swap shader program before and after rendering the house and set the colour uniform.
			GLCall(glUseProgram(basicShaderID));
			GLCall(glUniform4fv(uniform_basic_fallback_colour, 1, glm::value_ptr(objs[i].fallbackColour)));
			GLCall(glUniform4fv(uniform_basic_mv, 1, glm::value_ptr(objs[i].fallbackColour)));
			GLCall(glUniformMatrix4fv(uniform_basic_mv, 1, GL_FALSE, glm::value_ptr(objs[i].mv)));
			GLCall(glBindVertexArray(objs[i].vao));
			GLCall(glDrawArrays(GL_TRIANGLES, 0, objs[i].vertices.size()));
			GLCall(glBindVertexArray(0));
			GLCall(glUseProgram(shaderID));
		}
		else {
			glBindVertexArray(objs[i].vao);
			glDrawArrays(GL_TRIANGLES, 0, objs[i].vertices.size());
			glBindVertexArray(0);
		}
	}
    glutSwapBuffers();
}



//------------------------------------------------------------
// void Render(int n)
// Render method that is called by the timer function
//------------------------------------------------------------

void Render(int n)
{
	/* Update camera position */
	camera.UpdatePosition();

	/* Update view */
	view = camera.GetView();

	/* Model view matrix of each object with the new view change  */
	UpdateObjsMV();

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

    glutSetOption(GLUT_MULTISAMPLE, 8);
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA | GLUT_DEPTH);
    glutInitWindowSize(WIDTH, HEIGHT);
    glutCreateWindow("FinalAssignment-Tim-1097561");
    glutDisplayFunc(Render);
    glutTimerFunc(DELTA, Render, 0);

	glutKeyboardFunc(KeyDown);
	glutKeyboardUpFunc(KeyRelease);

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

	char * basicFragShader = glsl::readFile(basicFragShader_name);
	GLuint basic_fshID = glsl::makeFragmentShader(basicFragShader);

	char * basicVertexshader = glsl::readFile(basicVertexShader_name);
	GLuint basic_vshID = glsl::makeVertexShader(basicVertexshader);

    shaderID = glsl::makeShaderProgram(vshID, fshID);
	basicShaderID = glsl::makeShaderProgram(basic_vshID, basic_fshID);
}


//------------------------------------------------------------
// void InitMatrices()
//------------------------------------------------------------

void InitMatrices()
{
	objs[0].model = glm::scale(glm::rotate(glm::translate(glm::mat4(), glm::vec3(0.0, 0.0, 6.0)), (float) (8.0f / M_PI), glm::vec3(0, 1, 0)), glm::vec3(1.5, 1.5, 1.5));
	objs[1].model = glm::scale(glm::translate(glm::mat4(), glm::vec3(3.0, 0.0, 0.0)), glm::vec3(1, 1, 1));
	objs[2].model = glm::scale(glm::translate(glm::mat4(), glm::vec3(-3.0, 0.0, 0.0)), glm::vec3(1, 1, 1));
	objs[3].model = glm::scale(glm::rotate(glm::translate(glm::mat4(), glm::vec3(-6.0, 0.65, 3.0)), 30.0f, glm::vec3(1, 1, 1)), glm::vec3(1, 1, 1));
	objs[4].model = glm::scale(glm::translate(glm::mat4(), glm::vec3(-1.0, 0.0, 2.0)), glm::vec3(1, 1, 1));
	objs[5].model = glm::scale(glm::translate(glm::mat4(), glm::vec3(0.0, 0.0, 0.0)), glm::vec3(2, 1, 2));

	view = camera.GetView();
	proj = glm::perspective(
		glm::radians(45.0f),	// fov
		1.0f * WIDTH / HEIGHT,	// aspect ratio
		0.1f,					// near clip plane
		200.0f);				// far clip plane

	UpdateObjsMV();
}

//------------------------------------------------------------
// void InitObjects()
//------------------------------------------------------------

void InitObjects()
{
	// Models
    loadOBJ("Objects/teapot.obj", objs[0].vertices, objs[0].uvs, objs[0].normals);
    loadOBJ("Objects/box.obj", objs[1].vertices, objs[1].uvs, objs[1].normals);
	loadOBJ("Objects/MyHouse.obj", objs[2].vertices, objs[2].uvs, objs[2].normals);
	loadOBJ("Objects/MyCup.obj", objs[3].vertices, objs[3].uvs, objs[3].normals);
	loadOBJ("Objects/MyLampPost.obj", objs[4].vertices, objs[4].uvs, objs[4].normals);
	loadOBJ("Objects/MyPlane.obj", objs[5].vertices, objs[5].uvs, objs[5].normals);

	// Textures
	objs[0].textureID = -1;
    objs[1].textureID = loadBMP("Textures/uvtemplate.bmp");
	objs[2].textureID = loadBMP("Textures/Yellobrk.bmp");
	objs[3].textureID = objs[2].textureID;
	objs[4].textureID = loadBMP("Textures/floor.bmp");
	objs[5].textureID = objs[2].textureID;
}


//------------------------------------------------------------
// void InitMaterialsLight()
//------------------------------------------------------------

void InitMaterialsLight()
{
    light.position = glm::vec3(-1.0, 5.0, 2.0);

	for (int i = 0; i < amountOfObjects; i++) {
		objs[i].material.ambientColor = glm::vec3(1.0, 1.0, 1.0);
		objs[i].material.diffuseColor = glm::vec3(1.0, 1.0, 1.0);
		objs[i].material.specular = glm::vec3(1.0);
		objs[i].material.power = 128;
		objs[i].useTexture = objs[i].textureID > 0 ? true : false;
	}

	objs[0].material.ambientColor = glm::vec3(0.0, 1.0, 0.0);
	objs[0].material.diffuseColor = glm::vec3(0.0, 0.0, 1.0);
	objs[0].material.specular = glm::vec3(0.8);
	objs[0].material.power = 128;
	objs[0].fallbackColour = glm::vec4(0.08, 0.33, 0.86, 0.4);

	objs[4].material.ambientColor = glm::vec3(0.0, 1.0, 1.0);
	objs[4].material.diffuseColor = glm::vec3(0.5, 0.5, 0.5);
	objs[4].material.specular = glm::vec3(1.0);
	objs[4].material.power = 1;
	objs[4].useTexture = true;
}


//------------------------------------------------------------
// void InitBuffers()
// Allocates and fills buffers
//------------------------------------------------------------

void InitBuffers()
{
	GLuint vbo_vertices, vbo_normals, vbo_uvs;

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

	// Swap Program for basic shader uniforms
	glUseProgram(basicShaderID);
	GLuint uniform_basic_proj = glGetUniformLocation(basicShaderID, "projection");
	glUniformMatrix4fv(uniform_basic_proj, 1, GL_FALSE, glm::value_ptr(proj));
	uniform_basic_fallback_colour = glGetUniformLocation(basicShaderID, "u_Colour");
	uniform_basic_mv = glGetUniformLocation(basicShaderID, "mv");
	glUniformMatrix4fv(uniform_basic_proj, 1, GL_FALSE, glm::value_ptr(proj));
	glUseProgram(shaderID);

    // Fill uniform vars
    glUniformMatrix4fv(uniform_proj, 1, GL_FALSE, glm::value_ptr(proj));
    glUniform3fv(uniform_light_pos, 1, glm::value_ptr(light.position));

    for (int i = 0; i < amountOfObjects; i++)
    {
        // vbo for vertices
        glGenBuffers(1, &vbo_vertices);
        glBindBuffer(GL_ARRAY_BUFFER, vbo_vertices);

        glBufferData(GL_ARRAY_BUFFER, objs[i].vertices.size() * sizeof(glm::vec3),
            &objs[i].vertices[0], GL_STATIC_DRAW);
        glBindBuffer(GL_ARRAY_BUFFER, 0);

        // vbo for normals
        glGenBuffers(1, &vbo_normals);
        glBindBuffer(GL_ARRAY_BUFFER, vbo_normals);
        glBufferData(GL_ARRAY_BUFFER, objs[i].normals.size() * sizeof(glm::vec3),
            &objs[i].normals[0], GL_STATIC_DRAW);
        glBindBuffer(GL_ARRAY_BUFFER, 0);

        // vbo for uvs
        glGenBuffers(1, &vbo_uvs);
        glBindBuffer(GL_ARRAY_BUFFER, vbo_uvs);
        glBufferData(GL_ARRAY_BUFFER, objs[i].uvs.size() * sizeof(glm::vec2),
            &objs[i].uvs[0], GL_STATIC_DRAW);
        glBindBuffer(GL_ARRAY_BUFFER, 0);

        glGenVertexArrays(1, &(objs[i].vao));
        glBindVertexArray(objs[i].vao);

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
