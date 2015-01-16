using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleWar
{
    [Serializable]
    public enum EnumBattleAnim
    {
        DP1,// defender looses 1 pv
        AP1,// attacker looses 1 pv
        AM,// attacker dies
        AS,// attacker survive with 1 pv
        DMAPSP,// defender dies, then the attacker takes his place
        DS,// defender survive with 1 pv
        DMANPSP// defender dies, the attacker doesn't take his place

    }
}
