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
        public bool seDeplacer(int c, Double pm)

        {
            this.c = c;
            this.pm -= pm;

            return true;
        }

        public int combattre(Unite uniteAdv)
        {
            return 1;
        }

        public Double verifierDeplacement(int cInit, int c, EnumPeuple type, Carte carte, Peuple adv)
        {
            if (pm <= 0) return 0;

            if (type == EnumPeuple.ELF && carte.getCase(c).getType() == EnumCase.DESERT) return 0;

            if (((type == EnumPeuple.ELF && carte.getCase(c).getType() != EnumCase.FORET) ||
                (type == EnumPeuple.ORC && carte.getCase(c).getType() != EnumCase.PLAINE)) && pm > 0.5)
            {
                return 0.5;
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
                carte.getCase(c).getType() == EnumCase.MONTAGNE && !adv.verifierUnite(c).Any())
            {
                return 1;
            }

            if (casesDispo.Contains(c))
            {
                return 1;
            }

            return 0;
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
