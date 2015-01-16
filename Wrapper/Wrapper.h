#ifndef __WRAPPER__
#define __WRAPPER__
#include "../Algos/algos.h"
#include <iostream>
#pragma comment(lib, "../Debug/Algos.lib")

using namespace System;
namespace Wrapper {
	public ref class WrapperAlgos {
	private:

		/**
		 * In the static library
		 * @var Algos* alogs
		 */
		Algos* algos;

		// Marshalling //
		/**
		 * Convert an object 1 dimension into int*
		 * @param array<int>^ arr
		 * @return int*
		 */
		int* arrayToPtr(array<int>^ arr){
			pin_ptr<int> ptr = &arr[0];
			return ptr;
		}

		/**
		* Convert an object 2 dimensions into int**
		* @param array<int, 2>^ arr
		* @return int**
		*/
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

		/**
		* Convert a pointer into an object 1 dimension
		* @param int* ptr
		* @param int taille
		* @return array<int>^ arr
		*/
		array<int>^ ptrToArray(int* ptr, int taille){
			array<int>^ arr = gcnew array<int>(taille);
			for (int i = 0; i < taille; i++) { arr[i] = ptr[i]; }
			return arr;
		}

		/**
		* Convert a pointer of pointer into an object 2 dimensions
		* @param int** ptr
		* @param int ligne
		* @param int col
		* @return array<int, 2>^ arr
		*/
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
		/**
		 * WrapperAlogs Constructor
		 * Creates a new algos object
		 */
		WrapperAlgos() { algos = Algos_New(); }
		/**
		 * WrapperAlogs Destructor
		 * Destruct the algos object
		 */
		~WrapperAlgos() { Algos_Delete(algos); }

		/**
		 * Generates the map
		 * @param int nbCase : map's number of boxes
		 * @param int nbTypeCase : number of types for the map
		 * @return array<int>^
		 */
		array<int>^ generer_carte(int nbCase, int nbTypeCase) { return ptrToArray(algos->generer_carte(nbCase, nbTypeCase), nbCase); }
		/**
		 * Gives a position to the players so that
		 * they will be as much distant as they can
		 * @param int nbCase : map's number of boxes
		 * @return array<int>^
		 */
		array<int>^ placer_joueurs(int nbCase) { return ptrToArray(algos->placer_joueurs(nbCase), nbCase); }
		/**
		 * Returns the reachable boxes
		 * @param array<int>^ cases : map's boxes
		 * @param int nbCase : number of boxes (lentgh of cases)
		 * @param array<int>^ posEnnemi : position of the ennemies in the map
		 * @param int nbEnn : number of ennemies in the map (length of posEnnemi)
		 * @param int posActuelle : current position of the unit
		 * @param int typeUnite : type of the unit
		 * @param int pm : number of movement's point
		 * @return array<int, 2>^
		 */
		array<int, 2>^ cases_atteignable(array<int>^ cases, int nbCase, array<int>^ posEnnemi, int nbEnn, int posActuelle, int typeUnite, double pm) {
			int* p1 = arrayToPtr(cases);
			int* p2 = arrayToPtr(posEnnemi);
			int** ptr = algos->cases_atteignable(p1, nbCase, p2, nbEnn, posActuelle, typeUnite, pm);
			int ligne = ptr[0][0];
			int col = 3;
			return ptrTo2DArray(ptr, ligne, col);
		}
		/**
		* Returns the best reachable boxes, 3 boxes at most are suggested
		* @param array<int, 2>^ cases : array of array that is returned by cases_atteignable
		* @param int nbCase : lentgh of cases
		* @param int taille : number of boxes in the map
		* @param array<int>^ posEnnemi : position of the ennemies in the map
		* @param int nbEnn : number of ennemies in the map (length of posEnnemi)
		* @param int posActuelle : current position of the unit
		* @param int typeUnite : type of the unit
		* @return array<int, 2>^
		*/
		array<int, 2>^ suggestion_cases(array<int, 2>^ cases, int nbCase, int taille, array<int>^ posEnnemi, int nbEnnemi, int posActuelle, int typeUnite) {
			int** p1 = array2DToPtr(cases, nbCase);
			int* p2 = arrayToPtr(posEnnemi);
			int** ptr = algos->suggestion_cases(p1, nbCase, taille, p2, nbEnnemi, posActuelle, typeUnite);
			int ligne = ptr[0][0];
			int col = 3;
			return ptrTo2DArray(ptr, ligne, col);
		}
	};
}
#endif