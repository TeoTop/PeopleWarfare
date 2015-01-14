using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleWar;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace WarFareWPF
{
    public class RoundView
    {
        public Round round { get; set; }
        public String title { get { return round.title; } }
        public UnitView unitAtt { get; set; }
        public UnitView unitDef { get; set; }

        public RoundView()
        {
            this.round = new Round();
        }

        public void setTitle(String message)
        {
            round.title = message;
        }

        public void setStateBatle(EnumBattleAnim battle)
        {
            round.battle = battle;
        }

        public void setStateUnitAtt(UnitView unitAtt)
        {
            //clone
            this.unitAtt = (UnitView)Serialize.DeserializeObject<UnitView>(Serialize.SerializeObject<UnitView>(unitAtt));
            round.uniAtt = this.unitAtt.unit;
        }

        public void setStateUnitDef(UnitView unitDef)
        {
            //clone
            this.unitDef = (UnitView)Serialize.DeserializeObject<UnitView>(Serialize.SerializeObject<UnitView>(unitDef));
            round.unitDef = this.unitDef.unit;
        }
    }
}
