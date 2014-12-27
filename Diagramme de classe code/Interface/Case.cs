using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public interface Case
    {
        /**
         * Return the type of the box
         * return EnumCase
         */
        EnumCase getType();
    }
}
