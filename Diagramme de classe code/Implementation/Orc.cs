using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Orc : PeupleA
    {
        public readonly string[] noms = { "Orc1", "Orc2", "Orc3", "Orc4", "Orc5", "Orc6", "Orc7", "Orc8" };
               
        public Orc(int nbUnite, int posu)
        {
            // on crée les unités du peuple en fonction de nbUnite (méthode dans PeupleA)
            skin = 0;
            creerUnites(nbUnite, posu, noms);
        }

        public override string getInformation()
        {
            return "Peuple : \n\t- Race : " + this.GetType().Name + "\n\t- Bonus :" +
                "\n\t\t- Le coût de déplacement sur une case Plaine est divisé par deux." +
                "\n\t\t- L'unité Orc rapporte 1 point de bonus supplémentaire par tour pour chaque unité ennemie " +
                "tuée depuis le début de la partie. Lorsque que l'unité Orc meurt le bonus disparait." + 
                "\n\t- Malus :\n\t\t- Une unité Orc n'acquière aucun point sur la case Forêt.";
        }

        public override EnumPeuple getType()
        {
            return EnumPeuple.ORC;
        }
    }
}
