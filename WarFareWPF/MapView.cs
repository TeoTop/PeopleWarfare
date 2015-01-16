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
        public bool IsUnitSelected
        {
            get
            {
                return SelectedBoxForUnit != null && SelectedBoxForUnit.SelectedUnit != null;
            }
        }
        private BoxView selectedBoxForUnit;
        public BoxView SelectedBoxForUnit
        {
            get
            {
                return selectedBoxForUnit;
            }
            set
            {
                selectedBoxForUnit = value;
                RaisePropertyChanged("SelectedBoxForUnit");
                RaisePropertyChanged("IsUnitSelected");
            }
        }

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

        public GameView gv { get; set; }
        public List<Point> DefaultSizePoint { get; set; }
        public MapView(StrategieCarte map, GameView gv)
        {
            carte = map;
            this.gv = gv;
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
            resetAttSugg();
        }
        public void resetAttSugg()
        {
            cases.ForEach(b =>
        {
            b.Atteignable = false;
            b.Suggeree = false;
        });
        }
        public void MoveSelectedBox(int p)
        {
            SelectedBox = cases.ElementAt(p);
        }
        public void att_sug(BoxView boxView)
        {
            gv.att_sug(boxView);
        }
    }
}

