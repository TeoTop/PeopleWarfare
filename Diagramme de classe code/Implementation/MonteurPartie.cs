using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public abstract class MonteurPartie
    {

        public abstract StrategieCarte creerCarte(EnumCarte carte);

        public abstract JoueurImp creerJoueur(string nom, EnumPeuple p, int nbUnite, int posu);
    }
}
