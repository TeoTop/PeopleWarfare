using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class CombatImp : Combat
    {
        /**
         * @var UniteImp uniteDef
         */ 
        public UniteImp uniteDef { get; set; }

        /**
         * @var UniteImp uniteAtt
         */
        public UniteImp uniteAtt { get; set; }

        /**
         * Constructor
         * @param Unite uniteAtt
         * @param List<Unite> unitesDef
         */
        public CombatImp(UniteImp uniteAtt, List<Unite> unitesDef)
        {
            this.uniteAtt = uniteAtt;
            this.uniteDef = (UniteImp) choisirUniteDef(unitesDef);
        }

        /**
         * Calculate the number of battles
         * @return int
         */
        public int calculerNbCombat()
        {
            Random rnd = new Random();
            int max = (uniteAtt.vie > uniteDef.vie) ? uniteAtt.vie : uniteDef.vie;
            return rnd.Next(3, max + 2);
        }

        /**
         * Calculate the success' probability of the attack
         * Between 0 and 1
         * @return float
         */
        public float calculerReussiteAtt()
        {
            float att = 0,
                  def = 0,
                  taux = ((att = uniteAtt.getAttEff()) > (def = uniteDef.getDefEff())) ? (def / 2 * att) : (1 - att / 2 * def);
            return (1 - taux);
        }

        /**
         * Launch a battle
         * Return 0 if it's a draw, 1 if it's a victory, -1 a loss
         * @return int
         */
        public int effectuerCombat()
        {
            int nbCombat = calculerNbCombat();
            while (nbCombat > 0)
            {
                if (successAtt())
                {
                    uniteDef.vie -= 1;
                    if (uniteDef.vie <= 0)
                    {
                        // check if there's no other unit in the box so as to move on
                        return 1;
                    }
                }
                else
                {
                    uniteAtt.vie -= 1;
                    if (uniteAtt.vie <= 0)
                    {
                        return -1;
                    }
                }
            }
            return 0;
        }

        public bool successAtt()
        {
            Random rnd = new Random();
            float rand = rnd.Next(0, 1);
            float reussite = calculerReussiteAtt();
            float[] probability = new float[2] { reussite, 1 - reussite };
            Boolean[] success = new Boolean[2] { true, false };
            float cumul = 0;
            Boolean s = false;
            for (int i = 0; i < 2; i++)
            {
                cumul += probability[i];
                if (rand < cumul)
                {
                    s = success[i];
                    break;
                }
            }
            return s;
        }

        /**
         * Choose the item who will defend the box
         * @param List<Unite> unitesDef
         * @return Unite
         */
        public Unite choisirUniteDef(List<Unite> unitesDef)
        {
            if (unitesDef.Count() == 0) throw new ArgumentNullException();
            Unite uniteDef = null;
            foreach (Unite unite in unitesDef)
            {
                if (uniteDef == null || uniteDef.getDefEff() < unite.getDefEff())
                {
                    uniteDef = unite;
                }
            }
            return uniteDef;
        }
    }
}
