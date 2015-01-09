using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeopleWar;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
namespace WarFareWPF
{
    public class MapView : Notifier
    {
        public StrategieCarte carte { get; set; }
        public double Columns
        {
            get { return Math.Sqrt(carte.nbCase); }
        }
        private List<BoxView> _cases = new List<BoxView>();
        public List<BoxView> cases
        {
            get { return _cases; }
        }
        public BoxView SelectedBoxForUnit { get; set; }
        private BoxView selectedBox;
        public BoxView SelectedBox
        {
            get { return selectedBox; }
            set
            {
                if (selectedBox != null) selectedBox.IsSelected = false;
                selectedBox = value;
                if (selectedBox != null) selectedBox.IsSelected = true;
                RaisePropertyChanged("IsBoxSelected");
                RaisePropertyChanged("SelectedBox");
            }
        }
        public bool IsBoxSelected
        {
            get { return SelectedBox != null; }
        }

        public int _zoom;
        public int Zoom
        {
            get
            {
                return _zoom * 5;
            }
            set
            {
                value /= 5;
                Double val = value + 0.2;
                _zoom = (int)((val <= 80) ? (value >= 0) ? value : 0 : 80);
                RaisePropertyChanged("Zoom");
            }
        }
        public List<Point> DefaultSizePoint { get; set; }
        public MapView(StrategieCarte map)
        {
            carte = map;
            this.DefaultSizePoint = new List<Point>();
            DefaultSizePoint.Add(new Point(0, 0));
            DefaultSizePoint.Add(new Point(40, 20));
            DefaultSizePoint.Add(new Point(80, 0));
            DefaultSizePoint.Add(new Point(80, -40));
            DefaultSizePoint.Add(new Point(40, -60));
            DefaultSizePoint.Add(new Point(0, -40));
            int i = 0;
            foreach (var box in carte.cases)
            {
                BoxView boxview = new BoxView(carte.getX(i), carte.getY(i), i, box, this);
                _cases.Add(boxview);
                i++;
            }

            /*int nbCase = map.nbCase;
            int dim = (int)Math.Sqrt(nbCase);
            Double height = Application.Current.Windows.OfType<GameWindow>().First().SB.ActualHeight;

            Double poss = height / dim;

            Zoom = (int)(100 / 80 * poss);*/

            Zoom = 100;
        }

        public override string ToString()
        {
            String toReturn = "";
            foreach (var box in _cases)
            {
                toReturn += box.ToString() + "\n";
            }
            return toReturn;
        }
        public void resetBoxes()
        {
            cases.ForEach(box => box.SelectedUnit = null);
        }

        public void MoveSelectedBox(int p)
        {
            SelectedBox = cases.Where(b => b.box == carte.getCase(p)).First();
        }
    }
}
