using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Golem : PeupleA
    {
        public readonly string[] noms = { "Golem1", "Golem2", "Golem3", "Golem4", "Golem5", "Golem6", "Golem7", "Golem8" };

        public Golem(int nbUnite, int posu)
        {
            // on crée les unités du peuple en fonction de nbUnite (méthode dans PeupleA)
            skin = 0;
            creerUnites(nbUnite, posu, noms);
        }

        public override string getInformation()
        {
            return "Peuple : \n\t- Race : " + this.GetType().Name + "\n\t- Bonus :" +
                "\n\t\t- Un Golem gagne un point de plus lorsqu'il est sur une case marais." +
                "\n\t\t- Les golems sont grands donc ils peuvent parcourir plus de distance. Ainsi, un Golem divise son coup de" +
                " déplacement par deux sur chaque case. Mais comme ils sont vieux, il ne pourront pas se déplacer au prochain " +
                " tour si ils se déplacent deux fois." +
                "\n\t- Malus :\n\t\t- Un Golem ne peut pas se déplacer sur une case mer.";
        }

        public override EnumPeuple getType()
        {
            return EnumPeuple.GOLEM;
        }
    }
}
