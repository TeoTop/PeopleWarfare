using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleWar;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
namespace WarFareWPF
{
    public class GameView : Notifier
    {

        public PartieImp game { get; set; }
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

        public String nbTour
        {
            get { return game.getNbTour() + "/" + game.nbTourMax; }
        }
        /*public bool isUnitSelected
        {
            get;
            set;
        }*/

        public GameView(PartieImp game)
        {
            this.game = game;
            this.map = new MapView(game.carte);
            // definir les cases où les unités seront mises depuis le wrapper
            J1 = new PlayerView(game.j1, new Point(0, 0), map);
            J2 = new PlayerView(game.j2, new Point(Math.Sqrt(game.carte.cases.Count()) - 1, Math.Sqrt(game.carte.cases.Count()) - 1), map);
            CurrentPlayer = 0;
            ObservableCollection<UnitView> unitsJ1 = map.cases[game.carte.getKey((int)J1.InitialPosition.X, (int)J1.InitialPosition.Y)].unitsJ1;
            ObservableCollection<UnitView> unitsJ2 = map.cases[game.carte.getKey((int)J2.InitialPosition.X, (int)J2.InitialPosition.Y)].unitsJ2;
            J1.peuple.units.ForEach(unit => unitsJ1.Add(unit));
            J2.peuple.units.ForEach(unit => unitsJ2.Add(unit));
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


        public void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
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

        public void MoveUnit(object sender, MouseButtonEventArgs e)
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
                            //isUnitSelected = false;
                            SelectedBoxForUnit.SelectedUnit = null;
                            break;
                        case EnumMove.NOMOVE:
                            //isUnitSelected = false;
                            SelectedBoxForUnit.SelectedUnit = null;
                            break;
                        case EnumMove.CBT:
                            CombatImp cbt = new CombatImp(unit.unit, move.unites);
                            EnumBattle battle = cbt.effectuerCombat();
                            switch (battle)
                            {
                                case EnumBattle.CBT_DRAW:
                                    //isUnitSelected = false;
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
                                    //isUnitSelected = false;
                                    SelectedBoxForUnit.SelectedUnit = null;
                                    break;
                                case EnumBattle.CBT_VICTORY_NOMOVE:
                                    if (!cbt.uniteDef.survive(getOtherPlayer().peuple.peuple.getType()))
                                    {
                                        box.destroyUnit(cbt.uniteDef, OtherPlayer);
                                        getOtherPlayer().peuple.destroy(cbt.uniteDef);
                                    }
                                    //isUnitSelected = false;
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
            if ((j = (JoueurImp)game.verifierFinPartie()) != null)
            {
                this.getCurrentPlayer().RaisePropertyChanged("nbPoints");
                // j est vainqueur
                MessageBox.Show(j.nom + " gagne la partie " + this.getCurrentPlayer().joueur.nbPoints.ToString() + " à " + this.getOtherPlayer().joueur.nbPoints.ToString());
                return true;
            }
            return false;
        }

        public void NextTurn(object sender, RoutedEventArgs e)
        {
            // reset boxes
            map.resetBoxes();
            this.getCurrentPlayer().RaisePropertyChanged("nbPoints");
            this.getCurrentPlayer().peuple.reset();
            this.switchPlayer();
            if (map.SelectedBox != null)
            {
                map.SelectedBox.RaisePropertyChanged("unitsJ1");
                map.SelectedBox.RaisePropertyChanged("NbUniteJ1");
                map.SelectedBox.RaisePropertyChanged("unitsJ1");
                map.SelectedBox.RaisePropertyChanged("NbUniteJ2");
            }
            game.tours.Add(new TourImp()); // à changer lors de l'implementation des tours en ajoutant une classe TourView et ici on ajoute TourView.tour
            if (!this.verifierFinPartie())
            {
                RaisePropertyChanged("nbTour");
            }
        }
        public void save()
        {
            MessageBox.Show("Save");
        }
    }
}
