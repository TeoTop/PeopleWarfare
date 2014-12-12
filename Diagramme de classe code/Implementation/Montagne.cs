using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public class Montagne : CaseA
    {
        public Montagne()
        {
        }
        public override String ToString()
        {
            return "Montagne";
        }

        public override EnumCase getType()
        {
            return EnumCase.MONTAGNE;
        }
    }
}
