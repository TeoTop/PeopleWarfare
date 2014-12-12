using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace PeopleWar
{
    public abstract class StrategieCarte : Carte
    {

        public List<CaseA> cases { get; set; }

        public abstract EnumCarte type { get; set; }

        public abstract int nbCase { get; set; }

        /**
         * Creates a map which contains the same number of each box
         */
        unsafe public void creerCarte()
        {
            /*Random rnd = new Random();
            
            int[] nb = new int[4] {0,0,0,0};

            bool drap = false;
            for (int i = 0, typeCase = 0; i < nbCase; i++)
            {
                drap = false;
                while (!drap)
                {
                    typeCase = rnd.Next(0, 4);
                    if (nb[typeCase] < nbCase / 4)
                    {
                        nb[typeCase] += 1;
                        drap = true;
                    }
                }
                cases.Add((CaseA)FabriqueCase.INSTANCE.getCase((EnumCase)typeCase));
            }*/
            WrapperAlgos w = new WrapperAlgos();
            int* cases_wrapper = w.generer_carte(nbCase, Enum.GetNames(typeof(EnumCase)).Length);
            for (int i = 0; i < nbCase; i++)
            {
                cases.Add((CaseA)FabriqueCase.INSTANCE.getCase((EnumCase)cases_wrapper[i]));
            }
        }


        /**
         * Return the box which has the key 'key'
         * If the key is not valid, it returns -1
         * @param int key
         * @return Case
         */
        public Case getCase(int key)
        {
            if (isValidkey(key))
            {
                return cases[key];
            }
            return null;
        }

        /**
         * Return the key of the couple (x,y)
         * @param int x
         * @param int y
         * @return int
         */
        public int getKey(int x, int y)
        {
            return (int) (x * Math.Sqrt(nbCase) + y);
        }

        /**
         * Return the abscissa of the box
         * If the key is not valid, it returns -1
         * @param int key
         * @return int
         */
        public int getX(int key)
        {
            // key / sqrt(nbCase)
            if (isValidkey(key))
            {
                return (int) Math.Floor(key / Math.Sqrt(nbCase));
            }
            return -1;
        }

        /**
         * Return the ordinate of the box
         * If the key is not valid, it returns -1
         * @param int key
         * @return int
         */
        public int getY(int key)
        {
            // key % sqrt(nbCase)
            if (isValidkey(key))
            {
                return (int)(key % Math.Sqrt(nbCase));
            }
            return -1;
        }

        /**
         * Check whether a key is valid or not
         * @param int key
         * @return Boolean
         */
        public Boolean isValidkey(int key)
        {
            return (key >= 0 && key < nbCase);
        }

        /**
         * Overrides ToString
         * @return String
         */
        public override String ToString()
        {
            String s = "";
            foreach(CaseA c in cases) {
                s += c.ToString() + '\n';
            }
            return s;
        }
    }
}
