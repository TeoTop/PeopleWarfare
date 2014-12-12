using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public abstract class PeupleA : Peuple
    {
        public int uniteActuel { get; set; }

        public List<UniteImp> unites { get; set; }

        public Unite getUniteActuel()
        {
            return unites[uniteActuel];
        }

        public Unite uniteSuivante()
        {
            return getUnite((++uniteActuel)%unites.Count);
        }

        public Unite getUnite(int key)
        {
            return unites[key];
        }

        public abstract string getInformation();

        public List<Unite> verifierUnite(int c)
        {
            List<Unite> u = new List<Unite>();

            foreach (UniteImp unite in unites)
            {
                if (unite.c == c) u.Add(unite);
            }

            return u;
        }

        public int getNbUnite()
        {
            return unites.Count;
        }

        public void creerUnites(int nbUnite, int posu)
        {
            //on instancie la liste d'unités
            unites = new List<UniteImp>();

            //on boucle dur le nombre d'unité pour les instancier et les ajouter à la liste
            for (int i = 0; i < nbUnite; i++)
            {
                unites.Add(new UniteImp(posu));
            }
        }

        public abstract EnumPeuple getType();
    }
}
