using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeopleWar;
using System.ComponentModel;
using System.Windows;
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
                return _zoom;
            }
            set
            {
                _zoom = (int)((value <= 400) ? (value >= 0) ? value : _zoom : _zoom);
                RaisePropertyChanged("Zoom");
            }
        }
        public MapView(StrategieCarte map)
        {
            carte = map;
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
            cases.Select(box => box.SelectedUnit = null);
        }
    }
}
