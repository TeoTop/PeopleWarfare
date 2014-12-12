using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleWar
{
    public class Move
    {
        public bool move { get; set; }
        public bool hasPlayed { get; set; }

        public Move()
        {
            move = false;
            hasPlayed = false;
        }
    }
}
