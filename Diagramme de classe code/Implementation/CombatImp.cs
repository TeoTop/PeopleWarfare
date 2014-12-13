using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public class CombatImp : Combat
    {
        /**
         * @var UniteImp uniteDef
         */ 
        public UniteImp uniteDef
        {
            get;
            set;
        }

        /**
         * @var UniteImp uniteAtt
         */
        public UniteImp uniteAtt
        {
            get;
            set;
        }

        public bool move { get; set; }

        /**
         * Constructor
         * @param Unite uniteAtt
         * @param List<Unite> unitesDef
         */
        public CombatImp(UniteImp uniteAtt, List<Unite> unitesDef)
        {
            this.uniteAtt = uniteAtt;
            this.move = unitesDef.Count - 1 == 0;
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
         * Return
         * CBT_DRAW : if it's a draw
         * CBT_VICTORY_NOMOVE if it's a victory
         * CBT_VICTORY_MOVE if it's a victory and the units can move
         * CBT_LOSS if it's a loss
         * @return EnumMoveBattle
         */
        public EnumBattle effectuerCombat()
        {
            int nbCombat = calculerNbCombat();
            while (nbCombat > 0)
            {
                // revoir cette méthode
                if (successAtt())
                {
                    uniteDef.vie -= 1;
                    if (uniteDef.vie <= 0)
                    {
                        // check if there's no other unit in the box so as to move on
                        if (this.move)
                        {
                            return EnumBattle.CBT_VICTORY_MOVE;
                        }
                        return EnumBattle.CBT_VICTORY_NOMOVE;
                    }
                }
                else
                {
                    uniteAtt.vie -= 1;
                    if (uniteAtt.vie <= 0)
                    {
                        return EnumBattle.CBT_LOSS;
                    }
                }
            }
            return EnumBattle.CBT_DRAW;
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
