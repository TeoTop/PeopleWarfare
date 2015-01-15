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

using WarFareWPF.Command;



namespace WarFareWPF

{

    /// <summary>

    /// Logique d'interaction pour GameWindow.xaml

    /// </summary>

    public partial class GameWindow : Window

    {

        public GameView game { get; set; }

        public SelectUnit su { get; set; }

        public KeyEventCommand keyCommand { get; set; }

        public GameWindow(PartieImp game, bool pc1 = false, bool pc2 = false)

        {

            this.game = new GameView(game, this);
            this.pc1 = pc1;
            this.pc2 = pc2;

            keyCommand = new KeyEventCommand();



            // add commands

            keyCommand.addAction(new KeyClass(Key.S, true), () => this.game.save());

            keyCommand.addAction(new KeyClass(Key.F1, false), () => this.NextTurn(null, null));

            keyCommand.addAction(new KeyClass(Key.W, true), () => this.Quit(null, null));

            keyCommand.addAction(new KeyClass(Key.Space, false), () => this.NextUnit(null, null));

            keyCommand.addNumPadAction(this.game, new KeyClass(Key.NumPad3, false), (x, y, z) =>

            {

                return (x + y) % z;

            });

            keyCommand.addNumPadAction(this.game, new KeyClass(Key.NumPad6, false), (x, _, z) =>

            {

                return (x + 1) % z;

            });

            keyCommand.addNumPadAction(this.game, new KeyClass(Key.NumPad9, false), (x, y, z) =>

            {

                return (x - y + 1) % z;

            });

            keyCommand.addNumPadAction(this.game, new KeyClass(Key.NumPad7, false), (x, y, z) =>

            {

                return (x - y) % z;

            });

            keyCommand.addNumPadAction(this.game, new KeyClass(Key.NumPad4, false), (x, _, z) =>

            {

                return (x - 1) % z;

            });

            keyCommand.addNumPadAction(this.game, new KeyClass(Key.NumPad1, false), (x, y, z) =>

            {

                return (x + y - 1) % z;

            });

            keyCommand.addAction(new KeyClass(Key.Escape, false), () => this.game.map.resetBoxes());

            DoRound();

            InitializeComponent();

        }

        private void DoRound()
        {
            if (getPc())
            {
                MyTask task = new MyTask(() =>
                {
                    this.game.DoRound();
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        NextTurn(null, null);
                    }));
                });
            }
        }

        public bool getPc()
        {
            return game.CurrentPlayer == 0 ? pc1 : pc2;
        }





        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)

        {

            this.game.Grid_MouseWheel(sender, e);

        }



        public void MoveUnit(object sender, MouseButtonEventArgs e)

        {

            this.game.MoveUnit(sender, e);

            this.game.alreadySaved = false;

        }



        private void NextTurn(object sender, RoutedEventArgs e)

        {

            this.items.SelectedItem = null;

            this.game.NextTurn(sender, e);

            game.RaisePropertyChanged("CurrentPlayer");

            game.RaisePropertyChanged("OtherPlayer");

            this.game.alreadySaved = false;

            DoRound();
        }



        private void _this_KeyUp(object sender, KeyEventArgs e)

        {

            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;



            KeyClass key = new KeyClass(e.Key, handle);



            if (keyCommand.actions.Keys.Contains(key))

            {

                keyCommand.actions[key]();

            }

        }


        // TODO : cases ateignables
        // TODO : Rotate image in battle
        // TODO : nextunit space key
        // TODO : refresh units after a battle




        private void Quit(object sender, RoutedEventArgs e)

        {

            this.Exit(sender, e);

        }



        private void Exit(object sender, RoutedEventArgs e, bool openMW = true, CancelEventArgs ev = null, Action callback = null)

        {

            if (MessageBox.Show("Vous êtes sûr de vouloir quitter la partie ?", "Quitter", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)

            {

                if (!game.alreadySaved && !game.game.save && (MessageBox.Show("Voulez vous sauvegarder la partie ?", "Sauvegarder", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes))

                {

                    game.save();

                }



                if (openMW)

                {

                    MainWindow mw = new MainWindow();

                    mw.Show();

                }



                this.Closing -= _this_Closing;

                if (callback != null)

                {

                    callback();

                }

                try

                {

                    this.Close();

                }

                catch (Exception) { }

            }

            else if (ev != null)

            {

                ev.Cancel = true;

            }

        }



        private void _this_Closing(object sender, CancelEventArgs e)

        {

            this.Exit(sender, null, true, e);

        }



        private void NextUnit(object sender, RoutedEventArgs e)

        {

            this.game.NextUnit(sender, e);

            this.game.alreadySaved = false;

        }



        private void _this_ContentRendered(object sender, EventArgs e)

        {

            // zoom

            this.game.map.Zoom = (int)((100 / SB.RowDefinitions[3].ActualHeight) * (80 / (int)(Math.Sqrt(game.map.carte.nbCase))));

        }



        private void NewGame(object sender, RoutedEventArgs e)

        {

            this.Exit(null, null, false, null, () =>

            {

                MainWindow mw = new MainWindow();

                mw.NewGame(null, null);

            });

        }



        private void ChGame(object sender, RoutedEventArgs e)

        {

            this.Exit(null, null, false, null, () =>

            {

                MainWindow mw = new MainWindow();

                mw.ChGame(null, null);

            });

        }



        private void Save(object sender, RoutedEventArgs e)

        {

            this.game.save();

        }



        private void SaveAs(object sender, RoutedEventArgs e)

        {

            this.game.saveAs();

        }




        public bool pc1 { get; set; }

        public bool pc2 { get; set; }
    }

}

