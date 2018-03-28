#include <iostream>
#include <fstream>
#include <cstring>

using namespace std;

struct Player
{
	int ID;
	char name[32];
	int strength;
	int magic;
	int dexterity;
	int vitality;
	
	Player() {}
	
	Player(int the_ID, const char the_name[], int the_strength, int the_magic, int the_dexterity, int the_vitality)
	{
		ID = the_ID;
		strcpy(name, the_name); // Tim: I did not have strcpy_s(), so I used cstring.h here to use strcpy().
		strength = the_strength;
		magic = the_magic;
		dexterity = the_dexterity;
		vitality = the_vitality;
	}
	
	void print()
	{
		cout << "ID  : " << ID << endl;
		cout << "Name: " << name << endl;
		cout << "STR : " << strength << endl;
		cout << "MAG : " << magic << endl;
		cout << "DEX : " << dexterity << endl;
		cout << "VIT : " << vitality << endl;
	}
};

bool open_output_file(ofstream& fo, char* fname) {
    fo.open(fname, fstream::out);
    return fo.is_open();
}

void write_player(ofstream& file, Player player) {
    file << player.ID << endl;
    file << player.name << endl;
    file << player.strength << endl;
    file << player.magic << endl;
    file << player.vitality << endl;
    file << player.dexterity << endl;
    file.put('c');
    file.close();
}

bool open_input_file(ifstream& fi, char* fname) {
    fi.open(fname, fstream::in);
    return fi.is_open();
}

Player read_player(ifstream& file) {
    char* lineBuffer = new char[100]();
    // unfinshed
    file.close();
}

int main()
{
    ifstream in_file;
    ofstream out_file;

	Player player1(13, "Kees", 1, 2, 3, 4);
	Player player2;

	char filename[] = "player.struct";

	cout << "Player 1 :" << endl;
	player1.print();

    open_output_file(out_file, filename); // own call

	/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
	*   Exercise 3a and 3b test code :                                  *
	\* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

	if (open_output_file(out_file, filename))
		write_player(out_file, player1);

	/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
	*   Exercise 3c and 3d test code :                                  *
	\* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

	if (open_input_file(in_file, filename))
		player2 = read_player(in_file);

	cout << endl << "Player 2 :" << endl;
	player2.print();
	// output:
	// player2 should be the same as player1

	system("PAUSE");
}