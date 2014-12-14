﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        public void creerCarte()
        {
            WrapperAlgos w = new WrapperAlgos();
            int[] cases_tmp = w.generer_carte(nbCase, Enum.GetNames(typeof(EnumCase)).Length);

            int[] arr = new int[nbCase];
            int[] enn = new int[4]{23,24,25,26};

            for (int i = 0; i < nbCase; i++)
            {
                cases.Add((CaseA)FabriqueCase.INSTANCE.getCase((EnumCase)cases_tmp[i]));
                arr[i] = cases_tmp[i];
            }

            int[,] dispo = w.cases_atteignable(arr, nbCase, enn, 4, 3, 1, 1);
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
