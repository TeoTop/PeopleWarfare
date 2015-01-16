using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Golem : PeupleA
    {
        /**
         * Default names of unit
         * @var string[] noms
         */
        public readonly string[] noms = { "Golem1", "Golem2", "Golem3", "Golem4", "Golem5", "Golem6", "Golem7", "Golem8" };

        /**
         * Golem Constructor
         * @param int nbUnite
         * @param int posu
         */
        public Golem(int nbUnite, int posu)
        {
            // on crée les unités du peuple en fonction de nbUnite (méthode dans PeupleA)
            skin = 0;
            creerUnites(nbUnite, posu, noms);
        }

        public override string getInformation()
        {
            return "Race : " + this.GetType().Name + 
                "\nBonus :" +
                " - Un Golem gagne un point de plus lorsqu'il est sur une case Marais." +
                "\n\t  - Les golems peuvent se déplacer deux foix par tour. Mais ils ne" +
                "\n\t\t pourront pas se déplacer au prochain tour s'ils le font." +
                "\n\t- Malus : Un Golem ne peut pas se déplacer sur une case Mer.";
        }

        public override EnumPeuple getType()
        {
            return EnumPeuple.GOLEM;
        }
    }
}
