using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using PeopleWar;

using System.Windows;

using System.Windows.Input;

using System.Collections.ObjectModel;

using Wrapper;

namespace WarFareWPF

{

    public class BoxView : Notifier

    {

        public CaseA box { set;  get; }

        public ObservableCollection<UnitView> unitsJ1 { get; set; }

        public ObservableCollection<UnitView> unitsJ2 { get; set; }

        private UnitView selectedUnit;

        public UnitView SelectedUnit

        {

            get

            {

                return selectedUnit;

            }

            set

            {
                map.resetAttSugg();
                selectedUnit = value;

                if (map.SelectedBoxForUnit != null && map.SelectedBoxForUnit != this)

                {

                    BoxView box = map.SelectedBoxForUnit;

                    map.SelectedBoxForUnit = null;

                    box.SelectedUnit = null;

                }

                map.SelectedBoxForUnit = this;





                // cases atteignables et suggérées
                if (SelectedUnit != null)
                    map.att_sug(this);



                RaisePropertyChanged("IsUnitSelected");

                RaisePropertyChanged("SelectedUnit");

            }

        }

        public bool IsUnitSelected

        {

            get

            {

                return SelectedUnit != null;

            }

        }

        public int NbUniteJ1

        {

            get

            {

                return unitsJ1.Count;

            }

        }

        public int NbUniteJ2

        {

            get

            {

                return unitsJ2.Count;

            }

        }



        public void addUnit(UnitView unit, int player)

        {

            if (player == 0)

            {

                unitsJ1.Add(unit);

                RaisePropertyChanged("NbUniteJ1");

                RaisePropertyChanged("HasUnitJ1");

                RaisePropertyChanged("unitsJ1");

            }

            else

            {

                unitsJ2.Add(unit);

                RaisePropertyChanged("NbUniteJ2");

                RaisePropertyChanged("HasUnitJ2");

                RaisePropertyChanged("unitsJ2");

            }

        }



        public void removeUnit(UnitView unit, int player)

        {

            if (player == 0)

            {

                unitsJ1.Remove(unit);

                RaisePropertyChanged("NbUniteJ1");

                RaisePropertyChanged("HasUnitJ1");

                RaisePropertyChanged("unitsJ1");

            }

            else

            {

                unitsJ2.Remove(unit);

                RaisePropertyChanged("NbUniteJ2");

                RaisePropertyChanged("HasUnitJ2");

                RaisePropertyChanged("unitsJ2");

            }

        }

        



        public List<UnitView> getUnits(int player)

        {

            return ((player == 0) ? unitsJ1 : unitsJ2).ToList();

        }

        public int Uid { get; set; }

        public MapView map { get; set; }

        public BoxView(int row, int column, int i, CaseA box, MapView map)

        {

            this.box = box;

            switch (box.getType())

            {

                case EnumCase.DESERT: this.Type = "Desert"; this.Src = "res/desert.png"; break;

                case EnumCase.FORET: this.Type = "Foret"; this.Src = "res/foret.png"; break;

                case EnumCase.MONTAGNE: this.Type = "Montagne"; this.Src = "res/montagne.png"; break;

                case EnumCase.PLAINE: this.Type = "Plaine"; this.Src = "res/plaine.png"; break;

            }

            this.Row = row;

            this.Column = column;

            this.Uid = i;

            unitsJ1 = new ObservableCollection<UnitView>();

            unitsJ2 = new ObservableCollection<UnitView>();

            this.map = map;

        }



        public string Src { get; set; }

        public String Type { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public bool HasUnitJ1

        {

            get { return NbUniteJ1 > 0; }

        }

        public bool HasUnitJ2

        {

            get { return NbUniteJ2 > 0; }

        }

        private bool isSelected;

        public bool IsSelected

        {

            get { return isSelected; }

            set

            {

                isSelected = value;

                RaisePropertyChanged("IsSelected");

            }

        }

        private bool atteignable;

        public bool Atteignable

        {

            get

            {

                return atteignable;

            }

            set

            {

                atteignable = value;

                RaisePropertyChanged("Atteignable");

            }

        }

        private bool suggeree;

        public bool Suggeree
        {

            get
            {

                return suggeree;

            }

            set
            {

                suggeree = value;

                RaisePropertyChanged("Suggeree");

            }

        }

        public bool RowPair

        {

            get

            {

                return Row % 2 == 0;

            }

        }

        public bool RowPairReverse

        {

            get

            {

                return Row % 2 != 0;

            }

        }

        public override string ToString()

        {

            return "(" + Row + ", " + Column + ")" + ", Type : " + Type;

        }



        public void destroyUnit(UniteImp uniteImp, int player)

        {

            List<UnitView> units = getUnits(player);

            UnitView unite = units.Where(unit => unit.unit == uniteImp).First();

            this.removeUnit(unite, player);

        }



        public void notifyUnits()

        {

            RaisePropertyChanged("unitsJ1");

            RaisePropertyChanged("NbUniteJ1");

            RaisePropertyChanged("unitsJ2");

            RaisePropertyChanged("NbUniteJ2");

        }

    }

}

