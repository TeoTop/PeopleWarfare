using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleWar;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows;

namespace WarFareWPF
{
    public class BattleView : Notifier
    {
        public UnitView unitAtt { get; set; }
        public UnitView unitDef { get; set; }
        public CombatImp fight { get; set; }
        public GameView gv { get; set; }

        public bool draw_loss { get; set; }
        public bool draw_victory { get; set; }
        public bool loss { get; set; }
        public bool victory_move { get; set; }
        public bool victory_nomove { get; set; }
        public ObservableCollection<MyString> rounds { get; set; }
        private bool _new_round;
        public bool new_round 
        {
            get
            {
                return _new_round;
            }
            set
            {
                _new_round = value;
                RaisePropertyChanged("new_round");
            }
        }
        private bool _end;
        public bool end
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
                RaisePropertyChanged("end");
            }
        }
        public string nRound
        {
            get
            {
                return fight.nbCbt + "/" + fight.nbCbtMax;
            }
        }

        public BattleView(UnitView unit, List<UnitView> list, GameView gv)
        {
            List<Unite> unitesDef = new List<Unite>();
            list.ForEach(l => unitesDef.Add(l.unit));

            this.fight = new CombatImp(unit.unit, unitesDef);

            this.unitAtt = unit;
            this.unitDef = list.Where(l => l.unit == fight.uniteDef).First();

            this.reset();
            this.gv = gv;
            this.rounds = new ObservableCollection<MyString>();
            this.new_round = false;
        }

        public EnumBattle attaquer()
        {
            EnumBattle battle = this.fight.attaquer();
            MyString round = new MyString();
            switch (battle)
            {
                case EnumBattle.CBT_DRAW_VICTORY:
                    this.draw_victory = true;
                    round.value = "Défenseur perd 1 pv";
                    break;
                case EnumBattle.CBT_DRAW_LOSS:
                    this.draw_loss = true;
                    round.value = "Attaquant perd 1 pv";
                    break;
                case EnumBattle.CBT_LOSS:
                    if (!fight.uniteAtt.survive(gv.getCurrentPlayer().peuple.peuple.getType()))
                    {
                        this.loss = true;
                        round.value = "Attaquant meurt";
                    }
                    else
                    {
                        battle = EnumBattle.CBT_DRAW;
                        round.value = "Attaquant survit avec 1 pv";
                    }
                    break;
                case EnumBattle.CBT_VICTORY_MOVE:
                    if (!fight.uniteDef.survive(gv.getOtherPlayer().peuple.peuple.getType()))
                    {
                        this.victory_move = true;
                        round.value = "Défenseur meurt et l'attaquant prend sa place";
                    }
                    else
                    {
                        battle = EnumBattle.CBT_DRAW;
                        round.value = "Défenseur survit avec 1 pv";
                    }
                    break;
                case EnumBattle.CBT_VICTORY_NOMOVE:
                    if (!fight.uniteDef.survive(gv.getOtherPlayer().peuple.peuple.getType()))
                    {
                        this.victory_nomove = true;
                        round.value = "Défenseur meurt. L'attaquant ne prend pas sa place";
                    }
                    else
                    {
                        battle = EnumBattle.CBT_DRAW;
                        round.value = "Défenseur survit avec 1 pv";
                    }
                    break;
            }
            Action<MyString> AddItem = rounds.Add;
            Application.Current.Dispatcher.BeginInvoke(AddItem, round);

            return battle;
        }

        private void reset()
        {
            this.draw_victory = false;
            this.draw_loss = false;
            this.loss = false;
            this.victory_move = false;
            this.victory_nomove = false;
        }

        public void Do()
        {
            EnumBattle battle;
            bool res;
            do
            {
                this.new_round = true;
                
                Thread.Sleep(500);

                this.new_round = false;


                battle = this.attaquer();

                RaisePropertyChanged("nRound");
                
                Thread.Sleep(500);

                this.reset();
                res = battle == EnumBattle.CBT_VICTORY_MOVE || battle == EnumBattle.CBT_VICTORY_NOMOVE || battle == EnumBattle.CBT_LOSS;
            } while (this.fight.nbCbt < this.fight.nbCbtMax && !res);
            
            this.end = true;

            Thread.Sleep(500);

            if (!res)
            {
                battle = EnumBattle.CBT_DRAW;
            }

            gv.battle = battle;
        }


    }
}
