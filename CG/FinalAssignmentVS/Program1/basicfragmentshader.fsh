#version 430 core

layout(location = 0) out vec4 color;

uniform vec4 u_Colour;

void main()
{
	color = u_Colour;
    //color = vec4(1.0, 0.0, 0.0, 1.0);
};