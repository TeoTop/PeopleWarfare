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
        public int nbCbtMax { get; set; }
        public int nbCbt { get; set; }
		public EnumBattle resultat { get; set; }
        public List<Round> rounds { get; set; }

        /**
         * Constructor
         * @param Unite uniteAtt
         * @param List<Unite> unitesDef
         */
        public CombatImp(UniteImp uniteAtt, List<Unite> unitesDef)
        {
            this.move = unitesDef.Count - 1 == 0;
            this.uniteAtt = uniteAtt;
            this.uniteDef = (UniteImp)choisirUniteDef(unitesDef);
            this.nbCbtMax = calculerNbCombat();
            this.nbCbt = 0;
			resultat = EnumBattle.CBT_DRAW;
            rounds = new List<Round>();
        }

        /**
         * Adds a battle Round to the list of rounds
         * @param Round round
         * @return void
         */
        public void addRound(Round round)
        {
            rounds.Add(round);
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
            taux = ((att = uniteAtt.getAttEff()) > (def = uniteDef.getDefEff())) ? (def / (2 * att)) : (1 - att / (2 * def));
            return (1 - taux);
        }

        /**
         * Launch a round
         * Return
         * CBT_DRAW_LOSS if the attacker lost the round and did not die
         * CBT_DRAW_VICTORY if the attacker won the round and the defender did not die
         * CBT_VICTORY_NOMOVE if it's a victory
         * CBT_VICTORY_MOVE if it's a victory and the units can move
         * CBT_LOSS if it's a loss
         * @return EnumMoveBattle
       */
        public EnumBattle attaquer()
        {
            if (successAtt())
        {
            uniteDef.vie -= 1;
            if (uniteDef.vie <= 0)
        {
                // check if there's no other unit in the box so as to move on
                if (this.move)
                {
                resultat = EnumBattle.CBT_VICTORY_MOVE;
                return EnumBattle.CBT_VICTORY_MOVE;
            }
                resultat = EnumBattle.CBT_VICTORY_NOMOVE;
                return EnumBattle.CBT_VICTORY_NOMOVE;
            }
                return EnumBattle.CBT_DRAW_VICTORY;
            }
            else
            {
                uniteAtt.vie -= 1;
                if (uniteAtt.vie <= 0)
            {
                resultat = EnumBattle.CBT_LOSS;
                return EnumBattle.CBT_LOSS;
            }
                return EnumBattle.CBT_DRAW_LOSS;
            }
        }






        /**
         * Increment the number of battles
         * @return void
         */
        public void incCbt()
        {
            this.nbCbt++;
        }
        /**
         * Return true wether an attack succeded, false otherwise
         * @return bool
         */
        public bool successAtt()
        {
            Random rnd = new Random();
            float rand = (float)rnd.NextDouble();
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



