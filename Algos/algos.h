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
	/**
	 * Generates the map
	 * @param int nbCase : map's number of boxes
	 * @param int nbTypeCase : number of types for the map
	 * @return int*
	 */
	int* generer_carte(int nbCase, int nbTypeCase);
	/**
	 * Gives a position to the players so that
	 * they will be as much distant as they can
	 * @param int nbCase : map's number of boxes
	 * @return int*
	 */
	int* placer_joueurs(int nbCase);
	/**
	 * Returns the reachable boxes
	 * @param int* cases : map's boxes
	 * @param int nbCase : number of boxes (lentgh of cases)
	 * @param int* posEnnemi : position of the ennemies in the map
	 * @param int nbEnn : number of ennemies in the map (length of posEnnemi)
	 * @param int posActuelle : current position of the unit
	 * @param int typeUnite : type of the unit
	 * @param int pm : number of movement's point
	 * @return int**
	 */
	int** cases_atteignable(int* cases, int nbCase, int* posEnnemi, int nbEnn, int posActuelle, int typeUnite, double pm);
	/**
	 * Returns the best reachable boxes, 3 boxes at most are suggested
	 * @param int** cases : array of array that is returned by cases_atteignable
	 * @param int nbCase : lentgh of cases
	 * @param int taille : number of boxes in the map
	 * @param int* posEnnemi : position of the ennemies in the map
	 * @param int nbEnn : number of ennemies in the map (length of posEnnemi)
	 * @param int posActuelle : current position of the unit
	 * @param int typeUnite : type of the unit
	 * @return int**
	 */
	int** suggestion_cases(int** cases, int nbCase, int taille,  int* posEnnemi, int nbEnn, int posActuelle, int typeUnite);
private:

};

/**
 * Returns new Algos ptr
 * @return Algos*
 */
EXTERNC DLL Algos* Algos_New();

/**
 * Deletes the object gen
 * @param Algos* gen
 * @return void
 */
EXTERNC DLL void Algos_Delete(Algos* gen);