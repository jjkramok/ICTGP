#ifndef FILEIO_H
#define FILEIO_H

#include <iostream>
#include <stdio.h>
#include <string>
#include <stdlib.h>
#include <fstream>

class fileio
{
public:
	fileio();
	void read();
	void write();

	virtual ~fileio();
};


#endif // !FILEIO_H