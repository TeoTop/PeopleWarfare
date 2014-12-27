using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public interface Joueur
    {
        /**
         * Calculats number of points scored by the player at the end of his turn.
         * @return int
         */
        int calculerNbPoint(Carte carte);

        /**
         * Shows whether a player has lost all its units
         * True if he loses
         * @return bool
         */
        bool verifierDefaite();
    }
}
