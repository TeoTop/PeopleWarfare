using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public interface Carte
    {
        void creerCarte();

        Case getCase(int key);

        int getKey(int x, int y);

        int getX(int key);

        int getY(int key);

        Boolean isValidkey(int key);
    }
}
