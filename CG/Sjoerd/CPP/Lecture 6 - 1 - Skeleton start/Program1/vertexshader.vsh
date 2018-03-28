#version 430 core

in vec3 position;
in vec3 color;
uniform mat4 mvp;

out vec3 vColor;

void main()
{
    gl_Position = mvp * vec4(position, 1.0);

    vColor = color;
}