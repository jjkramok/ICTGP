#include <iostream>
#include<stdio.h>
#include<stdlib.h>
#include<X11/Xlib.h>
#include <GL/glew.h>
#include <GL/freeglut.h>
#include <GL/glx.h>
#include <GL/glu.h>
#include <glm/glm.hpp>

Display                 *dpy;
Window                  root;
GLint                   att[] = { GLX_RGBA, GLX_DEPTH_SIZE, 24, GLX_DOUBLEBUFFER, None };
XVisualInfo             *vi;
Colormap                cmap;
XSetWindowAttributes    swa;
Window                  win;
GLXContext              glc;
XWindowAttributes       gwa;
XEvent                  xev;

void DrawAQuad();

void Render() {
    static const GLfloat blue[] = {0.0, 0.0, 0.4, 1.0};
    glClearBufferfv(GL_COLOR, 0, blue);
    glutSwapBuffers();
}

//https://www.khronos.org/opengl/wiki/Programming_OpenGL_in_Linux:_GLX_and_Xlib
int main(int argc, char **argv) {
    std::cout << "Hello, World!" << std::endl;

    /*
    // Code below is cross-platform? Code below that is unix specific.
    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH);
    glutInitWindowSize(800, 600);
    glutCreateWindow("Hello OpenGL");
    glutDisplayFunc(Render);
    glewInit();
    */

    // Gain a reference to a display object, this is later used to create a window on.
    dpy = XOpenDisplay(NULL); // NULL is used to connect to local (see X11 / XServer for more info)
    if(dpy == NULL) {
        printf("\n\tcannot connect to X server\n\n");
        exit(0);
    }

    // Gain a reference to the root window, all other windows are in a tree structure beneath the root.
    root = DefaultRootWindow(dpy);

    // Find a visual that matches our situation / is supported by gpu
    vi = glXChooseVisual(dpy, 0, att);

    if(vi == NULL) {
        printf("\n\tno appropriate visual found\n\n");
        exit(0);
    }
    else {
        printf("\n\tvisual %p selected\n", (void *)vi->visualid); /* %p creates hexadecimal output like in glxinfo */
    }

    // Create a colormap for the window
    cmap = XCreateColormap(dpy, root, vi->visual, AllocNone);

    // Initialize XSetWindowAttributes
    swa.colormap = cmap;
    swa.event_mask = ExposureMask | KeyPressMask; // More options available in X11/Xlib.h
    // ^-- here we tell the X server which colormap to use and whereto the window should respond (Exposure and KeyPress events)

    // Finally create the actual window
    win = XCreateWindow(dpy, root, 0, 0, 800, 600, 0, vi->depth, InputOutput, vi->visual, CWColormap | CWEventMask, &swa);

    XMapWindow(dpy, win);
    XStoreName(dpy, win, "Hello OpenGL");

    // create a gl context
    glc = glXCreateContext(dpy, vi, NULL, GL_TRUE);
    glXMakeCurrent(dpy, win, glc);
    glEnable(GL_DEPTH_TEST); // GL_FALSE for network output

    while(1) {
        XNextEvent(dpy, &xev);

        if(xev.type == Expose) {
            XGetWindowAttributes(dpy, win, &gwa);
            glViewport(0, 0, gwa.width, gwa.height);
            DrawAQuad();
            glXSwapBuffers(dpy, win);
        }

        else if(xev.type == KeyPress) {
            glXMakeCurrent(dpy, None, NULL);
            glXDestroyContext(dpy, glc);
            XDestroyWindow(dpy, win);
            XCloseDisplay(dpy);
            exit(0);
        }
    }

    return 0;
}

void DrawAQuad() {
    glClearColor(1.0, 1.0, 1.0, 1.0);
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    glOrtho(-1., 1., -1., 1., 1., 20.);

    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
    gluLookAt(0., 0., 10., 0., 0., 0., 0., 1., 0.);

    glBegin(GL_QUADS);
    glColor3f(1., 0., 0.); glVertex3f(-.75, -.75, 0.);
    glColor3f(0., 1., 0.); glVertex3f( .75, -.75, 0.);
    glColor3f(0., 0., 1.); glVertex3f( .75,  .75, 0.);
    glColor3f(1., 1., 0.); glVertex3f(-.75,  .75, 0.);
    glEnd();
}