#include "Program.h"

const char *FSH = "fragmentshader.fsh";
const char *VSH = "vertexshader.vsh";

GLuint shaderID;

using namespace std;

void Render()
{
	glUseProgram(shaderID);
	glPointSize(40.0f);
	glDrawArrays(GL_POINTS, 0, 1);
	//glBegin(GL_TRIANGLES);


	static const GLfloat blue[] = { 0.0f, 0.0f, 0.4f, 1.0f };
	glClearBufferfv(GL_COLOR, 0, blue);
	glutSwapBuffers();
}

int main(int argc, char ** argv)
{

	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
	glutInitWindowSize(800, 600);
	glutCreateWindow("Hello OpenGL");
	glutDisplayFunc(Render);

	glewInit();

	char *fragshader = glsl::readFile(FSH);
	GLuint fshID = glsl::makeFragmentShader(fragshader);

	char *vertexshader = glsl::readFile(VSH);
	GLuint vshID = glsl::makeVertexShader(vertexshader);

	shaderID = glsl::makeShaderProgram(vshID, fshID);

	HWND hWnd = GetConsoleWindow();
	ShowWindow(hWnd, SW_HIDE);

	glutMainLoop();

	return 0;
}