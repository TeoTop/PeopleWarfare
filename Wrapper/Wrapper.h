#ifndef __WRAPPER__
#define __WRAPPER__
#include "../Algos/algos.h"
#pragma comment(lib, "../Debug/Algos.lib")

using namespace System;
namespace Wrapper {
	public ref class WrapperAlgos {
	private:
		Algos* algos;
	public:
		WrapperAlgos() { algos = Algos_New(); }
		~WrapperAlgos() { Algos_Delete(algos); }
		int* generer_carte(int nbCase, int nbTypeCase) { return algos->generer_carte(nbCase, nbTypeCase); }
		int* placer_joueurs(int nbCase) { return algos->placer_joueurs(nbCase); }
		int** suggestion_cases(int** cases, int taille, int* posEnnemi, int posActuelle, int typeUnite) { return algos->suggestion_cases(cases, taille, posEnnemi, posActuelle, typeUnite); }
		int** cases_atteignable(int* cases, int* posEnnemi, int posActuelle, int typeUnite, int pm) { return algos->cases_atteignable(cases, posEnnemi, posActuelle, typeUnite, pm); }
	};
}
#endif