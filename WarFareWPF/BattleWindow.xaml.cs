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
using System.Threading;
using System.Windows.Threading;

namespace WarFareWPF
{
    /// <summary>
    /// Logique d'interaction pour BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        public BattleView battle { get; set; }
        public String background
        {
            get;
            set;
        }
        
        public BattleWindow(BattleCmd battle, GameWindow gw)
        {
            this.battle = new BattleView(battle);
            this.gw = gw;
            switch (battle.box.box.getType())
            {
                case EnumCase.DESERT:
                    background = "res/desertBack.png";
                    break;
                case EnumCase.FORET:
                    background = "res/forestBack.png";
                    break;
                case EnumCase.MONTAGNE:
                    background = "res/mountBack.png";
                    break;
                case EnumCase.PLAINE:
                    background = "res/landBack.png";
                    break;
                case EnumCase.MER:
                    background = "res/seaBack.png";
                    break;
                case EnumCase.MARAIS:
                    background = "res/swampBack.png";
                    break;
            }
            InitializeComponent();
        }
        
        private void _this_ContentRendered(object sender, EventArgs e)
        {
            MyTask task = new MyTask(() => battle.Do());
            if (gw.getPc())
            {
                task.WaitWithPumping();
            }
            //Thread.Sleep(500);
            //this.Close();
        }


        public GameWindow gw { get; set; }
    }
}
