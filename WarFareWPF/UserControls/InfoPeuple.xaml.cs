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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WarFareWPF.UserControls
{
    /// <summary>
    /// Logique d'interaction pour InfoPeuple.xaml
    /// </summary>
    public partial class InfoPeuple : UserControl
    {
        public int Player
        {
            get
            {
                return (int)GetValue(Player_prop);
            }
            set
            {
                SetValue(Player_prop, value);
            }
        }
        public static readonly DependencyProperty Player_prop = DependencyProperty.Register("Player", typeof(int), typeof(InfoPeuple));
        public InfoPeuple()
        {
            InitializeComponent();
        }
    }
}
