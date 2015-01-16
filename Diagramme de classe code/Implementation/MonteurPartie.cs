using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public abstract class MonteurPartie
    {

        /**
         * Create a map from a type 'carte'
         * @param EnumCarte carte
         * @return Carte
         */
        public abstract StrategieCarte creerCarte(EnumCarte carte);

        /**
         * Create a player with his people.
         * @param string nom
         * @param EnumPeuple p
         * @param int nbUnite
         * @return Carte
         */
        public abstract JoueurImp creerJoueur(string nom, EnumPeuple p, int nbUnite, int posu);
    }
}
