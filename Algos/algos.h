#ifndef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

class DLL Algos
{
public:
	Algos() {}
	~Algos() {}
	int* generer_carte(int nbCase, int nbTypeCase);
	int* placer_joueurs(int nbCase);
	int** suggestion_cases(int** cases, int taille,  int* posEnnemi, int posActuelle, int typeUnite);
	int** cases_atteignable(int* cases, int* posEnnemi, int posActuelle, int typeUnite, int pm);
private:

};

EXTERNC DLL Algos* Algos_New();
EXTERNC DLL void Algos_Delete(Algos* gen);
//EXTERNC DLL int* Gen_GenCarte(Algos* gen, int nbCase, int nbTypeCase);
