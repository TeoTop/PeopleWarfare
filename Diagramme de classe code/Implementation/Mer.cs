using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleWar
{
    [Serializable]
    public class Mer : CaseA
    {
        /**
         * Mer Constructor
         */
        public Mer()
        {
        }
        public override String ToString()
        {
            return "Mer";
        }

        public override EnumCase getType()
        {
            return EnumCase.MER;
        }
    }
}
