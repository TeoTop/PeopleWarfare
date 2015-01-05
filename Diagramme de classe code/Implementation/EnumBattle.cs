using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public enum EnumBattle
    {
        CBT_LOSS,
        CBT_VICTORY_MOVE,
        CBT_VICTORY_NOMOVE,
        CBT_DRAW_VICTORY,
        CBT_DRAW_LOSS,
        CBT_DRAW,
    }
}
