using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public class FabriquePeuple
    {
        // la fabrique est instanciée et peut être appeler par toute les classes.
        public static FabriquePeuple INSTANCE = new FabriquePeuple();

        private FabriquePeuple() {}

        /**
         * Allows you to create the people with its units.
         * @param int p
         * @param int nbUnite
         * @return Peuple
         */
        public PeupleA creerPeuple(EnumPeuple p, int nbUnite, int posu)
        {
            // on instancie le peuple à null pour le return.
            PeupleA peuple = null;

            // crée le peuple en fonction de la race p.
            switch (p)
            {
                case EnumPeuple.ORC:
                    peuple = new Orc(nbUnite, posu);
                    break;
                case EnumPeuple.ELF:
                    peuple = new Elf(nbUnite, posu);
                    break;
                case EnumPeuple.NAIN:
                    peuple = new Nain(nbUnite, posu);
                    break;
                case EnumPeuple.CHEVALIER:
                    peuple = new Chevalier(nbUnite, posu);
                    break;
                case EnumPeuple.GOLEM:
                    peuple = new Golem(nbUnite, posu);
                    break;
                default:
                    Console.WriteLine("Erreur : impossible de créer le peuple, erreur dans le peuple associé");
                    break;
            }

            return peuple;
        }
    }
}
