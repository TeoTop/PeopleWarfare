using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Elf : PeupleA
    {
        /**
         * Default names of unit
         * @var string[] noms
         */
        public readonly string[] noms = { "Elf1", "Elf2", "Elf3", "Elf4", "Elf5", "Elf6", "Elf7", "Elf8" };

        /**
         * Elf Constructor
         * @param int nbUnite
         * @param int posu
         */
        public Elf(int nbUnite, int posu)
        {
            // on crée les unités du peuple en fonction de nbUnite (méthode dans PeupleA)
            skin = 0;
            creerUnites(nbUnite, posu, noms);
        }

        public override string getInformation()
        {
            return "Race : " + this.GetType().Name + 
                "\n- Bonus :" +
                " - Le coût de déplacement sur une case Fôret est divisé par deux." +
                "\n\t  - Si une unité Elf perd un combat et doit mourir, alors a elle 50% de" +
                "\n\t\t chance de survivre avec 1 point de vie." +
                "\n- Malus : Le coût de déplacement sur une case Désert est multiplié par deux.";
        }

        public override EnumPeuple getType()
        {
            return EnumPeuple.ELF;
        }
    }
}
