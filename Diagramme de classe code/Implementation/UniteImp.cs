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
            //les statistiques de base sont définie par le jeu
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
        public int seDeplacer(int cInit, int c, EnumPeuple type, Carte carte, Peuple adv)
        {
            Move move = verifierDeplacement(cInit, c, type, carte, adv);
            if (!move.move)
            {
                if (!move.hasPlayed)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                //se deplacer
                this.c = c;
                return 2;
            }
        }

        public int combattre(Unite uniteAdv)
        {
            return 1;
        }

        public Move verifierDeplacement(int cInit, int c, EnumPeuple type, Carte carte, Peuple adv)
        {
            // ajouter une enum MoveCbt et enlver la classe Move
            Double pmToRemove = 0;
            Move move = new Move();

            if (pm <= 0) return move;


            if (type == EnumPeuple.ELF && carte.getCase(c).getType() == EnumCase.DESERT) return move;

            if (((type == EnumPeuple.ELF && carte.getCase(c).getType() != EnumCase.FORET) ||
                (type == EnumPeuple.ORC && carte.getCase(c).getType() != EnumCase.PLAINE)) && pm > 0.5)
            {
                pmToRemove = 0.5;
                move.move = true;
            }

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


            // move on Montagne box for a Nain.
            if (type == EnumPeuple.NAIN && carte.getCase(cInit).getType() == EnumCase.MONTAGNE &&
                carte.getCase(c).getType() == EnumCase.MONTAGNE)
            {
                pmToRemove = 1;
                move.move = true;
            }


            if (casesDispo.Contains(c))
            {
                pmToRemove = 1;
                move.move = true;
            }


            List<Unite> uniteDef;

            if ((uniteDef = adv.verifierUnite(c)).Any())
            {
                //cbt
                CombatImp cbt = new CombatImp(this, uniteDef);
                int resCbt = cbt.effectuerCombat();

                if (resCbt == 1 || resCbt == 2)
                {
                    // destroy the other unit
                    if (resCbt == 1)
                    {
                        // do not move
                        move.move = false;
                    }
                }
                else if (resCbt == 0)
                {
                    move.move = false;
                }
                else if (resCbt == -1)
                {
                    // destroy unit
                    move.move = false;
                }
                move.hasPlayed = true;
            }

            this.pm -= pmToRemove;
            return move;
        }

        public void destroy()
        {

        }
        public void reset()
        {
            pm = 1;
        }
    }
}
