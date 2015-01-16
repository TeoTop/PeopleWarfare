using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleWar
{
    [Serializable]
    public class Marais : CaseA
    {
        /**
         * Marais Constructor
         */
        public Marais()
        {
        }
        public override String ToString()
        {
            return "Marais";
        }

        public override EnumCase getType()
        {
            return EnumCase.MARAIS;
        }
    }
}
