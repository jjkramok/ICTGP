#include <iostream>

using namespace std;


char* clear_string(char *string) {
	int numberCount = 0;
	int i = 0;
	while (string[i] != '\0') {
		if (string[i] >= '0' && string[i] <= '9') {
			numberCount++;
		}
		i++;
	}

	char *result = new char[numberCount + 1];
	int resultCount = 0;
	int j = 0;
	while (string[j] != '\0') {
		if (string[j] >= '0' && string[j] <= '9') {
			result[resultCount] = string[j];
			resultCount++;
		}
		j++;
	}
	result[numberCount] = '\0';

	delete[] string;
	return result;
}



void main()
{
	/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
	*   Exercise 4 test code :                                          *
	\* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

	cout << "case 1: " << endl;
	char* case1 = new char[6]{ 'a', '3', '4', 'b', '5', '\0' };
	cout << ">" << case1 << "<" << endl; // output: >a34b5<
	case1 = clear_string(case1);
	cout << ">" << case1 << "<" << endl; // output: >345<


	cout << endl << "case 2: " << endl;
	char* case2 = new char[4]{ '1', '2', '3', '\0' };
	cout << ">" << case2 << "<" << endl; // output: >123<
	case2 = clear_string(case2);
	cout << ">" << case2 << "<" << endl; // output: >123<


	cout << endl << "case 3: " << endl;
	char* case3 = new char[6]{ '5', 'b', '4', '3', 'a', '\0' };
	cout << ">" << case3 << "<" << endl; // output: >5b43a<
	case3 = clear_string(case3);
	cout << ">" << case3 << "<" << endl; // output: >543<


	cout << endl << "case 4: " << endl;
	char* case4 = new char[4]{ 'a', 'b', 'c', '\0' };
	cout << ">" << case4 << "<" << endl; // output: >abc<
	case4 = clear_string(case4);
	cout << ">" << case4 << "<" << endl; // output: ><


	system("PAUSE");
}