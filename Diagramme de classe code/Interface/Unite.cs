using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public interface Unite
    {
        /**
         * Displays the current characteristics of a unit.
         * @return string
         */
        string afficherCaracteristique();

        /**
         * Returns the attack function of life.
         * @return float
         */
        float getAttEff();

        /**
         * Returns the defense function of life.
         * @return float key
         */
        float getDefEff();

        /**
         * Modifies the box associated with the unit. 
         * @param int cInit
         * @param int c
         * @param EnumPeuple type
         * @param Carte carte
         * @param Peuple adv
         * @return Move
         */
        Move seDeplacer(int cInit, int c, EnumPeuple type, Carte carte, Peuple adv);
        /**
         * Shows whether the unit can reach the box c.
         * Number of pm required to move or 0 if it's impossible
         * @param int cInit
         * @param int c
         * @param EnumPeuple type
         * @param Carte carte
         * @param Peuple adv
         * @return Move
         */

        void move(int c);
        Move verifierDeplacement(int cInit, int c, EnumPeuple type, Carte carte, Peuple adv);

        /**
         * Reset caracteristics of an unit
         * @param EnumPeuple type
         * @return void
         */
        void reset(EnumPeuple type);
        /*
         * Return true if the unit survive, false otherwise
         * @param EnumPeuple type
         * @return bool
         */
        bool survive(EnumPeuple type);
    }
}
