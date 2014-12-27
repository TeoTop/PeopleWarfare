// test.cpp : définit le point d'entrée pour l'application console.
//

#include "stdafx.h"
#include "Algos.h"
#include <iostream>
#include <consoleapi.h>
using std::cout;
using std::cin;

using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	Algos a;
	int* carte = a.generer_carte(36, 4);
	cout << "ptr : " << carte << " \n";
	for (int j = 0; j < 36; j++)
		cout << " \t" << carte[j] << ",";
	
	cout << "\n\n";

	int* position = a.placer_joueurs(36);

	cout << "J1 : " << position[0] << "\t J2 : " << position[1] << "\n\n";

	int** cases = a.cases_atteignable(carte, 36, new int[4]{22, 23, 23, 25}, 4, 3, 1, 1);
	int** cases_sug = new int*[(cases[0][0]-1)];
	for (int i = 1; i < cases[0][0]; i++){
		cases_sug[i - 1] = new int[3];
		for (int j = 0; j < 3; j++){
			cout << " \t" << cases[i][j] << ",";
			
			cases_sug[i - 1][j] = cases[i][j];
		}
	}

	cout << "\n\n";

	int** sug = a.suggestion_cases(cases_sug, (cases[0][0]-1), sqrt((double)36), new int[4]{22, 23, 23, 25}, 4, 3, 1);

	for (int i = 1; i < sug[0][0]; i++){
		for (int j = 0; j < 3; j++){
			cout << " \t" << sug[i][j] << ",";
		}
	}

	system("pause>nul");
	return 0;
}

