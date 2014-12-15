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
        
        public BattleWindow(UnitView unit, List<UnitView> list, GameView gv)
        {
            battle = new BattleView(unit, list, gv);
            InitializeComponent();
        }
        
        private void _this_ContentRendered(object sender, EventArgs e)
        {
            MyTask task = new MyTask(() => battle.Do());
            task.WaitWithPumping();
        }
        
    }
}
