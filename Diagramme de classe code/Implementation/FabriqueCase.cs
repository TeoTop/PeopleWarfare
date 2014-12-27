using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public class FabriqueCase
    { 
        public static FabriqueCase INSTANCE = new FabriqueCase();

        private FabriqueCase()
        {
            Desert = null;
            Foret = null;
            Montagne = null;
            Plaine = null;
        }

        public Desert Desert { get; set; }

        public Foret Foret { get; set; }

        public Montagne Montagne { get; set; }

        public Plaine Plaine { get; set; }

        /**
         * Create a box from his 'type'
         * Use of Lazy instanciation
         * @param EnumCase type
         * @return Case
         */
        public CaseA getCase(EnumCase type)
        {
            CaseA c = null;
            switch (type)
            {
                case EnumCase.DESERT:
                    if (Desert == null)
                    {
                        Desert = new Desert();
                    }
                    c = Desert;
                    break;
                case EnumCase.FORET:
                    if (Foret == null)
                    {
                        Foret = new Foret();
                    }
                    c = Foret;
                    break;
                case EnumCase.MONTAGNE:
                    if (Montagne == null)
                    {
                        Montagne = new Montagne();
                    }
                    c = Montagne;
                    break;
                case EnumCase.PLAINE:
                    if (Plaine == null)
                    {
                        Plaine = new Plaine();
                    }
                    c = Plaine;
                    break;
            }
            return c;
        }
    }
}
