//
// Created by tim on 9-2-18.
//

#include <iostream>
#include <dlfcn.h>
#include "MyStaticLibrary.h"

using namespace std;

int main() {

    cout << Sum(1, 2) << endl;

    // Using .a / .dll files using dlopen()
//    void *handle;
//    int (*sum)(int, int);
//    char* error;
//    handle = dlopen("./libMyStaticLibrary.a", RTLD_LAZY);
//    if (!handle) {
//        fprintf(stderr, "%s\n", dlerror());
//        exit(EXIT_FAILURE);
//    }
//
//    dlerror(); /* Clear any existing error */
//
//    *(void**) (&sum) = dlsym(handle, "Sum");
//    if ((error = dlerror()) != nullptr) {
//        fprintf(stderr, "%s\n", error);
//        exit(EXIT_FAILURE);
//    }
//
//    cout << (*sum)(1, 2) << endl;
//    dlclose(handle);

}