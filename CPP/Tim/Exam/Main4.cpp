#include <iostream>
#include <cstring>

using namespace std;

void clear_string(char* input) {
    // Count the amount of digits.
    int new_length = 0;
    for (int i = 0; input[i] != '\0'; i++) {
        if (!((input[i] > 96 && input[i] < 123) || (input[i] > 64 && input[i] < 91))) {
            new_length++;
        }
    }

    //cout << new_length << endl;
    char* only_digits = new char[new_length]();

    // Build the new string, without the letters.
    int j = 0;
    int i = 0;
    while (input[i] != '\0') {
        if (!((input[i] > 96 && input[i] < 123) || (input[i] > 64 && input[i] < 91))) {
            only_digits[j] = input[i];
            //cout << input[i] << " : " << only_digits[j] << endl;
            j++;
        }
        i++;
    }
    only_digits[j] = '\0'; // Add end of string flag.

    //input = only_digits;
    strcpy(input, only_digits); // Overwrite the original char array.
    //cout << only_digits << endl;
}






int main()
{
	/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
	*   Exercise 4 test code :                                          *
	\* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

	cout << "case 1: " << endl;
	char* case1 = new char[6]{ 'a', '3', '4', 'b', '5', '\0' };
	cout << ">" << case1 << "<" << endl; // output: >a34b5<
	clear_string(case1);
	cout << ">" << case1 << "<" << endl; // output: >345<


	cout << endl << "case 2: " << endl;
	char* case2 = new char[4]{ '1', '2', '3', '\0' };
	cout << ">" << case2 << "<" << endl; // output: >123<
	clear_string(case2);
	cout << ">" << case2 << "<" << endl; // output: >123<


	cout << endl << "case 3: " << endl;
	char* case3 = new char[6]{ '5', 'b', '4', '3', 'a', '\0' };
	cout << ">" << case3 << "<" << endl; // output: >5b43a<
	clear_string(case3);
	cout << ">" << case3 << "<" << endl; // output: >543<


	cout << endl << "case 4: " << endl;
	char* case4 = new char[4]{ 'a', 'b', 'c', '\0' };
	cout << ">" << case4 << "<" << endl; // output: >abc<
	clear_string(case4);
	cout << ">" << case4 << "<" << endl; // output: ><


	system("PAUSE");
}