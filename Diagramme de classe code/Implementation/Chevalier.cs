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
            return "Race : " + this.GetType().Name + 
                "\n- Bonus :" +
                " - Le coût de déplacement sur une case Mer est divisé par deux." +
                "\n\t  - L'unité Chevalier rapporte 1 point de bonus supplémentaire si elle" +
                "\n\t\t se trouve sur la même case qu'un autre Chevalier." +
                "\n- Malus : Une unité Chevalier perd un point sur les cases Desert, Montagne" +
                "\n\t\t ou Marais.";
        }

        public override EnumPeuple getType()
        {
            return EnumPeuple.CHEVALIER;
        }
    }
}
