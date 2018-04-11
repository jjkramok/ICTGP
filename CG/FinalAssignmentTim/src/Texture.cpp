//
// Created by Tim on 2018-04-05.
//

#include "Texture.h"

#include "vendor/stb_images/stb_image.h"
#include "vendor/texture.h"

Texture::Texture(const std::string& path)
    : m_RendererID(0), m_FilePath(path), m_LocalBuffer(nullptr),
      m_Width(0), m_Height(0), m_BPP(0) {

}

Texture::~Texture() {
    GLCall(glDeleteTextures(1, &m_RendererID));
}

Texture *Texture::CreatePNG(const std::string &path) {
    Texture *texture = new Texture(path);

    stbi_set_flip_vertically_on_load(1);
    texture->m_LocalBuffer = stbi_load(path.c_str(), &(texture->m_Width), &(texture->m_Height), &(texture->m_BPP), 4);

    GLCall(glGenTextures(1, &(texture->m_RendererID)));
    texture->Bind();

    GLCall(glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR));
    GLCall(glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR));
    GLCall(glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE)); // tiling / clamp to edge
    GLCall(glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE));

    GLCall(glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA8, texture->m_Width, texture->m_Height, 0, GL_RGBA, GL_UNSIGNED_BYTE, texture->m_LocalBuffer));
    texture->Unbind();

    // Clears the texture data from the ram
    if (texture->m_LocalBuffer != nullptr)
        stbi_image_free(texture->m_LocalBuffer);
    return texture;
}

Texture *Texture::CreateBMP(const std::string &path) {
    Texture *texture = new Texture(path);

    texture->m_RendererID = loadBMP(path.c_str());
    texture->Unbind();

    return texture;
}

void Texture::Bind(unsigned int slot) const {
    GLCall(glActiveTexture(GL_TEXTURE0 + slot));
    GLCall(glBindTexture(GL_TEXTURE_2D, m_RendererID));
}
void Texture::Unbind() const {
    GLCall(glBindTexture(GL_TEXTURE_2D, 0));
}