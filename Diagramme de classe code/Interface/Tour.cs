using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public interface Tour
    {

        Move getMove(int key);

        Boolean isValidCbt(int key);

        int getNbCbt();
    }
}
