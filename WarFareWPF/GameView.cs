﻿using Microsoft.Win32;
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
        public bool alreadyFinished { get; set; }
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
            get { return game.nbTour + "/" + game.nbTourMax; }
        }
        public EnumBattle battle { get; set; }
        public bool FinPartie { get; set; }
        public GameView(PartieImp game, GameWindow gw)
        {
            this.game = game;
            this.gw = gw;
            this.map = new MapView(game.carte, this);
            J1 = new PlayerView(game.j1, this.map);
            J2 = new PlayerView(game.j2, this.map);
            J1.peuple.units.ForEach(unit => map.cases[unit.unit.pos].unitsJ1.Add(unit));
            J2.peuple.units.ForEach(unit => map.cases[unit.unit.pos].unitsJ2.Add(unit));
            this.al = new ActionListView(game);
            FinPartie = false;
            getCurrentPlayer().IsMyTurn = true;
            alreadySaved = false;
            alreadyFinished = false;
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
                                move.uniteDep = unit.unit;
                                box.addUnit(unit, CurrentPlayer);
                                SelectedBoxForUnit.removeUnit(unit, CurrentPlayer);
                                break;
                            case EnumMove.NOMOVE:
                                break;
                            case EnumMove.CBT:
                                BattleCmd battleCmd = new BattleCmd(unit, getOtherPlayer().peuple.Select(move.unites), box, this);
                                battleCmd.Do();
                                BattleWindow battleWin = new BattleWindow(battleCmd, gw);
                                battleWin.Show();
                                battleWin.Topmost = true;
                                switch (this.battle)
                                {
                                    case EnumBattle.CBT_DRAW:
                                        break;
                                    case EnumBattle.CBT_LOSS:
                                        if (getOtherPlayer().peuple.peuple.getType() == EnumPeuple.ORC)
                                        {
                                            battleCmd.unitDef.unit.point += 1;
                                        }
                                        SelectedBoxForUnit.destroyUnit(battleCmd.unitAtt.unit, CurrentPlayer);
                                        getCurrentPlayer().peuple.destroy(battleCmd.unitAtt.unit);
                                        break;
                                    case EnumBattle.CBT_VICTORY_MOVE:
                                        if (getCurrentPlayer().peuple.peuple.getType() == EnumPeuple.ORC)
                                        {
                                            battleCmd.unitAtt.unit.point += 1;
                                        }
                                        box.destroyUnit(battleCmd.unitDef.unit, OtherPlayer);
                                        getOtherPlayer().peuple.destroy(battleCmd.unitDef.unit);
                                        unit.unit.move(toCase);
                                        box.addUnit(unit, CurrentPlayer);
                                        SelectedBoxForUnit.removeUnit(unit, CurrentPlayer);
                                        break;
                                    case EnumBattle.CBT_VICTORY_NOMOVE:
                                        if (getCurrentPlayer().peuple.peuple.getType() == EnumPeuple.ORC)
                                        {
                                            battleCmd.unitAtt.unit.point += 1;
                                        }
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
                        this.getOtherPlayer().RaisePropertyChanged("nbPoints");
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
                alreadyFinished = true;
                return true;
            }
            return false;
        }
        public void NextTurn(object sender, RoutedEventArgs e)
        {
            this.getCurrentPlayer().peuple.reset();
            this.switchPlayer();
            if (!this.verifierFinPartie())
            {
                al.addTurn(this.game);
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
                    int index = -1;
                    if (u != null)
                        index = J.peuple.units.IndexOf(u);
                    UnitView unit = units[(index + 1) % units.Count];
                    map.SelectedBox = map.cases.Where(b => b.Row == map.carte.getX(unit.unit.pos) && b.Column == map.carte.getY(unit.unit.pos)).First();
                    map.SelectedBox.SelectedUnit = unit;
                }
            }
        }
        public List<int[,]> att_sug(BoxView boxView)
        {
            WrapperAlgos w = new WrapperAlgos();
            int[] boxes = map.cases.Select(b => (int)b.box.getType()).ToArray();
            int nbCase = map.cases.Count;
            List<UnitView> unitsEnnemi = getOtherPlayer().peuple.units;
            int[] posEnnemi = map.cases.Join(unitsEnnemi, box => box.Uid, unit => unit.unit.pos, (box, unit) => box.Uid).ToArray();
            int nbEnn = posEnnemi.Count();
            int posActuelle = boxView.Uid;
            int typeUnite = (int)getCurrentPlayer().peuple.peuple.getType();
            double pm = boxView.SelectedUnit.pm;
            int[,] cases_atteignables = w.cases_atteignable(boxes, nbCase, posEnnemi, nbEnn, posActuelle, typeUnite, pm);
            int nb = (int)Math.Sqrt(nbCase);
            int[,] cases_suggerees = w.suggestion_cases((int[,])cases_atteignables.Clone(), cases_atteignables.Length / 3, nb, posEnnemi, nbEnn, posActuelle, typeUnite);
            map.cases.ForEach(b =>
        {
            if (this.equal(b.Uid, cases_atteignables))
            {
                b.Atteignable = true;
            }
            if (this.equal(b.Uid, cases_suggerees))
            {
                b.Suggeree = true;
            }
        });
            List<int[,]> boxs = new List<int[,]>();
            boxs.Add(cases_atteignables);
            boxs.Add(cases_suggerees);
            return boxs;
        }
        private bool equal(int p, int[,] cases)
        {
            for (int i = 0; i < cases.Length / 3; i++)
            {
                if (p == cases[i, 0])
                    return true;
            }
            return false;
        }
        public void DoRound()
        {
            while (!getCurrentPlayer().endTurn())
            {
                Thread.Sleep(500);
                // choisir unité
                UnitView unit = getUnit();
                // choisir case atteignable
                BoxView box = getBox(unit);
                // jouer
                Thread.Sleep(500);
                DoTurn(box, unit);
            }
        }
        private void DoTurn(BoxView box, UnitView unit)
        {
            box.SelectedUnit = unit;
            map.SelectedBox = box;
            map.SelectedBoxForUnit = box;
            List<int[,]> boxes = att_sug(box);
            int b = -1;
            if (boxes[1].Length / 3 != 0)
            {
                b = chooseFrom2DArray(boxes[1]);
            }
            else if (boxes[0].Length / 3 != 0)
            {
                b = chooseFrom2DArray(boxes[0]);
            }
            if (b != -1)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    MoveUnit(null, null, box, b);
                }));
            }
        }
        private BoxView getBox(UnitView unit)
        {
            return map.cases.Where(b => b.Uid == unit.unit.pos).First();
        }
        private UnitView getUnit()
        {
            return getCurrentPlayer().peuple.units.Where(u => !u.HasAlreadyPlayed).First();
        }
        private int chooseFrom2DArray(int[,] p)
        {
            Random rnd = new Random();
            return p[rnd.Next(p.Length / 3), 0];
        }
        public GameWindow gw { get; set; }
    }
}

