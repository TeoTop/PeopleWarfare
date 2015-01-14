using Microsoft.Win32;

using System;

using System.Collections.Generic;

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

using Wrapper;

namespace WarFareWPF

{

    public class GameView : Notifier

    {

        public PartieImp game { get; set; }

        public MapView map { get; set; }

        public ActionListView al { get; set; }

        public bool alreadySaved { get; set; }

        public string fileName { get; set; }

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

        /*public UnitView SelectedUnit

        {

            get

            {

                return map.cases.Where(b => b.SelectedUnit != null).First().SelectedUnit;

;            }

        }*/



        public GameView(PartieImp game)

        {

            this.game = game;

            this.map = new MapView(game.carte, this);

            J1 = new PlayerView(game.j1, this.map);

            J2 = new PlayerView(game.j2, this.map);

            J1.peuple.units.ForEach(unit => map.cases[unit.unit.pos].unitsJ1.Add(unit));

            J2.peuple.units.ForEach(unit => map.cases[unit.unit.pos].unitsJ2.Add(unit));

            this.al = new ActionListView(game.tours);

            FinPartie = false;

            getCurrentPlayer().IsMyTurn = true;

            alreadySaved = false;

            fileName = "";

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



        public void MoveUnit(object sender, MouseButtonEventArgs e, BoxView SelectedBoxForUnit = null, int toCase = 0)

        {

            BoxView box;

            if (SelectedBoxForUnit == null)

            {

                Grid g = sender as Grid;

                toCase = System.Convert.ToInt32(g.Tag);

                SelectedBoxForUnit = map.SelectedBoxForUnit;

            }

            box = map.cases[toCase];

            // deplacer unité

            if (!FinPartie)

            {

                if (SelectedBoxForUnit != null)

                {
                    UnitView unit = SelectedBoxForUnit.SelectedUnit;
                    if (unit != null)
                    {

                        MoveImp move = (MoveImp)unit.unit.seDeplacer(SelectedBoxForUnit.Uid, toCase, getCurrentPlayer().peuple.peuple.getType(), map.carte, getOtherPlayer().peuple.peuple);

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

                                BattleCmd battleCmd = new BattleCmd(unit, getOtherPlayer().peuple.Select(move.unites), this);

                                battleCmd.Do();



                                BattleWindow battleWin = new BattleWindow(battleCmd);

                                battleWin.Show();

                                switch (this.battle)
                                {

                                    case EnumBattle.CBT_DRAW:

                                        break;

                                    case EnumBattle.CBT_LOSS:

                                        SelectedBoxForUnit.destroyUnit(battleCmd.unitAtt.unit, CurrentPlayer);

                                        getCurrentPlayer().peuple.destroy(battleCmd.unitAtt.unit);

                                        break;

                                    case EnumBattle.CBT_VICTORY_MOVE:

                                        box.destroyUnit(battleCmd.unitDef.unit, OtherPlayer);

                                        getOtherPlayer().peuple.destroy(battleCmd.unitDef.unit);

                                        unit.unit.move(toCase);

                                        box.addUnit(unit, CurrentPlayer);

                                        SelectedBoxForUnit.removeUnit(unit, CurrentPlayer);

                                        break;

                                    case EnumBattle.CBT_VICTORY_NOMOVE:

                                        box.destroyUnit(battleCmd.unitDef.unit, OtherPlayer);

                                        getOtherPlayer().peuple.destroy(battleCmd.unitDef.unit);

                                        break;

                                }
                                move.combattre(battleCmd.fight);
                                // ne sert à rien

                                battleCmd.RaisePropertyChanged("unitDef");

                                break;

                        }

                        SelectedBoxForUnit.SelectedUnit = null;

                        // ces raise servent lorsqu'il y a cbt et le défenseur ne meurt pas => c'est pour actualiser sa vie

                        // Mais les raise ne fonctionnent pas

                        /*SelectedBoxForUnit.notifyUnits();

                        box.notifyUnits();*/

                        this.getCurrentPlayer().RaisePropertyChanged("nbPoints");
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

            //map.resetBoxes();

            this.getCurrentPlayer().peuple.reset();

            this.switchPlayer();

            /*if (map.SelectedBox != null)

            {

                //map.SelectedBox.notifyUnits();

            }*/

            game.tours.Add(new TourImp()); // à changer lors de l'implementation des tours en ajoutant une classe TourView et ici on ajoute TourView.tour

            if (!this.verifierFinPartie())

            {

                RaisePropertyChanged("nbTour");

            }

        }

        public void save()

        {

            try

            {

                SaveFileDialog saveFileDialog = null;
                bool saveFromDialog = false;

                bool dialog = true;

                if (fileName == "")

                {

                    saveFileDialog = new SaveFileDialog();

                    saveFileDialog.FileName = "peopleWarfare_save";     //nom par default du fichier

                    saveFileDialog.Filter = "PeopleWarfare (*.ppw)|*.ppw|Tous les fichiers (*.*)|*.*";  //liste des extension

                    saveFileDialog.FilterIndex = 1;     // met ppw comme extension par default, 2 pour l'autre

                    saveFileDialog.Title = "Sauvegarde de partie de PeopleWarfare";  //titre de la fenetre

                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // trouver le dossier bureau

                    dialog = (bool)saveFileDialog.ShowDialog();
                    saveFromDialog = true;

                }

                if (dialog)

                {
                    if (saveFromDialog)
                    {
                        fileName = saveFileDialog.FileName;
                    }

                    IFormatter formatter = new BinaryFormatter();

                    Stream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);

                    formatter.Serialize(stream, game);

                    stream.Close();

                    this.alreadySaved = true;

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show("Erreur lors de la sauvegarde: " + ex.Message);

            }

        }



        public void saveAs()

        {

            fileName = "";

            this.save();

        }



        public void NextUnit(object sender, RoutedEventArgs e)

        {

            PlayerView J = getCurrentPlayer();

            List<UnitView> units = J.peuple.units.Where(unite => !unite.HasAlreadyPlayed).ToList();

            if (units.Count > 0)

            {
                if (map != null && map.SelectedBox != null)
                {
                    UnitView u = map.SelectedBox.SelectedUnit;

                    map.SelectedBox.SelectedUnit = null;

                    int index = J.peuple.units.IndexOf(u);

                    UnitView unit = units[(index + 1) % units.Count];

                    map.SelectedBox = map.cases.Where(b => b.Row == map.carte.getX(unit.unit.pos) && b.Column == map.carte.getY(unit.unit.pos)).First();

                    map.SelectedBox.SelectedUnit = unit;
                }

            }

        }

        public void cases_atteignables(BoxView boxView)

        {

            WrapperAlgos w = new WrapperAlgos();

            //int[][] cases_atteignables = w.suggestion_cases();

        }

    }

}

