using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public class Petite : StrategieCarte
    {
        public Petite()
        {
            cases = new List<CaseA>();
            nbCase = 100;
            type = EnumCarte.PETITE;
        }

        public override EnumCarte type { get; set; }

        public override int nbCase { get; set; }
    }
}
