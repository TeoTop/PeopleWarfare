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
		IntPtr generer_carte(int nbCase, int nbTypeCase) { return System::IntPtr(algos->generer_carte(nbCase, nbTypeCase)); }
		IntPtr placer_joueurs(int nbCase) { return System::IntPtr(algos->placer_joueurs(nbCase)); }
		IntPtr suggestion_cases(array<int, 2>^ cases, int taille, array<int>^ posEnnemi, int posActuelle, int typeUnite) {
			pin_ptr<int> p_posEnnemi = &posEnnemi[0];

			pin_ptr<int> c1 = &cases[0,0];
			pin_ptr<int> c2 = &cases[1,0];
			int* c3[2];
			c3[0] = c1;
			c3[1] = c2;
			int** c4 = c3;
			return System::IntPtr(algos->suggestion_cases(c4, taille, p_posEnnemi, posActuelle, typeUnite));
		}
		int** cases_atteignable(int* cases, int* posEnnemi, int posActuelle, int typeUnite, int pm) { return algos->cases_atteignable(cases, posEnnemi, posActuelle, typeUnite, pm); }
	};
}
#endif