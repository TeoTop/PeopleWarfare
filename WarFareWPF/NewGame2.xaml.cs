using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PeopleWar;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace WarFareWPF
{
    /// <summary>
    /// Logique d'interaction pour NewGame2.xaml
    /// </summary>
    public partial class NewGame2 : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private bool _errorName;

        public PartieImp partie { get; set; }

        public int nName { get { return partie.j1.peuple.unites.Count; } }

        private string[] _orcSkin = {"res/orc1.png", "res/orc2.png", "res/orc3.png"};

        private string[] _elfSkin = { "res/elf1.png", "res/elf2.png", "res/elf3.png" };

        private string[] _nainSkin = { "res/nain1.png", "res/nain2.png", "res/nain3.png" };

        private string _skinJ1;

        private string _skinJ2;

        public string skinSrc1
        {
            get
            {
                return _skinJ1;
            }
            set
            {
                _skinJ1 = value;
                OnPropertyChanged("skinSrc1");
            }
        }

        public string skinSrc2
        {
            get
            {
                return _skinJ2;
            }
            set
            {
                _skinJ2 = value;
                OnPropertyChanged("skinSrc2");
            }
        }

        
        public bool errorName {
            get
            {
                return _errorName;
            }
            set
            {
                _errorName = value;
                OnPropertyChanged("errorName");
                OnPropertyChanged("reverseError");
            }
        }

        public bool reverseError
        {
            get
            {
                return !_errorName;
            }
        }

        public NewGame2(PartieImp partie)
        {
            this.partie = partie;

            if (partie.j1.peuple.getType() == AbstractWindow.p1)
            {
                if (AbstractWindow.skinJ1 != -1)
                {
                    partie.j1.peuple.skin = AbstractWindow.skinJ1;
                }

                if (AbstractWindow.nomUniteJ1.Count != 0)
                {
                    for (int i = 0; i < AbstractWindow.nomUniteJ1.Count; i++)
                        partie.j1.peuple.unites.ElementAt(i).nom = AbstractWindow.nomUniteJ1.ElementAt(i);
                }
            }

            if (partie.j2.peuple.getType() == AbstractWindow.p2)
            {
                if (AbstractWindow.nomUniteJ2.Count != 0)
                {
                    for (int i = 0; i < AbstractWindow.nomUniteJ2.Count; i++)
                        partie.j2.peuple.unites.ElementAt(i).nom = AbstractWindow.nomUniteJ2.ElementAt(i);
                }

                if (AbstractWindow.skinJ2 != -1)
                {
                    partie.j2.peuple.skin = AbstractWindow.skinJ2;
                }
            }

            this._skinJ1 = changerSkin(partie.j1.peuple);
            this._skinJ2 = changerSkin(partie.j2.peuple);
            this._errorName = false;
            InitializeComponent();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void CreateGame(object sender, RoutedEventArgs e)
        {
            GameWindow gw = new GameWindow(this.partie);
            gw.Show();
            Dictionary.gw = gw;    //transférer dans le contructeur de GameWindows;
            this.Close();
        }

        private void Prec(object sender, RoutedEventArgs e)
        {
            NewGame m = new NewGame();
            m.Show();
            this.Close();
        }

        private void p1_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            String name = textbox.Text;
            this.errorName = allowedName(name);
        }

        private bool allowedName(string name)
        {
            Regex r = new Regex(@"^[a-zA-Z]+[A-Za-z|\d]*$");
            Match m = r.Match(name);
            return !m.Success;
        }

        private void precSkin1(object sender, RoutedEventArgs e)
        {
            partie.j1.peuple.skin = (partie.j1.peuple.skin + 2) % 3;
            skinSrc1 = changerSkin(partie.j1.peuple);
        }

        private void suivSkin1(object sender, RoutedEventArgs e)
        {
            partie.j1.peuple.skin = (partie.j1.peuple.skin + 1) % 3;
            skinSrc1 = changerSkin(partie.j1.peuple);
        }

        private void precSkin2(object sender, RoutedEventArgs e)
        {
            partie.j2.peuple.skin = (partie.j2.peuple.skin + 2) % 3;
            skinSrc2 = changerSkin(partie.j2.peuple);
        }

        private void suivSkin2(object sender, RoutedEventArgs e)
        {
            partie.j2.peuple.skin = (partie.j2.peuple.skin + 1) % 3;
            skinSrc2 = changerSkin(partie.j2.peuple);
        }

        private string changerSkin(PeupleA peuple){
            string ret = "";

            switch (peuple.getType()){
                    case EnumPeuple.ORC: 
                        ret = _orcSkin[peuple.skin]; 
                    break;
                    case EnumPeuple.ELF:
                        ret = _elfSkin[peuple.skin];
                        break;
                    case EnumPeuple.NAIN:
                        ret = _nainSkin[peuple.skin];
                        break;
                    default:
                        break;
            }

            return ret;
        }

        private void _this_Closing(object sender, CancelEventArgs e)
        {
            AbstractWindow.nomUniteJ1.Clear();
            AbstractWindow.nomUniteJ2.Clear();

            partie.j1.peuple.unites.ForEach(unit => AbstractWindow.nomUniteJ1.Add(unit.nom));
            partie.j2.peuple.unites.ForEach(unit => AbstractWindow.nomUniteJ2.Add(unit.nom));
            AbstractWindow.skinJ1 = partie.j1.peuple.skin;
            AbstractWindow.skinJ2 = partie.j2.peuple.skin;

        }
    }
}
