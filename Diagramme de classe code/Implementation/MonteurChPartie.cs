using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public class MonteurChPartie : PeopleWar.MonteurPartie
    {
        public MonteurChPartie()
        {
        }

        public override StrategieCarte creerCarte(EnumCarte carte)
        {
            throw new System.NotImplementedException();
        }

        public override JoueurImp creerJoueur(string nom, EnumPeuple p, int nbUnite, int posu)
        {
            throw new System.NotImplementedException();
        }
    }
}
