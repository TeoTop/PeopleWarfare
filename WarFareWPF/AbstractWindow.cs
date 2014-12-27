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
    }
}
