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
        public static String j1 { get; set; }
        public static String j2 { get; set; }
        private static EnumPeuple _p1 = EnumPeuple.NULL;
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

        public static bool checkJ1 { get; set; }

        public static bool checkJ2 { get; set; }
    }
}
