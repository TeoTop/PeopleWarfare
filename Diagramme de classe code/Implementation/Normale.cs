using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Normale : StrategieCarte
    {
        public Normale()
        {
            cases = new List<CaseA>();
            nbCase = 196;
            type = EnumCarte.PETITE;
        }

        public override EnumCarte type { get; set; }

        public override int nbCase { get; set; }
    }
}
