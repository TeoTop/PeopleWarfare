using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public class Nain : PeupleA
    {
        public Nain(int nbUnite, int posu)
        {
            // on crée les unités du peuple en fonction de nbUnite (méthode dans PeupleA)
            creerUnites(nbUnite, posu);
        }

        public override string getInformation()
        {
            return "Peuple : \n\t- Race : " + this.GetType().Name + "\n\t- Bonus :" +
                "\n\t\t- Le coût de déplacement sur une case Plaine est divisé par deux." +
                "\n\t\t- Si l'unité Nain est sur une case Montagne, elle peut se déplacer sur n'importe quelle autre" +
                " case Montagne à condition qu'il n'y ai pas d'unité adverse." +
                "\n\t- Malus :\n\t\t- Une unité Orc n'acquière aucun point sur la case Plaine.";
        }

        public override EnumPeuple getType()
        {
            return EnumPeuple.NAIN;
        }
    }
}
