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

        private int currentPlayer;
        public int CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
            set
            {
                currentPlayer = value;
                getOtherPlayer().IsMyTurn = false;
                getCurrentPlayer().IsMyTurn = true;
            }
        }
        public int OtherPlayer
        {
            get
            {
                return (CurrentPlayer + 1) % 2;
            }
        }
        public SelectUnit su { get; set; }
        public GameWindow(PartieImp partie)
        {
            this.partie = partie;
            this.map = new MapView(partie.carte, this);
            J1 = new PlayerView(partie.j1, new Point(0, 0), map);
            J2 = new PlayerView(partie.j2, new Point(Math.Sqrt(partie.carte.cases.Count()) - 1, Math.Sqrt(partie.carte.cases.Count()) - 1), map);
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

        public void SelectUnit(object sender, MouseButtonEventArgs e)
        {
            Grid g = sender as Grid;
            int uid = System.Convert.ToInt32(g.Tag);
            BoxView SelectedBoxForUnit = map.SelectedBoxForUnit;
            BoxView box = map.cases[uid];
            /*if (isUnitSelected)
            {*/
                /*if (SelectedBoxForUnit == box)
                {
                    isUnitSelected = false;
                    SelectedBoxForUnit.SelectedUnit = null;
                }*/
                // deplacer unité
            if (SelectedBoxForUnit != null)
            {
                UnitView unit = SelectedBoxForUnit.SelectedUnit;
                if (unit != null)
                {
                    Move move = unit.unit.seDeplacer(SelectedBoxForUnit.Uid, uid, getCurrentPlayer().peuple.peuple.getType(), map.carte, getOtherPlayer().peuple.peuple);
                    unit.HasAlreadyPlayed = move.hasPlayed;
                    switch (move.mv)
                    {
                        case EnumMove.MOVE:
                            box.addUnit(unit, CurrentPlayer);
                            SelectedBoxForUnit.removeUnit(unit, CurrentPlayer);
                            isUnitSelected = false;
                            SelectedBoxForUnit.SelectedUnit = null;
                            break;
                        case EnumMove.NOMOVE:
                            isUnitSelected = false;
                            SelectedBoxForUnit.SelectedUnit = null;
                            break;
                        case EnumMove.CBT:
                            CombatImp cbt = new CombatImp(unit.unit, move.unites);
                            EnumBattle battle = cbt.effectuerCombat();
                            switch (battle)
                            {
                                case EnumBattle.CBT_DRAW:
                                    isUnitSelected = false;
                                    SelectedBoxForUnit.SelectedUnit = null;
                                    break;
                                case EnumBattle.CBT_LOSS:
                                    if (!cbt.uniteAtt.survive(getCurrentPlayer().peuple.peuple.getType()))
                                    {
                                        SelectedBoxForUnit.destroyUnit(cbt.uniteAtt, CurrentPlayer);
                                        getCurrentPlayer().peuple.destroy(cbt.uniteAtt);
                                    }
                                    break;
                                case EnumBattle.CBT_VICTORY_MOVE:
                                    if (!cbt.uniteDef.survive(getOtherPlayer().peuple.peuple.getType()))
                                    {
                                        box.destroyUnit(cbt.uniteDef, OtherPlayer);
                                        getOtherPlayer().peuple.destroy(cbt.uniteDef);
                                        unit.unit.move(uid);
                                        box.addUnit(unit, CurrentPlayer);
                                        SelectedBoxForUnit.removeUnit(unit, CurrentPlayer);
                                    }
                                    isUnitSelected = false;
                                    SelectedBoxForUnit.SelectedUnit = null;
                                    break;
                                case EnumBattle.CBT_VICTORY_NOMOVE:
                                    if (!cbt.uniteDef.survive(getOtherPlayer().peuple.peuple.getType()))
                                    {
                                        box.destroyUnit(cbt.uniteDef, OtherPlayer);
                                        getOtherPlayer().peuple.destroy(cbt.uniteDef);
                                    }
                                    isUnitSelected = false;
                                    SelectedBoxForUnit.SelectedUnit = null;
                                    break;
                            }
                            break;
                        }
                        this.verifierFinPartie();
                    }
                }   
            //}
            /*else
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
            }*/
        }

        public bool verifierFinPartie()
        {
            JoueurImp j;
            if ((j = (JoueurImp)partie.verifierFinPartie()) != null)
            {
                this.getCurrentPlayer().RaisePropertyChanged("nbPoints");
                // j est vainqueur
                MessageBox.Show(j.nom + " gagne la partie " + this.getCurrentPlayer().joueur.nbPoints.ToString() + " à " + this.getOtherPlayer().joueur.nbPoints.ToString());
                return true;
            }
            return false;
        }
        private void NextTurn(object sender, RoutedEventArgs e)
        {
            // reset boxes
            map.resetBoxes();
            this.getCurrentPlayer().RaisePropertyChanged("nbPoints");
            this.items.SelectedItem = null;
            this.getCurrentPlayer().peuple.reset();
            this.switchPlayer();
            if (map.SelectedBox != null)
            {
                map.SelectedBox.RaisePropertyChanged("units");
                map.SelectedBox.RaisePropertyChanged("unitsCount");
                map.SelectedBox.RaisePropertyChanged("otherUnits");
                map.SelectedBox.RaisePropertyChanged("otherUnitsCount");
            }
            partie.tours.Add(new TourImp()); // à changer lors de l'implementation des tours en ajoutant une classe TourView et ici on ajoute TourView.tour
            if (!this.verifierFinPartie())
            {
                RaisePropertyChanged("nbTour");
            }
        }
        public String nbTour
        {
            get { return partie.getNbTour() + "/" + partie.nbTourMax; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(String property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void _this_KeyUp(object sender, KeyEventArgs e)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (e.Key == Key.S && handle)
            {
                this.save();
            }
        }

        public void save()
        {
            MessageBox.Show("Save");
        }
    }
}
