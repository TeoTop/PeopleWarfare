using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Desert : CaseA
    {
        public Desert() { }

        public override String ToString()
        {
            return "Desert";
        }

        public override EnumCase getType()
        {
            return EnumCase.DESERT;
        }
    }

}
