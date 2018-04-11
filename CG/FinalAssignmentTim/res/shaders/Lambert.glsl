#shader vertex
#version 330

// Uniform matrices
uniform mat4 mv;
uniform mat4 projection;
uniform vec3 lightPos;

// Per-vertex inputs
in vec3 position;
in vec3 normal;
in vec2 uv;

out VS_OUT
{
   vec3 N;
   vec3 L;
   vec3 V;
} vs_out;

out vec2 v_UV;

void main()
{
    // Calculate view-space coordinate
    vec4 P = mv * vec4(position, 1.0);

    // Calculate normal in view-space
    vs_out.N = mat3(mv) * normal;

    // Calculate light vector
    vs_out.L = lightPos - P.xyz;

    // Calculate view vector;
    vs_out.V = -P.xyz;

    // Calculate the clip-space position of each vertex
    gl_Position = projection * P;

    // Pass uvs to the fragment shader.
    v_UV = uv;
}

#shader fragment
#version 330

// Input from vertex shader
in VS_OUT
{
    vec3 N;
    vec3 L;
    vec3 V;
} fs_in;

in vec2 v_UV;

// Material properties
uniform int u_UseTexture;
uniform vec3 matAmbient;
uniform vec3 matDiffuse;
uniform vec3 matSpecular;
uniform float matPower;

layout(location = 0) out vec4 color;
uniform sampler2D u_Texture;

void main()
{
    // Normalize the incoming N, L and V vectors
    vec3 N = normalize(fs_in.N);
    vec3 L = normalize(fs_in.L);
    vec3 V = normalize(fs_in.V);

    // Calculate R locally
    vec3 R = reflect(-L, N);

    // Compute the diffuse and specular components for each fragment
    vec3 ambient, diffuse;
    if (u_UseTexture == 1) {
        // Disable ambient color and use texture data to color pixels
        ambient = vec3(0.0, 0.0, 0.0);
        diffuse = max(dot(N, L), 0.0) * texture2D(u_Texture, v_UV).rgb;
    } else {
        // Use passed colors to color pixels
        ambient = matAmbient;
        diffuse = max(dot(N, L), 0.0) * matDiffuse;
    }
    vec3 specular = pow(max(dot(R, V), 0.0), matPower) * matSpecular;

    // Write final color to the framebuffer
    color = vec4(ambient + diffuse + specular, 1.0);
}