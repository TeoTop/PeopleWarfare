using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public interface Combat
    {

        int calculerNbCombat();

        float calculerReussiteAtt();

        Unite choisirUniteDef(List<Unite> UnitesDef);

        bool successAtt();
        
        EnumBattle attaquer();
    }
}
