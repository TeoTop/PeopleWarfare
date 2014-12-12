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
         * True if movement it's done.
         * @param int c
         * @param Double pm
         * @return bool
         */
        bool seDeplacer(int c, Double pm);

        /**
         * Lets fighting unity against enemy unit.
         * @param Unite uniteAdv
         * @return int
         */
        int combattre(Unite uniteAdv);

        /**
         * Shows whether the unit can reach the box c.
         * Number of pm required to move or 0 if it's impossible
         * @param int cInit
         * @param int c
         * @param EnumPeuple
         * @param Carte carte
         * @param Peuple adv
         * @return Double
         */
        Double verifierDeplacement(int cInit, int c, EnumPeuple type, Carte carte, Peuple adv);

        /**
         * Reset caracteristics of an unit
         * @return void
         */
        void reset();
    }
}
