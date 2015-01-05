using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class JoueurImp : Joueur
    {
        public PeupleA peuple { get; set; }

        public int nbPoints { get; set; }

        public String nom { get; set; }

        public JoueurImp(String n, PeupleA p)
        {
            nom = n;
            nbPoints = 0;
            peuple = p;
        }

        public int calculerNbPoint(Carte carte)
        {
            nbPoints = 0;
            // adds 1 point for each unit still alive
            foreach (UniteImp u in peuple.unites)
            {
                if(!(peuple.getType() == EnumPeuple.ORC && carte.getCase(u.pos).getType() == EnumCase.FORET) &&
                    !(peuple.getType() == EnumPeuple.NAIN && carte.getCase(u.pos).getType() == EnumCase.PLAINE))
                {
                    nbPoints += u.point;
                }
            }
            return nbPoints;
        }

        public bool verifierDefaite()
        {
            //we check if the list still contains elements
            if (!peuple.unites.Any())
            {
                // player loses
                return true;
            }

            return false;
        }

        public override String ToString()
        {
            return "Joueur : \n\t- Nom : " + nom + "\n\t- Peuple : " + peuple.GetType().Name + "\n\t- Nombre de points : " + nbPoints.ToString();
        }
    }
}
