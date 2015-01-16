using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Orc : PeupleA
    {
        /**
         * Default names of unit
         * @var string[] noms
         */
        public readonly string[] noms = { "Orc1", "Orc2", "Orc3", "Orc4", "Orc5", "Orc6", "Orc7", "Orc8" };

        /**
         * Orc Constructor
         * @param int nbUnite
         * @param int posu
         */
        public Orc(int nbUnite, int posu)
        {
            // on crée les unités du peuple en fonction de nbUnite (méthode dans PeupleA)
            skin = 0;
            creerUnites(nbUnite, posu, noms);
        }

        public override string getInformation()
        {
            return "Race : " + this.GetType().Name + 
                "\n- Bonus :" +
                " - Le coût de déplacement sur une case Plaine est divisé par deux." +
                "\n\t  - L'unité rapporte un point de plus pour chaque unité ennemie tuée et" +
                "\n\t\t cela, jusqu'à ce que l'unité meurt." + 
                "\n- Malus : Une unité Orc n'acquière aucun point sur la case Forêt.";
        }

        public override EnumPeuple getType()
        {
            return EnumPeuple.ORC;
        }
    }
}
