using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public class UniteImp : Unite
    {
        public int c { get; set; }

        public int attaque { get; set; }

        public int defense { get; set; }

        public int vie { get; set; }
        public Double pm { get; set; }
        public int point { get; set; }
        public UniteImp(int posu)
        {
            //les statistiques de base sont définies par le jeu
            attaque = 2;
            defense = 1;
            vie = 5;
            pm = 1;
            point = 1;
            c = posu;
        }

        public string afficherCaracteristique()
        {
            return "Unite :\n\t- Attaque : " + attaque + "\n\t- Defense : " + defense + "\n\t- Vie : " + vie +
                "\n\t- Point de mouvement : " + pm + "\n\t- Point rapporté : " + point;
        }

        public float getAttEff()
        {
            return attaque * vie / 5;
        }

        public float getDefEff()
        {
            return defense * vie / 5;
        }
        public Move seDeplacer(int cInit, int c, EnumPeuple type, Carte carte, Peuple adv)
        {
            Move move = verifierDeplacement(cInit, c, type, carte, adv);
            this.pm -= move.pm;
            if (move.mv == EnumMove.MOVE)
            {
                //se deplacer
                this.move(c);
            }
            return move;
        }

        public void move(int c)
        {
            this.c = c;
        }
        public Move verifierDeplacement(int cInit, int c, EnumPeuple type, Carte carte, Peuple adv)
        {
            Move move = new Move();

            if (pm <= 0) return move;

            EnumCase box = carte.getCase(c).getType();
            if (type == EnumPeuple.ELF && box == EnumCase.DESERT) return move;


            List<int> casesDispo = new List<int>();
            int taille = (int)Math.Sqrt(((StrategieCarte)carte).cases.Count);

            casesDispo.Add(cInit - taille);
            casesDispo.Add(cInit + taille);
            casesDispo.Add(cInit - 1);
            casesDispo.Add(cInit + 1);

            if (carte.getX(cInit) % 2 == 1)
            {
                casesDispo.Add(cInit - (taille - 1));
                casesDispo.Add(cInit + (taille + 1));
            }
            else
            {
                casesDispo.Add(cInit + (taille - 1));
                casesDispo.Add(cInit - (taille + 1));
            }

            if (casesDispo.Contains(c))
            {
                move.pm = 1;
                move.hasPlayed = true;
                move.mv = EnumMove.MOVE;
            }

            // move on Montagne box for a Nain.
            if (type == EnumPeuple.NAIN && carte.getCase(cInit).getType() == EnumCase.MONTAGNE &&
                carte.getCase(c).getType() == EnumCase.MONTAGNE)
            {
                move.pm = 1;
                move.hasPlayed = true;
                move.mv = EnumMove.MOVE;
            }

            if (((type == EnumPeuple.ELF && box == EnumCase.FORET) ||
                (type == EnumPeuple.ORC && box == EnumCase.PLAINE) || (type == EnumPeuple.NAIN && box == EnumCase.PLAINE)) && pm > 0.5)
            {
                move.pm = 0.5;
                move.hasPlayed = false;
                move.mv = EnumMove.MOVE;
            }

            List<Unite> unitesDef;

            if ((unitesDef = adv.verifierUnite(c)).Any())
            {
                //cbt
                move.mv = EnumMove.CBT;
                move.unites = unitesDef;
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
                if (rnd.Next() == 0)
                {
                    this.vie = 1;
                    return true;
                }
            }
            return false;
        }
        public void reset()
        {
            pm = 1;
        }
    }
}
