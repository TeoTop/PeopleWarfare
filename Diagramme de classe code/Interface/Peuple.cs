using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public interface Peuple
    {
        /**
        * Return the current unit of the list.
        * @return Unite
        */
        Unite getUniteActuel();

        /**
        * Return the next unit of the list.
        * @return Unite
        */
        Unite uniteSuivante();

        /**
        * Return unit on the key's position in the list.
        * @return Unite
        */
        Unite getUnite(int key);

        /**
         * Return information on the people(number of units, bonus, malus).
         * @return String
         */
        String getInformation();

        /**
         * Shows whether there are units on the box c.
         * Return the list of units on the box
         * @param int c
         * @return List<Unite>
         */
        List<Unite> verifierUnite(int c);

        int getNbUnite();

        /**
         * People's units are created depending on unit number.
         * @param int nbUnite
         * @return void
         */
        void creerUnites(int nbUnite, int posu);

        /**
         * Return the type of the people
         * @return EnumPeuple
         */
        EnumPeuple getType();

        /*
         * Destroy an unit
         * @param Unite
         * @return void
         */
        void destroy(Unite uniteImp);
    }
}
