using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class TourImp : Tour
    {
        /**
         * List of battles
         */
        public List<MoveImp> mouvements
        {
            get;
            set;
        }

        /**
         * Constructor
         */
        public TourImp()
        {
            mouvements = new List<MoveImp>();
        }

       


        /**
         * Return a battle using a key
         * The count begins from 0
         * @param int key
         * @return CombatImp|null
         */
        public Move getMove(int key)
        {
            if (isValidCbt(key))
            {
                return mouvements[key];
            }
            return null;
        }


        /**
         * Return the number of battles
         * @return int
         */
        public int getNbCbt()
        {
            return mouvements.Count();
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
