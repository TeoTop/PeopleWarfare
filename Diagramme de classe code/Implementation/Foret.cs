using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Foret : CaseA
    {
        public Foret()
        {
        }
        public override String ToString()
        {
            return "Foret";
        }
        public override EnumCase getType()
        {
            return EnumCase.FORET;
        }
    }
}
