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

namespace WarFareWPF
{
    /// <summary>
    /// Logique d'interaction pour GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public PartieImp partie { get; set; }
        public MapView map { get; set; }
        public PlayerView J1
        {
            get;
            set;
        }
        public PlayerView J2
        {
            get;
            set;
        }
        public bool isUnitSelected
        {
            get;
            set;
        }


        public int CurrentPlayer { get; set; }

        public int nbUnitesj1
        {
            get { return partie.j1.peuple.getNbUnite(); }
        }

        public int nbUnitesj2
        {
            get { return partie.j2.peuple.getNbUnite(); }
        }

        public SelectUnit su { get; set; }

        public GameWindow(PartieImp partie)
        {
            this.partie = partie;
            this.map = new MapView(partie.carte);
            J1 = new PlayerView(partie.j1, new Point(0, 0));
            J2 = new PlayerView(partie.j2, new Point(Math.Sqrt(partie.carte.cases.Count()) - 1, Math.Sqrt(partie.carte.cases.Count()) - 1));
            CurrentPlayer = 0;
            // definir les cases où les unités seront mises depuis le wrapper
            List<UnitView> unitsJ1 = map.cases[partie.carte.getKey((int)J1.InitialPosition.X, (int)J1.InitialPosition.Y)].unitsJ1;
            List<UnitView> unitsJ2 = map.cases[partie.carte.getKey((int)J2.InitialPosition.X, (int)J2.InitialPosition.Y)].unitsJ2;
            J1.peuple.units.ForEach(unit => unitsJ1.Add(unit));
            J2.peuple.units.ForEach(unit => unitsJ2.Add(unit));
            isUnitSelected = false;
            InitializeComponent();
        }

        public PlayerView getCurrentPlayer()
        {
            return CurrentPlayer == 0 ? J1 : J2;
        }

        public PlayerView getOtherPlayer()
        {
            return CurrentPlayer == 0 ? J2 : J1;
        }

        public void switchPlayer()
        {
            CurrentPlayer = CurrentPlayer == 0 ? 1 : 0;
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //ctrl
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (handle)
            {
                // zoom / dezoom without scroll if ctrl is handled
                if (e.Delta > 0)
                {
                    this.map.Zoom += 5;
                }
                else
                {
                    this.map.Zoom -= 5;
                }
                e.Handled = true;
            }
        }

        private void SelectUnit(object sender, MouseButtonEventArgs e)
        {
            Grid g = sender as Grid;
            int uid = System.Convert.ToInt32(g.Tag);
            BoxView SelectedBoxForUnit = map.SelectedBoxForUnit;
            BoxView box = map.cases[uid];
            if (isUnitSelected && SelectedBoxForUnit != box)
            {
                // deplacer unité
                if (SelectedBoxForUnit != null)
                {
                    UnitView unit = SelectedBoxForUnit.SelectedUnit;
                    if (unit != null)
                    {
                        Double pm;
                        if ((pm = unit.unit.verifierDeplacement(SelectedBoxForUnit.Uid, uid, getCurrentPlayer().peuple.peuple.getType(), map.carte, getOtherPlayer().peuple.peuple)) != 0)
                        {
                            // ne pas oublier de vérifier le cbt
                            unit.unit.seDeplacer(uid, pm);
                            box.addUnit(unit, CurrentPlayer);
                            SelectedBoxForUnit.removeUnit(unit, CurrentPlayer);
                            unit.HasAlreadyPlayed = true;
                            isUnitSelected = false;
                            SelectedBoxForUnit.SelectedUnit = null;
                        }
                    }
                }
            }
            else
            {
                // selectionner unité
                List<UnitView> units = box.getUnits(CurrentPlayer);
                if (su != null) su.Close();
                su = new SelectUnit(box, units, this);
                if (su.IsInitialized)
                {
                    su.ShowDialog();
                    //Focus
                    su.Activate();
                }
            }
        }

        private void NextTurn(object sender, RoutedEventArgs e)
        {
            JoueurImp j;
            if ((j = (JoueurImp)partie.verifierFinPartie()) == null)
            {
                this.switchPlayer();
                this.getCurrentPlayer().peuple.reset();
            }
            else
            {
                // j est vainqueur
            }
        }
    }
}
