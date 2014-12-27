#include <time.h>
#include <stdlib.h>
#include "algos.h"
#include <math.h>

int* Algos::generer_carte(int nbCase, int nbTypeCase) {
	srand(time(NULL));
	int* nb = (int*)malloc(nbTypeCase * sizeof(int));
	int* cases = (int*)malloc(nbCase * sizeof(int));
	for (int i = 0; i < nbTypeCase; i++)
	{
		nb[i] = 0;
	}
	bool drap = false;
	for (int i = 0, typeCase = 0; i < nbCase; i++)
	{
		drap = false;
		while (!drap)
		{
			typeCase = rand() % nbTypeCase;
			if (nb[typeCase] < nbCase / nbTypeCase)
			{
				nb[typeCase] += 1;
				drap = true;
			}
		}
		cases[i] = typeCase;
	}
	return cases;
}
int** Algos::suggestion_cases(int** cases, int taille, int* posEnnemi, int posActuelle, int typeUnite) {
	int** cases_sucg;
	int* distanceInit;
	int nbCaseDispo = sizeof(cases) / sizeof(*cases);
	int nbCaseDispoCourante = nbCaseDispo;
	int nbEnnemi = sizeof(posEnnemi) / sizeof(*posEnnemi);
	int x, y, a, b, dist;
	int typeCaseBon, typeCaseMauvais, pertePoint;


	x = posActuelle / taille;
	y = posActuelle % taille;
	distanceInit = (int*)malloc(sizeof(int)* nbEnnemi);

	for (int i = 0; i < nbEnnemi; i++){
		a = posEnnemi[i] / taille;
		b = posEnnemi[i] % taille;
		distanceInit[i] = abs(a - x) + abs(b - y);
	}

	for (int i = 0; i < nbCaseDispo; i++){
		a = cases[i][0] / taille;
		b = cases[i][0] % taille;

		for (int j = 0; j < nbEnnemi; j++){
			dist = abs(a - x) + abs(b - y);
			if (distanceInit[j] >= dist){
				cases[i][0] = -1;
				--nbCaseDispoCourante;
			}
		}
	}

	switch (typeUnite)
	{
	case 0:
		pertePoint = 1;
		typeCaseMauvais = 1;
		typeCaseBon = 2;
		break;
	case 1:
		pertePoint = 0;
		typeCaseMauvais = -1;
		typeCaseBon = 1;
		break;
	case 2:
		pertePoint = 1;
		typeCaseMauvais = 2;
		typeCaseBon = 3;
		break;
	}

	if (pertePoint){
		for (int i = 0; i < nbCaseDispo; i++){
			if (cases[i][1] == typeCaseMauvais){
				cases[i][0] = -1;
				--nbCaseDispoCourante;
			}
		}
	}
	

	for (int i = 0; i < nbCaseDispo && nbCaseDispoCourante > 3; i++){
		if (cases[i][1] != typeCaseBon){
			cases[i][0] = -1;
			--nbCaseDispoCourante;
		}
	}

	cases_sucg = (int**)malloc(sizeof(*cases) * nbCaseDispoCourante);
	
	for (int i = 0; i < nbCaseDispo; i++){
		if (cases[i][0] >= 0){
			cases_sucg[i][0] = cases[i][0];			//n° de case
			cases_sucg[i][1] = cases[i][1];		   //type de case
			cases_sucg[i][2] = cases[i][2];		  //deplacement ou attaque
		}
	}

	return cases_sucg;
}



int** Algos::cases_atteignable(int* cases, int* posEnnemi, int posActuelle, int typeUnite, int pm){
	int taille_dispo = 1;
	int nbCase = sqrt( (double)(sizeof(cases) / sizeof(*cases)) );
	int cases_possible[6];
	int **case_dispo = NULL;

	if (pm == 0){
		return case_dispo;
	}
	
	cases_possible[0] = posActuelle - nbCase;
	cases_possible[1] = posActuelle + nbCase;
	cases_possible[2] = posActuelle + 1;
	cases_possible[3] = posActuelle - 1;

	if ((posActuelle/nbCase) % 2 == 1)
	{
		cases_possible[4] = posActuelle - (nbCase - 1);
		cases_possible[5] = posActuelle + (nbCase + 1);
	}
	else
	{
		cases_possible[4] = posActuelle + (nbCase - 1);
		cases_possible[5] = posActuelle - (nbCase + 1);
	}

	switch (typeUnite)
	{
		case 0:
			if (pm != 1)
			{
				for (int i = 0; i < 6; i++){
					if (cases[i] != 2){
						cases_possible[i] = -1;
					}
				}
			}
			break;
		case 1:
			for (int i = 0; i < 6; i++){
				if ((pm != 1 && cases[i] != 1) || cases[i] == 3){
					cases_possible[i] = -1;
				}
			}
			break;
		case 2:
			if (pm != 1)
			{
				for (int i = 0; i < 6; i++){
					if (cases[i] != 2){
						cases_possible[i] = -1;
					}
				}
			}
			if (cases[posActuelle] == 3){
				for (int i = 0; i < nbCase; ++i){
					if (cases[i] == 3){
						for (int j = 0; j < sizeof(posEnnemi) / sizeof(int); j++){
							if (i == posEnnemi[j]){
								cases_possible[i] = -1;
							}
						}
					}
				}
			}
			break;
	}
	
	for (int i = 0; i < 6; i++){
		if (cases_possible[i] >= 0){
			case_dispo = (int**)realloc(case_dispo, taille_dispo * sizeof(int*));
			case_dispo[taille_dispo - 1] = (int*)malloc(3 * sizeof(int));
			case_dispo[taille_dispo - 1][0] = cases_possible[i];			//n° de case
			case_dispo[taille_dispo - 1][1] = cases[cases_possible[i]];		//type de case
			case_dispo[taille_dispo - 1][2] = 0;							//deplacement ou attaque
			for (int j = 0; j < sizeof(posEnnemi) / sizeof(*posEnnemi); j++){
				if (cases_possible[i] == posEnnemi[j]){
					case_dispo[taille_dispo - 1][2] = 1;
				}
			}
		}
	}

	return case_dispo;
}


int* Algos::placer_joueurs(int nbCase) {
	int placement[2];
	srand(time(NULL));
	if (rand() % 2) {
		placement[0] = 0;
		placement[1] = nbCase - 1;
	}
	else {
		placement[0] = sqrt((double)nbCase);
		placement[1] = nbCase - (placement[0] + 1);
	}
	return placement;
}


EXTERNC DLL Algos* Algos_New() {
	return new Algos();
}
EXTERNC DLL void Algos_Delete(Algos* algos) {
	delete algos;
}
/*EXTERNC DLL int* Gen_GenCarte(Algos* gen, int nbCase, int nbTypeCase) {
	return gen->generer_carte(nbCase, nbTypeCase);
}*/