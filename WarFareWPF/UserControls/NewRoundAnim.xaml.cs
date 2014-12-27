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
    /// Logique d'interaction pour NewRoundAnim.xaml
    /// </summary>
    public partial class NewRoundAnim : UserControl
    {
        public bool Show
        {
            get
            {
                return (bool)GetValue(Show_prop);
            }
            set
            {
                SetValue(Show_prop, value);
            }
        }
        public static readonly DependencyProperty Show_prop = DependencyProperty.Register("Show", typeof(bool), typeof(NewRoundAnim));

        public String Text
        {
            get
            {
                return (String)GetValue(Text_prop);
            }
            set
            {
                SetValue(Text_prop, value);
            }
        }
        public static readonly DependencyProperty Text_prop = DependencyProperty.Register("Text", typeof(String), typeof(NewRoundAnim));
        public NewRoundAnim()
        {
            InitializeComponent();
        }
    }
}
