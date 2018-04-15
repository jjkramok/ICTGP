#version 430 core

in vec3 position;

uniform mat4 mv;
uniform mat4 projection;

void main()
{
	gl_Position = projection * (mv * vec4(position, 1.0));
};