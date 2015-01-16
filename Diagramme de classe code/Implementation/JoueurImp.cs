using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class JoueurImp : Joueur
    {
        /**
         * @var PeupleA peuple
         */
        public PeupleA peuple { get; set; }

        /**
         * Number of point of the player
         * @var int nbPoints
         */
        public int nbPoints { get; set; }
        /**
         * Name of the player
         * @var String nom
         */
        public String nom { get; set; }

        /**
         * JoueurImp constructor
         * @param String n
         * @param PeupleA p
         */
        public JoueurImp(String n, PeupleA p)
        {
            nom = n;
            nbPoints = 0;
            peuple = p;
        }

        public int calculerNbPoint(Carte carte)
        {
            nbPoints = 0;

            if(!(peuple.getType() == EnumPeuple.CHEVALIER)){

                // adds 1 point for each unit still alive
                foreach (UniteImp u in peuple.unites)
                {
                    if (!(peuple.getType() == EnumPeuple.ORC && carte.getCase(u.pos).getType() == EnumCase.FORET) &&
                        !(peuple.getType() == EnumPeuple.NAIN && carte.getCase(u.pos).getType() == EnumCase.PLAINE))
                    {
                        nbPoints += u.point;
                    }

                    if (peuple.getType() == EnumPeuple.GOLEM && carte.getCase(u.pos).getType() == EnumCase.MARAIS)
                    {
                        nbPoints += u.point;
                    }
                }
            
            } else {
                bool[] flag = new bool[peuple.unites.Count];
                for(int i = 0 ; i < peuple.unites.Count; i++) flag[i] = false;

                for(int i = 0 ; i < peuple.unites.Count; i++)
                    for (int j = i+1; j < peuple.unites.Count; j++)
                    {
                        if (peuple.unites[i].pos == peuple.unites[j].pos)
                        {
                            flag[i] = true;
                            flag[j] = true;
                        }
                    }

                for (int i = 0; i < peuple.unites.Count; i++)
                    if (flag[i])
                    {
                        peuple.unites[i].point = 2;
                    }
                    else
                    {
                        peuple.unites[i].point = 1;
                    }

                foreach (UniteImp u in peuple.unites)
                {
                    if (carte.getCase(u.pos).getType() == EnumCase.MARAIS ||
                        carte.getCase(u.pos).getType() == EnumCase.MONTAGNE ||
                        carte.getCase(u.pos).getType() == EnumCase.DESERT)
                    {
                        nbPoints += u.point - 1;
                    }
                    else
                    {
                        nbPoints += u.point;
                    }
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
