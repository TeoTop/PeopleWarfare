#ifndef __WRAPPER__
#define __WRAPPER__
#include "../Algos/algos.h"
#pragma comment(lib, "../Debug/Algos.lib")

using namespace System;
namespace Wrapper {
	public ref class WrapperAlgos {
	private:
		Algos* algos;
		int* arrayToPtr(array<int>^ arr){
			pin_ptr<int> ptr = &arr[0];
			return ptr;
		}

		int** array2DToPtr(array<int,2>^ arr){
			pin_ptr<int> p1 = &arr[0,0];
			pin_ptr<int> p2 = &arr[1,0];
			int* p3[2];
			p3[0] = p1;
			p3[1] = p2;
			int** ptr = p3;
			return ptr;
		}

		array<int>^ ptrToArray(int* ptr, int taille){
			array<int>^ arr = gcnew array<int>(taille);
			for (int i = 0; i < taille; i++) { arr[i] = ptr[i]; }
			return arr;
		}

		array<int,2>^ ptrTo2DArray(int** ptr, int ligne, int col){
			array<int, 2>^ arr = gcnew array<int, 2>(ligne, col);
			for (int i = 0; i < ligne; i++) {
				for (int j = 0; j < col; j++) {
					arr[i,j] = ptr[i+1][j];
				}
			}
			return arr;
		}

	public:
		WrapperAlgos() { algos = Algos_New(); }
		~WrapperAlgos() { Algos_Delete(algos); }
		array<int>^ generer_carte(int nbCase, int nbTypeCase) { return ptrToArray(algos->generer_carte(nbCase, nbTypeCase), nbCase); }
		array<int>^ placer_joueurs(int nbCase) { return ptrToArray(algos->placer_joueurs(nbCase), nbCase); }
		array<int,2>^ suggestion_cases(array<int, 2>^ cases, int nbCase, int taille, array<int>^ posEnnemi, int nbEnnemi, int posActuelle, int typeUnite) {
			int** ptr = algos->suggestion_cases(array2DToPtr(cases), nbCase, taille, arrayToPtr(posEnnemi), nbEnnemi, posActuelle, typeUnite);
			int ligne = sizeof(ptr) / sizeof(*ptr);
			int col = 3;
			return ptrTo2DArray(ptr, ligne, col);
		}
		array<int, 2>^ cases_atteignable(array<int>^ cases, int nbCase, array<int>^ posEnnemi, int nbEnn, int posActuelle, int typeUnite, int pm) {
			int* p1 = arrayToPtr(cases);
			int* p2 = arrayToPtr(posEnnemi);
			int** ptr = algos->cases_atteignable(p1, nbCase, p2, nbEnn, posActuelle, typeUnite, pm);
			int ligne = ptr[0][0];
			int col = 3;
			return ptrTo2DArray(ptr, ligne, col);
		}
	};
}
#endif