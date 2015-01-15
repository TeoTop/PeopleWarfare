#ifndef ALGOS_H
#define ALGOS_H
using namespace std;

class Algos
{
public:
	int* generer_carte(int nbCase, int nbTypeCase);
	int* placer_joueurs(int nbCase);
	int** suggestion_cases(int** cases, int nbCase, int taille, int* posEnnemi, int nbEnn, int posActuelle, int typeUnite);
	int** cases_atteignable(int* cases, int nbCase, int* posEnnemi, int nbEnn, int posActuelle, int typeUnite, double pm);
private:

};

#endif
