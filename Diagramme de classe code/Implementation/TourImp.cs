using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public class TourImp : Tour
    {
        /**
         * List of battles
         */
        public List<CombatImp> combats
        {
            get;
            set;
        }

        /**
         * Constructor
         */
        public TourImp()
        {
            combats = new List<CombatImp>();
        }

       


        /**
         * Return a battle using a key
         * The count begins from 0
         * @param int key
         * @return CombatImp|null
         */
        public Combat getCombat(int key)
        {
            if (isValidCbt(key))
            {
                return combats[key];
            }
            return null;
        }


        /**
         * Return the number of battles
         * @return int
         */
        public int getNbCbt()
        {
            return combats.Count();
        }


        /**
         * Check if a battle is valid
         * @param int key
         * @return Boolean
         */
        public bool isValidCbt(int key)
        {
            return (key >= 0 && key < getNbCbt());
        }
    }
}
