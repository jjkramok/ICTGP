#include "fileio.h"



fileio::fileio()
{
	
}

void fileio::read()
{
	std::cout << "read file:" << std::endl;
	std::string line;

	std::ifstream myfile;
	myfile.open("sample.txt");

	while (getline(myfile, line))
	{
		std::cout << line << std::endl;
	}

	myfile.close();
	std::cout << "read file done." << std::endl;
}

void fileio::write()
{
	std::cout << "write to file" << std::endl;
	std::ofstream myfile;
	myfile.open("sample.txt");
	for (int i = 0; i < 5; i++) {
		myfile << "Writing sample to a file: " << i << std::endl;
	}
	myfile.close();
	std::cout << "wrote to file" << std::endl;
}


fileio::~fileio()
{
}
