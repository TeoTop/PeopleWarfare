using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using PeopleWar;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Threading;
using System.IO;
namespace WarFareWPF
{
    public class GameView : Notifier
    {
        public PartieImp game { get; set; }
        public MapView map { get; set; }
        public ActionListView al { get; set; }
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
        public int CurrentPlayer { get { return game.joueurCourant; } }
        public int OtherPlayer { get { return (CurrentPlayer + 1) % 2; } }

        public String nbTour
        {
            get { return game.getNbTour() + "/" + game.nbTourMax; }
        }
        public EnumBattle battle { get; set; }
        /*public bool isUnitSelected
        {
            get;
            set;
        }*/
        public bool FinPartie { get; set; }

        public GameView(PartieImp game)
        {
            this.game = game;
            this.map = new MapView(game.carte);
            // definir les cases où les unités seront mises depuis le wrapper
            /*J1 = new PlayerView(game.j1, map);
            J2 = new PlayerView(game.j2, map);*/
            J1 = new PlayerView(game.j1, this.map);
            J2 = new PlayerView(game.j2, this.map);
            //ObservableCollection<UnitView> unitsJ1 = map.cases[game.carte.getKey((int)J1.InitialPosition.X, (int)J1.InitialPosition.Y)].unitsJ1;
            //ObservableCollection<UnitView> unitsJ2 = map.cases[game.carte.getKey((int)J2.InitialPosition.X, (int)J2.InitialPosition.Y)].unitsJ2;
            J1.peuple.units.ForEach(unit => map.cases[unit.unit.pos].unitsJ1.Add(unit));
            J2.peuple.units.ForEach(unit => map.cases[unit.unit.pos].unitsJ2.Add(unit));
            this.al = new ActionListView(game.tours);
            FinPartie = false;
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
            //CurrentPlayer = CurrentPlayer == 0 ? 1 : 0;
            game.switcherJoueur();
            getOtherPlayer().IsMyTurn = false;
            getCurrentPlayer().IsMyTurn = true;
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
            // deplacer unité
            if (!FinPartie)
            {
                if (SelectedBoxForUnit != null)
                {
                    UnitView unit = SelectedBoxForUnit.SelectedUnit;
                    if (unit != null)
                    {
                        MoveImp move = (MoveImp)unit.unit.seDeplacer(SelectedBoxForUnit.Uid, uid, getCurrentPlayer().peuple.peuple.getType(), map.carte, getOtherPlayer().peuple.peuple);
                        unit.HasAlreadyPlayed = move.hasPlayed;
                        switch (move.mv)
                        {
                            case EnumMove.MOVE:
                                box.addUnit(unit, CurrentPlayer);
                                SelectedBoxForUnit.removeUnit(unit, CurrentPlayer);
                                break;
                            case EnumMove.NOMOVE:
                                break;
                            case EnumMove.CBT:
                                BattleWindow battle = new BattleWindow(unit, getOtherPlayer().peuple.Select(move.unites), this);
                                battle.ShowDialog();

                                switch (this.battle)
                                {
                                    case EnumBattle.CBT_DRAW:
                                        break;
                                    case EnumBattle.CBT_LOSS:
                                        SelectedBoxForUnit.destroyUnit(battle.battle.unitAtt.unit, CurrentPlayer);
                                        getCurrentPlayer().peuple.destroy(battle.battle.unitAtt.unit);
                                        break;
                                    case EnumBattle.CBT_VICTORY_MOVE:
                                        box.destroyUnit(battle.battle.unitDef.unit, OtherPlayer);
                                        getOtherPlayer().peuple.destroy(battle.battle.unitDef.unit);
                                        unit.unit.move(uid);
                                        box.addUnit(unit, CurrentPlayer);
                                        SelectedBoxForUnit.removeUnit(unit, CurrentPlayer);
                                        break;
                                    case EnumBattle.CBT_VICTORY_NOMOVE:
                                        box.destroyUnit(battle.battle.unitDef.unit, OtherPlayer);
                                        getOtherPlayer().peuple.destroy(battle.battle.unitDef.unit);
                                        break;
                                }
                                move.combattre(battle.battle.fight);
                                break;
                        }
                        SelectedBoxForUnit.SelectedUnit = null;
                        // les raise ne fonctionnent pas
                        SelectedBoxForUnit.RaisePropertyChanged("unitsJ1");
                        SelectedBoxForUnit.RaisePropertyChanged("NbUniteJ1");
                        SelectedBoxForUnit.RaisePropertyChanged("unitsJ2");
                        SelectedBoxForUnit.RaisePropertyChanged("NbUniteJ2");
                        box.RaisePropertyChanged("unitsJ1");
                        box.RaisePropertyChanged("NbUniteJ1");
                        box.RaisePropertyChanged("unitsJ2");
                        box.RaisePropertyChanged("NbUniteJ2");
                        this.getCurrentPlayer().RaisePropertyChanged("nbPoints"); /*Essayer d'ajouter la map*/
                        al.addAction(move);
                        this.verifierFinPartie();
                    }
                }
            }
        }

        public bool verifierFinPartie()
        {
            JoueurImp j;
            if ((j = (JoueurImp)game.verifierFinPartie()) != null)
            {
                FinPartie = true;
                // j est vainqueur
                VictoryWindows vw = new VictoryWindows(j);
                vw.Show();
                return true;
            }
            return false;
        }

        public void NextTurn(object sender, RoutedEventArgs e)
        {
            // reset boxes
            map.resetBoxes();
            this.getCurrentPlayer().peuple.reset();
            this.switchPlayer();
            if (map.SelectedBox != null)
            {
                map.SelectedBox.RaisePropertyChanged("unitsJ1");
                map.SelectedBox.RaisePropertyChanged("NbUniteJ1");
                map.SelectedBox.RaisePropertyChanged("unitsJ1");
                map.SelectedBox.RaisePropertyChanged("NbUniteJ2");
            }
            al.addTurn(); // à changer lors de l'implementation des tours en ajoutant une classe TourView et ici on ajoute TourView.tour
            if (!this.verifierFinPartie())
            {
                RaisePropertyChanged("nbTour");
            }
        }
        public void save()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "peopleWarfare_save";     //nom par default du fichier
                saveFileDialog.Filter = "PeopleWarfare (*.ppw)|*.ppw|Tous les fichiers (*.*)|*.*";  //liste des extension
                saveFileDialog.FilterIndex = 1;     // met ppw comme extension par default, 2 pour l'autre
                saveFileDialog.Title = "Sauvegarde de partie de PeopleWarfare";  //titre de la fenetre
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // trouver le dossier bureau

                if (saveFileDialog.ShowDialog() == true)
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    formatter.Serialize(stream, game);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la sauvegarde: " + ex.Message);
            }
        }



        public void NextUnit(object sender, RoutedEventArgs e)
        {
            PlayerView J = getCurrentPlayer();
            UnitView u = map.SelectedBox.SelectedUnit;
            map.SelectedBox.SelectedUnit = null;
            int index = J.peuple.units.IndexOf(u);
            UnitView unit = J.peuple.units[(index + 1) % J.peuple.units.Count];
            map.SelectedBox = map.cases.Where(b => b.Row == map.carte.getX(unit.unit.pos) && b.Column == map.carte.getY(unit.unit.pos)).First();
            map.SelectedBox.SelectedUnit = unit;
        }
    }
}
