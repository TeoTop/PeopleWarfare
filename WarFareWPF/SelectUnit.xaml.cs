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
            this.units = new List<UnitView>();
            foreach (var unit in units)
            {
                if (!unit.HasAlreadyPlayed)
                {
                    this.units.Add(unit);
                }
            }
            this.box = box;
            this.gw = gw;
            if (this.units.Count == 1)
            {
                selectUnit(0);
                return;
            }
            InitializeComponent();
        }


        public void selectUnit(int selectedIndex = -1)
        {
            int Selected = (selectedIndex == -1) ? this.items.SelectedIndex : selectedIndex;
            //gw.isUnitSelected = false;
            if (Selected >= 0 && Selected < this.units.Count)
            {
                box.SelectedUnit = units[Selected];
                //gw.isUnitSelected = true;
                //gw.map.SelectedBoxForUnit = this.box;
            }
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
