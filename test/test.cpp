// test.cpp : définit le point d'entrée pour l'application console.
//

#include "stdafx.h"
#include "Algos.h"
#include <iostream>
#include <consoleapi.h>
#define NBCASE 4
using std::cout;
using std::cin;

using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	Algos a;
	//int* carte = a.generer_carte(NBCASE, 4);
	//int* carte = new int[NBCASE]{0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3};
	int* carte = new int[NBCASE]{2,1,0,3};
	cout << "ptr : " << carte << " \n";
	for (int j = 0; j < NBCASE; j++)
		cout << " \t" << carte[j] << ",";
	
	cout << "\n\n";

	int* position = a.placer_joueurs(NBCASE);

	cout << "J1 : " << position[0] << "\t J2 : " << position[1] << "\n\n";

	int** cases = a.cases_atteignable(carte, NBCASE, new int[4]{3,3,3,3}, 4, 0, 0, 1);
	int** cases_sug = new int*[(cases[0][0]-1)];
	for (int i = 1; i < cases[0][0]; i++){
		cases_sug[i - 1] = new int[3];
		for (int j = 0; j < 3; j++){
			cout << " \t" << cases[i][j] << ",";
			
			cases_sug[i - 1][j] = cases[i][j];
		}
	}


	cout << "\n\n";
	int n = cases[0][0];
	int nb = sqrt(NBCASE);
	int j = 0;
	int** c = new int*[n - 1];
	for (int i = 1; i < n; i++)
	{
		c[j] = new int[3];
		c[j][0] = cases[i][0];
		c[j][1] = cases[i][1];
		c[j++][2] = cases[i][2];
	}
	int** sug = a.suggestion_cases(cases_sug, n - 1, nb, new int[4]{3, 3, 3, 3}, 4, 0, 0);

	for (int i = 1; i <= sug[0][0]; i++){
		for (int j = 0; j < 3; j++){
			cout << " \t" << sug[i][j] << ",";
		}
	}

	system("pause>nul");
	return 0;
}

