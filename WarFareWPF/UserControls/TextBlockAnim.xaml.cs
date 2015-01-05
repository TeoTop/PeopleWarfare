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
    /// Logique d'interaction pour TextBlockAnim.xaml
    /// </summary>
    public partial class TextBlockAnim : UserControl
    {
        public bool loss_or_victory
        {
            get
            {
                return (bool)GetValue(loss_or_victory_prop);
            }
            set
            {
                SetValue(loss_or_victory_prop, value);
            }
        }
        public static readonly DependencyProperty loss_or_victory_prop = DependencyProperty.Register("loss_or_victory", typeof(bool), typeof(TextBlockAnim));

        public TextBlockAnim()
        {
            InitializeComponent();
        }
    }
}
