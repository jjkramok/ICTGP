In this .rar-file you find a folder named '2018.03.27.ICT.GP.CPP'. This has a Visual 
Studio solution. If you are going to use Visual Studio, please use this. If you are 
going to use something other then Visual Studio, you can use the separate main-files 
(one for each exercise) to still have all the same test code.

Tim:
 I made some small changes to the test files, read below:
 Used strcpy instead of strcpy_s, therefore including cstring.h
 Main methods must have return type of int, so I changed void to int for all test files.
 Refractored all 'unsigned char(x)' statements  from Main2 to casts of the same type, it raised narrowing conversion errors.
 RE:Ex.4 Since you cannot change the pointer (you get a copy of it, since the pointer is passed by reference) I choose to overwrite the data behind the pointer.
 I had some problems with writing files, I think it is related to file ownership but I didn't have time to fix this.
