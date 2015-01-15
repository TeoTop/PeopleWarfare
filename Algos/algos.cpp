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

// nbCase correspond à la taille du tableau
// nbCase correspond à la taille du tableau
int** Algos::suggestion_cases(int** cases, int nbCase, int taille, int* posEnnemi, int nbEnnemi, int posActuelle, int typeUnite) {
	int** distanceInit;
	int nbCaseDispoCourante = nbCase;
	int x, y, a, b, dist;
	int typeCaseBon, typeCaseMauvais, pertePoint;


	x = posActuelle / taille;
	y = posActuelle % taille;
	distanceInit = (int**)malloc(sizeof(int*)* nbEnnemi);

	for (int i = 0; i < nbEnnemi; i++){
		distanceInit[i] = (int*)malloc(sizeof(int)* nbEnnemi);
		distanceInit[i][0] = posEnnemi[i] / taille;
		distanceInit[i][1] = posEnnemi[i] % taille;
		distanceInit[i][2] = abs(distanceInit[i][0] - x + distanceInit[i][1] - y);
	}

	int versEnn;
	for (int i = 0; i < nbCase; i++){
		x = cases[i][0] / taille;
		y = cases[i][0] % taille;
		versEnn = false;

		for (int j = 0; j < nbEnnemi; j++){
			dist = abs(distanceInit[j][0] - x + distanceInit[j][1] - y);
			int d = distanceInit[j][2];
			if (d >= dist){
				versEnn = true;
			}
		}
		if (!versEnn) {
			cases[i][0] = -1;
			nbCaseDispoCourante--;
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
		for (int i = 0; i < nbCase; i++){
			if (cases[i][2] == 0) {
				if (cases[i][1] == typeCaseMauvais && cases[i][0] != -1){
					cases[i][0] = -1;
					--nbCaseDispoCourante;
				}
			}
		}
	}


	for (int i = 0; i < nbCase && nbCaseDispoCourante > 3; i++){
		if (cases[i][2] == 0) {
			if (cases[i][1] != typeCaseBon && cases[i][0] != -1){
				cases[i][0] = -1;
				--nbCaseDispoCourante;
			}
		}
	}

	int** cases_sucg = new int*[nbCaseDispoCourante + 1];
	cases_sucg[0] = new int[1];

	int c = 1;
	cases_sucg[0][0] = nbCaseDispoCourante;
	for (int i = 0; i < nbCase; i++){
		if (cases[i][0] >= 0){
			cases_sucg[c] = new int[3];
			cases_sucg[c][0] = cases[i][0];			//n° de case
			cases_sucg[c][1] = cases[i][1];		   //type de case
			cases_sucg[c++][2] = cases[i][2];		  //deplacement ou attaque
		}
	}

	return cases_sucg;
}



int** Algos::cases_atteignable(int* cases, int nbCase, int* posEnnemi, int nbEnn, int posActuelle, int typeUnite, double pm){
	int taille_dispo = 6;
	int taille = (int)sqrt((double)nbCase);
	int* cases_possible = new int[6];

	if (pm == 0){
		int** a = new int*[1];
		a[0] = new int[1];
		a[0][0] = 0;
		return a;
	}
	
	cases_possible[0] = posActuelle - taille;
	cases_possible[1] = posActuelle + taille;
	cases_possible[2] = posActuelle + 1;
	cases_possible[3] = posActuelle - 1;

	if ((posActuelle / taille) % 2 == 1)
	{
		if ((posActuelle%taille) == taille - 1) {
			cases_possible[4] = -1;
			cases_possible[5] = -1;
		}
		else {
			cases_possible[4] = posActuelle - (taille - 1);
			cases_possible[5] = posActuelle + (taille + 1);
		}
	}
	else
	{
		if ((posActuelle%taille) == 0) {
			cases_possible[4] = -1;
			cases_possible[5] = -1;
		}
		else {
			cases_possible[4] = posActuelle + (taille - 1);
			cases_possible[5] = posActuelle - (taille + 1);
		}
	}
	if ((posActuelle%taille) == 0) {
		cases_possible[3] = -1;
	}
	if ((posActuelle%taille) == taille - 1) {
		cases_possible[2] = -1;
	}

	int c;
	switch (typeUnite)
	{
	case 0:
		if (pm != 1)
		{
			for (int i = 0; i < 6; i++){
				c = cases_possible[i];
				if (cases[c] != 2){
					cases_possible[i] = -1;
				}
			}
		}
		break;
	case 1:
		for (int i = 0; i < 6; i++){
			c = cases_possible[i];
			if ((pm != 1 && cases[c] != 1) || cases[c] == 0){
				cases_possible[i] = -1;
			}
		}
		break;
	case 2:
		if (pm != 1)
		{
			for (int i = 0; i < 6; i++){
				c = cases_possible[i];
				if (cases[c] != 2){
					cases_possible[i] = -1;
				}
			}
		}

		if (cases[posActuelle] == 3){
			for (int i = 0; i < nbCase; ++i){
				if (cases[i] == 3){
					for (int j = 0; j < nbEnn; j++){
						if (i != posEnnemi[j]){
							// on augmmente rajoute une case et on augmente 'lindice de taille
							cases_possible = (int*)realloc(cases_possible, (taille_dispo + 1)*sizeof(int));
							cases_possible[taille_dispo++] = i;
						}
					}
				}
			}
		}
		break;
	}

	int* cases_selec = new int[1];
	int taille_selec = 1;

	//on recree un tableau composer que de case valide
	for (int i = 0; i < taille_dispo; i++){
		if (cases_possible[i] >= 0 && cases_possible[i] < nbCase){           // case ou l'on peut aller
			cases_selec = (int*)realloc(cases_selec, (++taille_selec)*sizeof(int));
			cases_selec[taille_selec - 1] = cases_possible[i];
		}
	}

	//on retourne un tableau[taille_selec][3] + une ligne en zero pour connaitre la taille.
	int** case_dispo = new int*[taille_selec];
	case_dispo[0] = new int[1];
	case_dispo[0][0] = taille_selec-1;
	for (int i = 1; i < taille_selec; i++){
		case_dispo[i] = new int[3];
		case_dispo[i][0] = cases_selec[i];			//n° de case
		case_dispo[i][1] = cases[cases_selec[i]];	//type de case
		case_dispo[i][2] = 0;						//deplacement ou attaque
		for (int j = 0; j < nbEnn; j++){
			if (cases_selec[i] == posEnnemi[j]){
				case_dispo[i][2] = 1;
			}
		}
	}
	
	return case_dispo;
}

int* Algos::placer_joueurs(int nbCase) {
	int* placement = (int*)malloc(2 * sizeof(int));
	srand(time(NULL));
	if (rand() % 2) {
		placement[0] = 0;
		placement[1] = nbCase - 1;
	}
	else {
		placement[0] = (int)sqrt((double)nbCase);
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