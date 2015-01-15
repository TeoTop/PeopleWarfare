using PeopleWar;
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

namespace WarFareWPF
{
    /// <summary>
    /// Logique d'interaction pour VictoryWindows.xaml
    /// </summary>
    public partial class VictoryWindows : Window
    {
        /*
         * The winner player of the game
         */
        public JoueurImp winner { get; set; }

        /*
         * List of images to show in the bottom of the window
         */
        public List<MyString> urlImages { get; set; }

        public int nImage { get { return urlImages.Count; } }

        /*
         * VictoryWindows constructor
         * @param JoueurImp winner
         * @param GameWindow gw
         */
        public VictoryWindows(JoueurImp winner)
        {
            this.winner = winner;
            this.urlImages = new List<MyString>();
            switch (winner.peuple.getType())
            {
                case EnumPeuple.ORC :
                    urlImages.Add(new MyString("res/orc1.png"));
                    urlImages.Add(new MyString("res/orc3.png"));
                    urlImages.Add(new MyString("res/orc2.png"));
                    break;
                case EnumPeuple.ELF:
                    urlImages.Add(new MyString("res/elf1.png"));
                    urlImages.Add(new MyString("res/elf3.png"));
                    urlImages.Add(new MyString("res/elf2.png"));
                    break;
                case EnumPeuple.NAIN:
                    urlImages.Add(new MyString("res/nain1.png"));
                    urlImages.Add(new MyString("res/nain3.png"));
                    urlImages.Add(new MyString("res/nain2.png"));
                    break;
            }

            InitializeComponent();
            this.DataContext = this;
        }

        /*
         * Close this window and the main game window and show the menu window (MainWindow)
         */
        private void backToMenu(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            Window w = Application.Current.Windows.OfType<GameWindow>().First();
            if (w != null)
            {
                w.Close();
            }

        }

        /*
         * Close this window
         */
        private void stayOnGame(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
