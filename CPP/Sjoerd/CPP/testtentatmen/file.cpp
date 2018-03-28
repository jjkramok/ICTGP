#include "file.h"


int readFile(char filename[]) {

	std::ofstream outputFile;
	outputFile.open("output.txt");

	std::ifstream inputFile;
	inputFile.open(filename);
	std::string line;

	int total = 0;

	while (getline(inputFile, line))
	{
		std::cout << line << std::endl;

		for (int i = 0; line[i] != '\0'; i++) {
			if (!(line[i] > '0' && line[i] < '9')) 
			{
				outputFile << line[i];
			}
			else 
			{
				total += line[i] - '0';
			}
		}

		outputFile << std::endl;
	}

	return total;
}

