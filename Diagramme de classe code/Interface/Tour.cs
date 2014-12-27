using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public interface Tour
    {

        Combat getCombat(int key);
        Boolean isValidCbt(int key);

        int getNbCbt();
    }
}
