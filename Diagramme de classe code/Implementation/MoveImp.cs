using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleWar
{
    [Serializable]
    public class MoveImp : Move
    {
        /**
         * Movement point used to move the unit
         * @var Double pm
         */
        public Double pm { get; set; }

        /**
         * True if the unit hasPlayed, false otherwise
         * @var bool hasPlayed
         */
        public bool hasPlayed { get; set; }

        /**
         * NOMOVE : unit won't move
         * MOVE : unit will move
         * CBT : unit will fight another unit
         * @var EnumMove mv
         */
        public EnumMove mv { get; set; }

        /**
         * Unit that moves
         * @var UniteImp uniteDep
         */
        public UniteImp uniteDep { get; set; }

        /**
         * Defensive unit that are in the box where the uniteDep
         * want to move
         * @var List<Unite> unites
         */
        public List<Unite> unites { get; set; }

        /**
         * If there's a fight, this property is instaciated
         * @var CombatImp combat
         */
        public CombatImp combat { get; set; }

        /**
         * MoveImp Constructor
         */
        public MoveImp()
        {
            pm = 0;
            mv = EnumMove.NOMOVE;
            hasPlayed = false;
            uniteDep = new UniteImp();
            unites = new List<Unite>();
            combat = null;
        }

        public void combattre(Combat combat)
        {
            this.combat = (CombatImp)combat;
        }
    }
}
