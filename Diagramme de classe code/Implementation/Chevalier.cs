using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Chevalier : PeupleA
    {
        /**
         * Default names of unit
         * @var string[] noms
         */
        public readonly string[] noms = { "Chevalier1", "Chevalier2", "Chevalier3", "Chevalier4", "Chevalier5", "Chevalier6", "Chevalier7", "Chevalier8" };

        /**
         * Chevalier Constructor
         * @param int nbUnite
         * @param int posu
         */
        public Chevalier(int nbUnite, int posu)
        {
            // on crée les unités du peuple en fonction de nbUnite (méthode dans PeupleA)
            skin = 0;
            creerUnites(nbUnite, posu, noms);
        }

        public override string getInformation()
        {
            return "Peuple : \n\t- Race : " + this.GetType().Name + "\n\t- Bonus :" +
                "\n\t\t- Le coût de déplacement sur une case Mer est divisé par deux." +
                "\n\t\t- L'unité Chevalier rapporte 1 point de bonus supplémentaire si elle se trouve sur la même case qu'un." +
                " autre Chevalier" +
                "\n\t- Malus :\n\t\t- Une unité Chevalier perd un point sur les cases desert, montagne et marais.";
        }

        public override EnumPeuple getType()
        {
            return EnumPeuple.CHEVALIER;
        }
    }
}
