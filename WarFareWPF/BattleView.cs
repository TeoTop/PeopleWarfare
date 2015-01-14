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

        public BattleCmd battleCmd { get; set; }

        private RoundView _currentRound;

        public RoundView currentRound

        {

            get

            {

                return _currentRound;

            }

            set

            {

                _currentRound = value;

                RaisePropertyChanged("currentRound");

            }

        }



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

        public int _nRound;

        public int nRound

        {

            get { return _nRound; }

            set

            {

                _nRound = value;

                RaisePropertyChanged("nRound");

            }

        }



        public int nRoundMax { get { return battleCmd.rounds.Count; } }



        public BattleView(BattleCmd battleCmd)

        {

            this.battleCmd = battleCmd;

            currentRound = battleCmd.rounds.First();

        }



        public void attaquer(RoundView round)

        {

            switch (round.round.battle)

            {

                case EnumBattleAnim.DP1:

                    this.loose_point_def = true;

                    this.draw_victory = true;

                    break;

                case EnumBattleAnim.AP1:

                    this.loose_point_att = true;

                    this.draw_loss = true;

                    break;

                case EnumBattleAnim.AM:

                    this.loose_point_att = true;

                    this.loss = true;

                    break;

                case EnumBattleAnim.AS:

                    break;

                case EnumBattleAnim.DMAPSP:

                    this.loose_point_def = true;

                    this.victory_move = true;

                    break;

                case EnumBattleAnim.DS:

                    break;

                case EnumBattleAnim.DMANPSP:

                    this.loose_point_def = true;

                    this.victory_nomove = true;

                    break;

            }

            RaisePropertyChanged("unitAtt");

            RaisePropertyChanged("unitDef");

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

            int i = 0;

            battleCmd.rounds.ForEach(round =>

            {
                this.new_round = true;

                i++;

                nRound = i;

                Thread.Sleep(500);



                this.attaquer(round);

                currentRound = round;



                this.new_round = false;



                Thread.Sleep(1000);



                this.reset();

            });

            

            this.end = true;



            Thread.Sleep(500);

        }

    }

}

