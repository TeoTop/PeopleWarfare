#ifndef __WRAPPER__
#define __WRAPPER__
#include "../Algos/algos.h"
#include <iostream>
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

		int** array2DToPtr(array<int, 2>^ arr, int n){
			int** p3 = new int*[n];
			for (int i = 0; i < n; i++)
			{
				p3[i] = new int[1];
				pin_ptr<int> ptr = &arr[i, 0];
				p3[i] = ptr;
			}
			return p3;
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
					arr[i, j] = ptr[i + 1][j];
				}
			}
			return arr;
		}

	public:
		WrapperAlgos() { algos = Algos_New(); }
		~WrapperAlgos() { Algos_Delete(algos); }
		array<int>^ generer_carte(int nbCase, int nbTypeCase) { return ptrToArray(algos->generer_carte(nbCase, nbTypeCase), nbCase); }
		array<int>^ placer_joueurs(int nbCase) { return ptrToArray(algos->placer_joueurs(nbCase), nbCase); }
		array<int, 2>^ suggestion_cases(array<int, 2>^ cases, int nbCase, int taille, array<int>^ posEnnemi, int nbEnnemi, int posActuelle, int typeUnite) {
			int** p1 = array2DToPtr(cases, nbCase);
			int* p2 = arrayToPtr(posEnnemi);
			int** ptr = algos->suggestion_cases(p1, nbCase, taille, p2, nbEnnemi, posActuelle, typeUnite);
			int ligne = ptr[0][0];
			int col = 3;
			return ptrTo2DArray(ptr, ligne, col);
		}
		array<int, 2>^ cases_atteignable(array<int>^ cases, int nbCase, array<int>^ posEnnemi, int nbEnn, int posActuelle, int typeUnite, double pm) {
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