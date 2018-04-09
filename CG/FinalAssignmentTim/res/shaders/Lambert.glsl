#shader vertex
#version 330

// Uniform matrices
uniform mat4 mv;
uniform mat4 projection;
uniform vec3 lightPos;

// Per-vertex inputs
in vec3 position;
in vec3 normal;

out VS_OUT
{
   vec3 N;
   vec3 L;
   //vec3 V;
} vs_out;


void main()
{
    // Calculate view-space coordinate
    vec4 P = mv * vec4(position, 1.0);

    // Calculate normal in view-space
    vs_out.N = mat3(mv) * normal;

    // Calculate light vector
    vs_out.L = lightPos - P.xyz;

    // Calculate view vector;
    // vs_out.V = -P.xyz;

    // Calculate the clip-space position of each vertex
    gl_Position = projection * P;
}

#shader fragment
#version 330

// Input from vertex shader
in VS_OUT
{
    vec3 N;
    vec3 L;
    //vec3 V;
} fs_in;

// Material properties
uniform vec3 matAmbient;
uniform vec3 matDiffuse;

void main()
{
    // Normalize the incoming N, L and V vectors
    vec3 N = normalize(fs_in.N);
    vec3 L = normalize(fs_in.L);
    //vec3 V = normalize(fs_in.V);

    // Calculate R locally
    //vec3 R = reflect(-L, N);

    // Compute the diffuse and specular components for each fragment
    vec3 diffuse = max(dot(N, L), 0.0) * matDiffuse;
    // vec3 specular = pow(max(dot(R, V), 0.0), matPower) * matSpecular;

    // Write final color to the framebuffer
    gl_FragColor = vec4(matAmbient + diffuse, 1.0);
}
