using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleWar
{
    public class Round
    {

        public string title { get; set; }

        public EnumBattleAnim battle { get; set; }

        public UniteImp uniAtt { get; set; }

        public UniteImp unitDef { get; set; }
    }
}
