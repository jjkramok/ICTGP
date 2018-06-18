cmake_minimum_required(VERSION 3.9)
project(FinalAssignmentTim)

# Setup verbose output
set(CMAKE_VERBOSE_MAKEFILE OFF)

set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}/bin")

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++14 -static-libgcc -static-libstdc++")
set(CMAKE_MODULE_PATH ${CMAKE_MODULE_PATH} "${CMAKE_SOURCE_DIR}")

# Setup GLFW
set(GLFW_ROOT "${PROJECT_SOURCE_DIR}/dependencies/glfw-3.2.1")
set(GLFW_INCLUDE_DIRS "${GLFW_ROOT}/include")
set(GLFW_LIBRARIES "glfw")
add_subdirectory(${GLFW_ROOT})

# Setup GLM
set(GLM_ROOT "${PROJECT_SOURCE_DIR}/dependencies/glm")
set(GLM_INCLUDE_DIRS "${GLM_ROOT}/glm")

# Setup GLEW
#set(GLEW_ROOT "${PROJECT_SOURCE_DIR}/dependencies/glew-2.1.0")
#set(GLEW_INCLUDE_DIRS "${GLEW_ROOT}/include")
#set(GLEW_INCLUDE_DIR "${GLEW_ROOT}/include")
#link_directories(${GLEW_ROOT}/lib/Release/Win32)
#set(GLEW_LIBRARY "glew32s")
find_package(GLEW REQUIRED)

find_package(PkgConfig)
find_package(OpenGL REQUIRED)

add_executable(${PROJECT_NAME} src/main.cpp src/Renderer.cpp src/Renderer.h src/MyMacros.h src/VertexBuffer.cpp src/VertexBuffer.h src/IndexBuffer.cpp src/IndexBuffer.h src/VertexArray.cpp src/VertexArray.h src/VertexBufferLayout.cpp src/VertexBufferLayout.h src/Shader/Shader.cpp src/Shader/Shader.h src/vendor/stb_images/stb_image.h src/vendor/stb_images/std_image.h.cpp src/Texture.cpp src/Texture.h src/vendor/objloader.cpp src/vendor/objloader.h src/vendor/texture.cpp src/vendor/texture.h src/Shader/LambertShader.cpp src/Shader/LambertShader.h src/Model.cpp src/Model.h src/Object.cpp src/Object.h src/Camera.cpp src/Camera.h src/MyMacros.cpp src/Shader/BasicShader.cpp src/Shader/BasicShader.h)

# Very important to ensure GLEW links staticly.
target_compile_definitions(${PROJECT_NAME} PRIVATE GLEW_STATIC)

include_directories(${OPENGL_INCLUDE_DIR} ${GLFW_INCLUDE_DIRS} ${GLEW_INCLUDE_DIRS} "${PROJECT_SOURCE_DIR}/deps/stb" ${GLM_INCLUDE_DIRS})

target_link_libraries(${PROJECT_NAME} ${GLEW_LIBRARIES} ${GLFW_LIBRARIES} ${OPENGL_LIBRARIES})