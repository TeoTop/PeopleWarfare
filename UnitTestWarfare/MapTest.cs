using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wrapper;
using PeopleWar;
using System.Collections.Generic;

namespace UnitTestWarfare
{
    [TestClass]
    public class MapTest
    {
        public int[] cases { get; set; }

        /*
         * Create a map with 36 boxes
         * Test whether the number of boxes from each type is equal
         */
        [TestMethod]
        public void CreationMap()
        {
            WrapperAlgos w = new WrapperAlgos();
            cases = w.generer_carte(36, Enum.GetNames(typeof(EnumCase)).Length);
            int nbDesert = 0,
                nbForet = 0,
                nbPlaine = 0,
                nbMontagne = 0;
            foreach (var b in cases)
            {
                switch (b)
                {
                    case (int)EnumCase.DESERT:
                        nbDesert++;
                        break;
                    case (int)EnumCase.FORET:
                        nbForet++;
                        break;
                    case (int)EnumCase.PLAINE:
                        nbPlaine++;
                        break;
                    case (int)EnumCase.MONTAGNE:
                        nbMontagne++;
                        break;
                }
            }
            // same number of boxes
            bool equal = nbDesert == nbForet && nbForet == nbPlaine && nbPlaine == nbMontagne;
            Assert.IsTrue(equal);
        }

        /*
         * Reachable boxes
         * Pos : 0
         * Pos Ennemies : 3 3 3 3
         * Boxes to reach 1 2
         */
        [TestMethod]
        public void AtteignableTest()
        {
            WrapperAlgos w = new WrapperAlgos();
            int[,] att = w.cases_atteignable(new int[4] { 0, 1, 2, 3 }, 4, new int[4] { 3, 3, 3, 3 }, 4, 0, 0, 1);
            List<int[]> latt = new List<int[]>();
            for (int i = 0; i < att.Length / 3; i++)
            {
                int[] c = new int[3] { att[i,0], att[i,1], att[i,2] };
                latt.Add(c);
            }
            bool a = latt.Exists(b => b[0] == 1 && b[1] == 1 && b[2] == 0);
            Assert.IsTrue(a);
            a = latt.Exists(b => b[0] == 2 && b[1] == 2 && b[2] == 0);
            Assert.IsTrue(a);
            Assert.IsTrue(latt.Count == 2);
        }
    }
}
