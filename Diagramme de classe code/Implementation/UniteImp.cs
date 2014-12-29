using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class UniteImp : Unite
    {
        public int c { get; set; }

        public int attaque { get; set; }

        public int defense { get; set; }

        public int vie { get; set; }

        public int pm { get; set; }

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

        public bool seDeplacer(int c)
        {
            this.c = c;

            return true;
        }

        public int combattre(Unite uniteAdv)
        {
            return 1;
        }

        public bool verifierDeplacement(int cInit, int c, int taille, EnumPeuple type, Carte carte, Peuple adv)
        {
            if (pm == 0) return false;

            if (type == EnumPeuple.ELF && carte.getCase(c).getType() == EnumCase.DESERT) return false;

            if ( ((type == EnumPeuple.ELF && carte.getCase(c).getType() != EnumCase.FORET) ||
                (type == EnumPeuple.ORC && carte.getCase(c).getType() != EnumCase.PLAINE)) && pm < 1)
            {
                return false;
            }

            List<int> casesDispo = new List<int>();

            casesDispo.Add(c - taille);
            casesDispo.Add(c + taille);
            casesDispo.Add(c - 1);
            casesDispo.Add(c + 1);             

            if (carte.getX(c) % 2 == 1)
            {
                casesDispo.Add(c - (taille - 1));
                casesDispo.Add(c + (taille + 1));
            }
            else
            {
                casesDispo.Add(c + (taille - 1));
                casesDispo.Add(c - (taille + 1));
            }

            // move on Montagne box for a Nain.
            if (type == EnumPeuple.NAIN && carte.getCase(cInit).getType() == EnumCase.MONTAGNE && 
                carte.getCase(c).getType() == EnumCase.MONTAGNE && !adv.verifierUnite(c).Any())
            {
                return true;
            }

            if (casesDispo.Contains(cInit))
            {
                return true;
            }

            return false;
        }

        public void destroy()
        {

        }
    }
}
