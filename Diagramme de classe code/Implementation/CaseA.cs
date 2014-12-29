using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    [Serializable]
    public abstract class CaseA : Case
    {
        public abstract EnumCase getType();
    }
}
