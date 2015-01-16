using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public class Demo : StrategieCarte
    {
        /**
         * Demo Constructor
         */
        public Demo()
        {
            cases = new List<CaseA>();
            nbCase = 16;
            type = EnumCarte.DEMO;
        }

        public override EnumCarte type { get; set; }

        public override int nbCase { get; set; }
    }
}
