using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public class FabriqueCase
    {
        /**
         * Singleton
         * @var FabriqueCase INSTANCE
         */
        public static FabriqueCase INSTANCE = new FabriqueCase();

        /**
         * FabriqueCase Constructor
         */
        private FabriqueCase()
        {
            Desert = null;
            Foret = null;
            Montagne = null;
            Plaine = null;
        }

        /**
         * @var Desert Desert
         */
        public Desert Desert { get; set; }


        /**
         * @var Foret Foret
         */
        public Foret Foret { get; set; }


        /**
         * @var Montagne Montagne
         */
        public Montagne Montagne { get; set; }


        /**
         * @var Plaine Plaine
         */
        public Plaine Plaine { get; set; }


        /**
         * @var Mer Mer
         */
        public Mer Mer { get; set; }


        /**
         * @var Marais Marais
         */
        public Marais Marais { get; set; }

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
                case EnumCase.MER:
                    if (Mer == null)
                    {
                        Mer = new Mer();
                    }
                    c = Mer;
                    break;
                case EnumCase.MARAIS:
                    if (Marais == null)
                    {
                        Marais = new Marais();
                    }
                    c = Marais;
                    break;
            }
            return c;
        }
    }
}
