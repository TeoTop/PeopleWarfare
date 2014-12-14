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
            //this.items1.ItemsSource = ((BoxView)this.items1.DataContext).getUnits(game.CurrentPlayer);
            //this.items2.ItemsSource = ((BoxView)this.items2.DataContext).getUnits(game.OtherPlayer);
        }

        private void _this_KeyUp(object sender, KeyEventArgs e)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (e.Key == Key.S && handle)
            {
                this.game.save();
            }
        }

    }
}
