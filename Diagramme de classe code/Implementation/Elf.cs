using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Elf : PeupleA
    {
        public readonly string[] noms = { "Elf1", "Elf2", "Elf3", "Elf4", "Elf5", "Elf6", "Elf7", "Elf8" };
        public Elf(int nbUnite, int posu)
        {
            // on crée les unités du peuple en fonction de nbUnite (méthode dans PeupleA)
            skin = 0;
            creerUnites(nbUnite, posu, noms);
        }

        public override string getInformation()
        {
            return "Peuple : \n\t- Race : " + this.GetType().Name + "\n\t- Bonus :" +
                "\n\t\t- Le coût de déplacement sur une case Fôret est divisé par deux." +
                "\n\t\t- Si une unité Elf perd un combat et doit mourir, alors elle 50% de chance de survivre avec 1 point de vie" +
                "\n\t- Malus :\n\t\t- Le coût de déplacement sur une case Désert est multiplié par deux.";
        }

        public override EnumPeuple getType()
        {
            return EnumPeuple.ELF;
        }
    }
}
