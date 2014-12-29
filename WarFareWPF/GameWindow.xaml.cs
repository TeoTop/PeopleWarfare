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
using System.Windows.Controls.Primitives;
using System.ComponentModel;

namespace WarFareWPF
{
    /// <summary>
    /// Logique d'interaction pour GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameView game { get; set; }
        public SelectUnit su { get; set; }
        public GameWindow(PartieImp game)
        {
            this.game = new GameView(game);
            InitializeComponent();
        }


        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.game.Grid_MouseWheel(sender, e);
        }

        public void MoveUnit(object sender, MouseButtonEventArgs e)
        {
            this.game.MoveUnit(sender, e);
        }

        private void NextTurn(object sender, RoutedEventArgs e)
        {
            this.items.SelectedItem = null;
            this.game.NextTurn(sender, e);
            game.RaisePropertyChanged("CurrentPlayer");
            game.RaisePropertyChanged("OtherPlayer");
        }

        private void _this_KeyUp(object sender, KeyEventArgs e)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (e.Key == Key.S && handle)
            {
                this.game.save();
            }
            if (e.Key == Key.F1)
            {
                this.NextTurn(null, null);
            }
            if (e.Key == Key.W && handle)
            {
                this.Quit(null, null);
            }
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Vous êtes sûr de vouloir quitter la partie ?", "Quitter", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (MessageBox.Show("Voulez vous sauvegarder la partie ?", "Sauvegarder", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    game.save();
                }
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }

    }
}
