using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WarFareWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            NewGame n = new NewGame();
            n.Show();
            this.Close();
        }

        private void ChGame(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.Filter = "PeopleWarfare (*.ppw)|*.ppw|Tous les fichiers (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Chargement d'une sauvegarde PeopleWarfare";

            if (openFileDialog1.ShowDialog() == true)
            {
                DirecteurPartie dp = new DirecteurPartie();
                dp.definirMonteur(new MonteurNvllePartie());
                PartieImp partie = dp.chargerPartie(openFileDialog1.FileName);
                GameWindow gw = new GameWindow(partie);
                gw.Show();
                this.Close();
            }
        }

    }
}
