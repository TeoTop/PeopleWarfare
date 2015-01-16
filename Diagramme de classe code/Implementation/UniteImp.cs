using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class UniteImp : Unite
    {
        /**
         * Unit's position
         * @var int pos
         */
        public int pos { get; set; }

        /**
         * Unit's attack
         * @var int attaque
         */
        public int attaque { get; set; }

        /**
         * Unit's defense
         * @var int defense
         */
        public int defense { get; set; }

        /**
         * Unit's life
         * @var int vie
         */
        public int vie { get; set; }

        /**
         * Unit's movement point
         * @var int pm
         */
        public Double pm { get; set; }

        /**
         * Unit's point
         * @var int point
         */
        public int point { get; set; }

        /**
         * Unit's name
         * @var String nom
         */
        public String nom { get; set; }

        /**
         * UniteImp Constructor
         * @param int posu
         * @param String nom
         */
        public UniteImp(int posu, String nom)
        {
            //les statistiques de base sont définies par le jeu
            attaque = 2;
            defense = 1;
            vie = 5;
            pm = 1;
            point = 1;
            pos = posu;
            this.nom = nom;
        }

        
        /**
         * UniteImp Constructor
         */
        public UniteImp() { }

        public string afficherCaracteristique()
        {
            return "Unite :\n\t- Attaque : " + attaque + "\n\t- Defense : " + defense + "\n\t- Vie : " + vie +
                "\n\t- Point de mouvement : " + pm + "\n\t- Point rapporté : " + point;
        }

        public float getAttEff()
        {
            return attaque * vie / 5.0F;
        }

        public float getDefEff()
        {
            return defense * vie / 5.0F;
        }
        public Move seDeplacer(int cInit, int pos, EnumPeuple type, Carte carte, Peuple adv)
        {
            MoveImp move = (MoveImp)verifierDeplacement(cInit, pos, type, carte, adv);
            this.pm -= move.pm;
            if (move.mv == EnumMove.MOVE)
            {
                //se deplacer
                this.move(pos);
            }
            return move;
        }

        public void move(int pos)
        {
            this.pos = pos;
        }
        public Move verifierDeplacement(int cInit, int c, EnumPeuple type, Carte carte, Peuple adv)
        {
            MoveImp move = new MoveImp();

            if (pm <= 0) return move;

            EnumCase box = carte.getCase(c).getType();
            if (type == EnumPeuple.ELF && box == EnumCase.DESERT) return move;
            if (type == EnumPeuple.GOLEM && box == EnumCase.MER) return move;


            List<int> casesDispo = new List<int>();
            int taille = (int)Math.Sqrt(((StrategieCarte)carte).cases.Count);

            casesDispo.Add(cInit - taille);
            casesDispo.Add(cInit + taille);

            if ((cInit % taille) != 0)
            {
                casesDispo.Add(cInit - 1);
            }
            if ((cInit % taille) != taille - 1)
            {
                casesDispo.Add(cInit + 1);
            }

            if ((cInit / taille) % 2 == 0)
            {
                if ((cInit % taille) != 0)
                {
                    casesDispo.Add(cInit - (taille + 1));
                    casesDispo.Add(cInit + (taille - 1));
                }
            }
            else
            {
                if ((cInit % taille) != taille - 1)
                {
                    casesDispo.Add(cInit + (taille + 1));
                    casesDispo.Add(cInit - (taille - 1));
                }
            }
            

            if (casesDispo.Contains(c))
            {
                move.pm = 1;
                move.hasPlayed = true;
                move.mv = EnumMove.MOVE; 

                if ((type == EnumPeuple.ELF && box == EnumCase.FORET) ||
                    (type == EnumPeuple.ORC && box == EnumCase.PLAINE) || 
                    (type == EnumPeuple.NAIN && box == EnumCase.PLAINE) ||
                    (type == EnumPeuple.CHEVALIER && box == EnumCase.MER) ||
                    type == EnumPeuple.GOLEM)

                {

                    move.pm = 0.5;

                    move.hasPlayed = pm - move.pm <= 0;

                    move.mv = EnumMove.MOVE;

                }

            }

            // move on Montagne box for a Nain.
            if (type == EnumPeuple.NAIN && carte.getCase(cInit).getType() == EnumCase.MONTAGNE &&
                carte.getCase(c).getType() == EnumCase.MONTAGNE && !casesDispo.Contains(c))
            {

                move.pm = 1;
                move.hasPlayed = true;
                move.mv = EnumMove.MOVE; 

                for (int i = 0; i < ((PeupleA)adv).unites.Count; i++)
                    if (((PeupleA)adv).unites[i].pos == c)
                        move.pm = 2;
                
            }





            List<Unite> unitesDef;

            if ((unitesDef = adv.verifierUnite(c)).Any())
            {
                if (move.mv == EnumMove.MOVE)
                {
                    //cbt
                    move.mv = EnumMove.CBT;
                    move.unites = unitesDef;
                }
            }

            if (move.pm > pm)
            {
                move.pm = 0;
                move.hasPlayed = false;
                move.mv = EnumMove.NOMOVE;
            }

            return move;
        }

        public bool survive(EnumPeuple type)
        {
            if (type == EnumPeuple.ELF)
            {
                Random rnd = new Random();
                if (rnd.Next(0, 2) == 0)
                {
                    this.vie = 1;
                    return true;
                }
            }
            return false;
        }
        public void reset(EnumPeuple type)
        {
            if (type == EnumPeuple.GOLEM && pm == 0)
            {
                pm = -1;
            }
            else
            {
                pm = 1;
            }
        }
    }
}
