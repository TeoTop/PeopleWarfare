using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WarFareWPF
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class NewGame : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private bool _error;
        public bool errorNameJ1 { get; set; }
        public bool errorNameJ2 { get; set; }
        public bool errorPeuple { get; set; }
        private EnumCarte _carte_enum;
        public EnumCarte carte
        {
            get
            {
                return _carte_enum;
            }
            set
            {
                _carte_enum = value;
                OnPropertyChanged("carte");
            }
        }

        private String _carte;
        public String Carte
        {
            get
            {
                return _carte;
            }
            set
            {
                _carte = value;
                OnPropertyChanged("Carte");
            }
        }
        public bool error {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                OnPropertyChanged("error");
                OnPropertyChanged("reverseError");
            }
        }

        public bool reverseError {
            get
            {
                return !_error;
            }
        }

        public EnumPeuple orc
        {
            get { return EnumPeuple.ORC; }
        }

        public EnumPeuple elf
        {
            get { return EnumPeuple.ELF; }
        }

        public EnumPeuple nain
        {
            get { return EnumPeuple.NAIN; }
        }

        public EnumPeuple chevalier
        {
            get { return EnumPeuple.CHEVALIER; }
        }

        public EnumPeuple golem
        {
            get { return EnumPeuple.GOLEM; }
        }

        public NewGame()
        {
            // focus event for textbox
            EventManager.RegisterClassHandler(typeof(System.Windows.Controls.Primitives.TextBoxBase), UIElement.GotFocusEvent, new RoutedEventHandler(TextBoxBaseGotFocus));
            this.errorNameJ1 = false;
            this.errorNameJ2 = false;
            this.errorPeuple = false;
            SetError();
            InitializeComponent();
            if (AbstractWindow.j1 != null)
            {
                this.j1.Text = AbstractWindow.j1;
            }
            if (AbstractWindow.j2 != null)
            {
                this.j2.Text = AbstractWindow.j2;
            }
            if (AbstractWindow.p1 != EnumPeuple.NULL)
            {
                this.p1.SelectedIndex = (int)AbstractWindow.p1;
            }
            if (AbstractWindow.p2 != EnumPeuple.NULL)
            {
                this.p2.SelectedIndex = (int)AbstractWindow.p2;
            }
            checkJ1 = AbstractWindow.checkJ1;
            checkJ2 = AbstractWindow.checkJ2;
            switch (AbstractWindow.carte)
            {
                case EnumCarte.DEMO:
                    this.carte = EnumCarte.DEMO;
                    break;
                case EnumCarte.PETITE:
                    this.carte = EnumCarte.PETITE;
                    break;
                case EnumCarte.NORMALE:
                    this.carte = EnumCarte.NORMALE;
                    break;
            }
        }


        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Suivant(object sender, RoutedEventArgs e)
        {
            //ajouter le choix des skins
            DirecteurPartie dp = new DirecteurPartie();
            dp.definirMonteur(new MonteurNvllePartie());
            PartieImp partie = dp.creerPartie(this.j1.Text, this.j2.Text, this.carte, (EnumPeuple)this.p1.SelectedIndex, (EnumPeuple)this.p2.SelectedIndex);
            NewGame2 ng2 = new NewGame2(partie, checkJ1, checkJ2);
            ng2.Show();
            this.Close();
        }

        private static void TextBoxBaseGotFocus(object sender, RoutedEventArgs e)
        {
            // Get the TextBoxBase
            var elem = sender as System.Windows.Controls.Primitives.TextBoxBase;
            if (elem != null)
            {
                elem.SelectAll();
            }
        }

        private void p1_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            String name = textbox.Text;
            this.errorNameJ1 = notAllowedName(name);
            SetError();
        }

        private void p2_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            String name = textbox.Text;
            this.errorNameJ2 = notAllowedName(name);
            SetError();
        }
        
        private bool notAllowedName(string name)
        {
            Regex r = new Regex(@"^[a-zA-Z]+[A-Za-z\d\s]*$");
            Match m = r.Match(name);
            return !m.Success;
        }

        private void p1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (this.p1 == null || this.p2 == null)
            {
                this.errorPeuple = false;
            }
            else
            {
                this.errorPeuple = (this.p1.SelectedIndex == this.p2.SelectedIndex) ? true : false;
            }
            SetError();
        }
        
        private void SetError()
        {
            this.error = this.errorNameJ1 || this.errorNameJ2 || this.errorPeuple;
        }

        private void Prec(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
			m.Show();
            this.Close();
        }

        private void _this_Closing(object sender, CancelEventArgs e)
        {
            AbstractWindow.j1 = this.j1.Text;
            AbstractWindow.j2 = this.j2.Text;
            AbstractWindow.p1 = (EnumPeuple)this.p1.SelectedIndex;
            AbstractWindow.p2 = (EnumPeuple)this.p2.SelectedIndex;
            AbstractWindow.carte = this.carte;
            AbstractWindow.checkJ1 = this.checkJ1;
            AbstractWindow.checkJ2 = this.checkJ2;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton r = sender as RadioButton;
            this.Carte = r.Content.ToString();
        }

        private bool _checkJ1;
        public bool checkJ1
        {
            get
            {
                return _checkJ1;
            }
            set
            {
                _checkJ1 = value;
                OnPropertyChanged("checkJ1");
            }
        }


        private bool _checkJ2;
        public bool checkJ2
        {
            get
            {
                return _checkJ2;
            }
            set
            {
                _checkJ2 = value;
                OnPropertyChanged("checkJ2");
            }
        }
    }
}
