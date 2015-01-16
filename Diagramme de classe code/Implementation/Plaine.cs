using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Plaine : CaseA
    {
        /**
         * Plaine Constructor
         */
        public Plaine()
        {
        }
        public override String ToString()
        {
            return "Plaine";
        }

        public override EnumCase getType()
        {
            return EnumCase.PLAINE;
        }
    }
}
