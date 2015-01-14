using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleWar
{
    public class Move
    {
        public Double pm { get; set; }
        public bool hasPlayed { get; set; }
        public EnumMove mv { get; set; }
        public List<Unite> unites { get; set; }

        public Move()
        {
            pm = 0;
            mv = EnumMove.NOMOVE;
            hasPlayed = false;
            unites = new List<Unite>();
        }
    }
}
