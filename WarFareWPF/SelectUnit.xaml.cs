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
    /// Logique d'interaction pour SelectUnit.xaml
    /// </summary>
    public partial class SelectUnit : Window
    {
        public List<UnitView> units { get; set; }
        public BoxView box { get; set; }

        public GameWindow gw;
        public SelectUnit(BoxView box, List<UnitView> units, GameWindow gw)
        {
            this.units = units;
            this.box = box;
            this.gw = gw;
            /*if (box.SelectedUnit != null)
                MessageBox.Show(box.SelectedUnit.carac);*/
            InitializeComponent();
        }


        public void selectUnit()
        {
            int Selected = this.items.SelectedIndex;
            gw.isUnitSelected = false;
            if (Selected >= 0 && Selected < this.units.Count)
            {
                box.SelectedUnit = units[Selected];
                //units[Selected].IsSelected = true;
                gw.isUnitSelected = true;
                gw.map.SelectedBoxForUnit = this.box;
            }
            //MessageBox.Show(box.SelectedUnit.carac);
            this.Close();
        }

        private void Selectionner(object sender, RoutedEventArgs e)
        {
            selectUnit();
        }

        private void Annuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Selectionner(object sender, MouseButtonEventArgs e)
        {
            selectUnit();
        }

    }
}
