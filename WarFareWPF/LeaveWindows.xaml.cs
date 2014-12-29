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

namespace WarFareWPF
{
    /// <summary>
    /// Logique d'interaction pour LeaveWindows.xaml
    /// </summary>
    public partial class LeaveWindows : Window
    {
        public LeaveWindows()
        {
            InitializeComponent();
        }

        private void btnAnnuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnQuitter(object sender, RoutedEventArgs e)
        {

        }
    }
}
