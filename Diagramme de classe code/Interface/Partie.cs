using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public interface Partie
    {
        /**
         * Return one of the two players
         * key = 0 for Player1 and key = 1 for Player2
         * if key != 0 and key != 1, return null
         * @param key
         * @return Joueur|null
         */
        Joueur getJoueur(int key);

        int getNbTour();

        void selectionnerCase(int key);

        Joueur verifierFinPartie();

        void switcherJoueur();

        Tour getTour(int key);
    }
}
