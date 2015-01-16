using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleWar;
namespace WarFareWPF
{
    public class AbstractWindow
    {

        /*
         * @var String j1
         * Player's 1 name
         */
        public static String j1 { get; set; }

        /*
         * @var String j2
         * Player's 2 name
         */
        public static String j2 { get; set; }
        private static EnumPeuple _p1 = EnumPeuple.NULL;

        /*
         * @var EnumPeuple p1
         * Type of player's 1 people
         */
        public static EnumPeuple p1 {
            get
            {
                return _p1;
            }
            set
            {
                _p1 = value;
            }
        }
        private static EnumPeuple _p2 = EnumPeuple.NULL;

        /*
         * @var EnumPeuple p2
         * Type of player's 2 people
         */
        public static EnumPeuple p2
        {
            get
            {
                return _p2;
            }
            set
            {
                _p2 = value;
            }
        }
        private static EnumCarte _carte = EnumCarte.DEMO;

        /*
         * @var EnumCarte carte
         * Type of map
         */
        public static EnumCarte carte
        {
            get
            {
                return _carte;
            }
            set
            {
                _carte = value;
            }
        }

        private static int _skinJ1 = -1;

        /*
         * @var int skinJ1
         * Player's 1 skin
         */
        public static int skinJ1
        {
            get
            {
                return _skinJ1;
            }
            set
            {
                _skinJ1 = value;
            }
        }

        private static int _skinJ2 = -1;

        /*
         * @var int skinJ2
         * Player's 2 skin
         */
        public static int skinJ2
        {
            get
            {
                return _skinJ2;
            }
            set
            {
                _skinJ2 = value;
            }
        }

        private static List<string> _nomUniteJ1 = new List<string>();

        /*
         * @var List<string> nomUniteJ2
         * Name of player's 1 units
         */
        public static List<string> nomUniteJ1
        {
            get
            {
                return _nomUniteJ1;
            }
            set
            {
                _nomUniteJ1 = value;
            }
        }

        private static List<string> _nomUniteJ2 = new List<string>();

        /*
         * @var List<string> nomUniteJ2
         * Name of player's 2 units
         */
        public static List<string> nomUniteJ2
        {
            get
            {
                return _nomUniteJ2;
            }
            set
            {
                _nomUniteJ2 = value;
            }
        }

        /*
         * @var bool checkJ1
         * IA1 is checked
         */
        public static bool checkJ1 { get; set; }


        /*
         * @var bool checkJ2
         * IA2 is checked
         */
        public static bool checkJ2 { get; set; }

        /**
         * Reset New game window
         * 
         */
        public static void clearWindows()
        {
            j1 = "Joueur1";
            j2 = "Joueur2";
            _p1 = EnumPeuple.NULL;
            _p2 = EnumPeuple.NULL;
            _carte = EnumCarte.DEMO;
            _skinJ1 = -1;
            _skinJ2 = -1;
            checkJ1 = false;
            checkJ2 = false;
            AbstractWindow.nomUniteJ1.Clear();
            AbstractWindow.nomUniteJ2.Clear();
        }
    }
}
