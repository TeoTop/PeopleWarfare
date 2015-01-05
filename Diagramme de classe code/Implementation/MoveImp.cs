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
        public Double pm { get; set; }
        public bool hasPlayed { get; set; }
        public EnumMove mv { get; set; }
        public List<Unite> unites { get; set; }
        public CombatImp combat { get; set; }

        public MoveImp()
        {
            pm = 0;
            mv = EnumMove.NOMOVE;
            hasPlayed = false;
            unites = new List<Unite>();
            combat = null;
        }

        public void combattre(Combat combat)
        {
            this.combat = (CombatImp)combat;
        }
    }
}
