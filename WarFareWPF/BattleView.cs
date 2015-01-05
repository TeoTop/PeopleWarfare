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

        private bool _draw_loss;
        public bool draw_loss
        {
            get
            {
                return _draw_loss;
            }
            set
            {
                _draw_loss = value;
                RaisePropertyChanged("draw_loss");
            }
        }

        private bool _draw_victory;
        public bool draw_victory
        {
            get
            {
                return _draw_victory;
            }
            set
            {
                _draw_victory = value;
                RaisePropertyChanged("draw_victory");
            }
        }
        private bool _loose_point_att;
        public bool loose_point_att
        {
            get
            {
                return _loose_point_att;
            }
            set
            {
                _loose_point_att = value;
                RaisePropertyChanged("loose_point_att");
            }
        }
        private bool _loose_point_def;
        public bool loose_point_def
        {
            get
            {
                return _loose_point_def;
            }
            set
            {
                _loose_point_def = value;
                RaisePropertyChanged("loose_point_def");
            }
        }
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
                    this.loose_point_def = true;
                    this.draw_victory = true;
                    round.value = "Défenseur perd 1 pv";
                    break;
                case EnumBattle.CBT_DRAW_LOSS:
                    this.loose_point_att = true;
                    this.draw_loss = true;
                    round.value = "Attaquant perd 1 pv";
                    break;
                case EnumBattle.CBT_LOSS:
                    if (!fight.uniteAtt.survive(gv.getCurrentPlayer().peuple.peuple.getType()))
                    {
                        this.loose_point_att = true;
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
                        this.loose_point_def = true;
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
                        this.loose_point_def = true;
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
            RaisePropertyChanged("unitAtt");
            RaisePropertyChanged("unitDef");
            return battle;
        }

        private void reset()
        {
            this.draw_victory = false;
            this.draw_loss = false;
            this.loss = false;
            this.victory_move = false;
            this.victory_nomove = false;
            this.loose_point_att = false;
            this.loose_point_def = false;
        }

        public void Do()
        {
            EnumBattle battle;
            bool res;
            do
            {
                this.new_round = true;
                this.fight.incCbt();
                RaisePropertyChanged("nRound");
                
                Thread.Sleep(500);


                battle = this.attaquer();

                this.new_round = false;

                Thread.Sleep(1000);

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
