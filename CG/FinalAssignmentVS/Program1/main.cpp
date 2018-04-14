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
#include "MyMacros.h"

using namespace std;

//--------------------------------------------------------------------------------
// Consts
//--------------------------------------------------------------------------------

const char * fragshader_name = "fragmentshader.fsh";
const char * vertexshader_name = "vertexshader.vsh";
const int WIDTH = 800, HEIGHT = 600;
const int DELTA = 10;
unsigned int amountOfObjects = 2;

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

char keysPressed; // Keeps track of keys that remain pressed during render cycles.
enum MyKeys
{
	W = 0, S = 1, A = 2, D = 3, I, K, J, L, // keysPressed layout
};

struct Camera 
{
	glm::vec3 pos = glm::vec3(0, 0, 10.0);
	glm::vec3 front = glm::vec3(0, 0, -1.0);
	glm::vec3 up = glm::vec3(0.0, 1.0f, 0);
	ViewMode mode = Walking;
	float speed = 0.1f;
	float viewSpeed = 0.01f;

	glm::mat4 GetView() 
	{
		return glm::lookAt(pos, pos + front, up);
	}
	//gluLookAt(camera[0], camera[1], camera[2], /* look from camera XYZ */ 0, 0, 0, /* look at the origin */ 0, 1, 0); /* positive Y up vector */ 
	//glRotatef(orbitDegrees, 0.f, 1.f, 0.f);/* orbit the Y axis */ /* ...where orbitDegrees is derived from mouse motion */ 
	//glCallList(SCENE); /* draw the scene */
	

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
			front = front + glm::normalize(front + up) * viewSpeed; // can't use up vector, since it always points up (and not orthogonal to the front vect)
		}
		else if (CHECK_BIT(keysPressed, K)) {
			front = front + glm::normalize(front - up) * viewSpeed;
		}
		else if (CHECK_BIT(keysPressed, J)) {
			front = front - glm::normalize(glm::cross(front, up)) * viewSpeed;
		}
		else if (CHECK_BIT(keysPressed, L)) {
			front = front + glm::normalize(glm::cross(front, up)) * viewSpeed; // first speed then normal? Or other way around
		}
	}

	void SwapMode() {
		mode = mode ? Walking : BirdsEye;
		if (mode == Walking) {
			pos = glm::vec3(0.0f, 0.0f, 10.0f);
			front = glm::vec3(0, 0, -1.0);
		}
		else {
			pos = glm::vec3(0.0f, 5.0f, 10.0f);
			front = glm::vec3(0, -0.4, -1.0); // bird eye angle vector
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
	vector<glm::vec3> vertices;
	vector<glm::vec3> normals;
	vector<glm::vec2> uvs;
};

//--------------------------------------------------------------------------------
// Variables
//--------------------------------------------------------------------------------

GLuint shaderID;

Object* objs = new Object[amountOfObjects];

GLuint uniform_mv;
GLuint uniform_apply_texture;
GLuint uniform_material_ambient;
GLuint uniform_material_diffuse;
GLuint uniform_material_specular;
GLuint uniform_material_power;

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

		glBindVertexArray(objs[i].vao);
		glDrawArrays(GL_TRIANGLES, 0, objs[i].vertices.size());
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

    shaderID = glsl::makeShaderProgram(vshID, fshID);
}


//------------------------------------------------------------
// void InitMatrices()
//------------------------------------------------------------

void InitMatrices()
{
	objs[0].model = glm::scale(glm::translate(glm::mat4(), glm::vec3(0.0, 0.0, 0.0)), glm::vec3(1, 1, 1));
	objs[1].model = glm::scale(glm::translate(glm::mat4(), glm::vec3(3.0, 0.5, 0.0)), glm::vec3(1, 1, 1));

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

	// Textures
    objs[0].textureID = loadBMP("Textures/Yellobrk.bmp");
    objs[1].textureID = loadBMP("Textures/uvtemplate.bmp");
}


//------------------------------------------------------------
// void InitMaterialsLight()
//------------------------------------------------------------

void InitMaterialsLight()
{
    light.position = glm::vec3(4.0, 4.0, 4.0);

	for (int i = 0; i < amountOfObjects; i++) {
		objs[i].material.ambientColor = glm::vec3(1.0, 1.0, 1.0);
		objs[i].material.diffuseColor = glm::vec3(1.0, 1.0, 1.0);
		objs[i].material.specular = glm::vec3(1.0);
		objs[i].material.power = 128;
		objs[i].useTexture = true;
	}
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
