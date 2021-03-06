//
// Created by Tim on 2018-04-05.
//

#ifndef FINALASSIGNMENTTIM_TEXTURE_H
#define FINALASSIGNMENTTIM_TEXTURE_H

#include "Renderer.h"
#include <string>

class Texture {
public:
    explicit Texture(const std::string& path);
    ~Texture();

    void Bind(unsigned int slot = 0) const;
    void Unbind() const;

    inline int GetWidth() const { return m_Width; }
    inline int GetHeight() const { return m_Height; }
    inline int GetBPP() const { return m_BPP; }

    static Texture *CreateBMP(const std::string &path);
    static Texture *CreatePNG(const std::string &path);

private:
    unsigned int m_RendererID;
    std::string m_FilePath;
    unsigned char* m_LocalBuffer;
    int m_Width, m_Height, m_BPP;

};


#endif //FINALASSIGNMENTTIM_TEXTURE_H
