//
// Created by Tim on 2018-04-01.
//

#ifndef FINALASSIGNMENTTIM_MYMACROS_H
#define FINALASSIGNMENTTIM_MYMACROS_H

/**
 * Defines the dynamic breakpoint function that is supported by the IDE. __debugbreak() is voor VC, __builtin_trap() is for CLion.
 */
#ifdef _MSC_VER
#define DEBUG_BREAK __debugbreak()
#else
#define DEBUG_BREAK __builtin_trap()
#endif

#include <iostream>

/**
 * Basic assert function that toggles a dynamic breakpoint when resulting in false.
 */
#define ASSERT(x) if (!(x)) DEBUG_BREAK

/**
 * Calls function x and logs any GL errors that were thrown to the console
 */
#define GLCall(x) GLClearErrors();\
    x;\
    ASSERT(GLLogCall(#x, __FILE__, __LINE__))

#endif // FINALASSIGNMENTTIM_MYMACROS_H