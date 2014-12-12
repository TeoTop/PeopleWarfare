﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeopleWar;
using System.Windows;
using System.Windows.Input;
namespace WarFareWPF
{
    public class BoxView : Notifier
    {
        private CaseA box { set;  get; }
        public List<UnitView> unitsJ1;
        public List<UnitView> unitsJ2;
        public UnitView SelectedUnit { get; set; }
        public int NbUniteJ1
        {
            get
            {
                return unitsJ1.Count;
            }
            set
            {
                //_nbUniteJ1 = value;
                RaisePropertyChanged("NbUniteJ1");
                RaisePropertyChanged("HasUnitJ1");
            }
        }
        public int NbUniteJ2
        {
            get
            {
                return unitsJ2.Count;
            }
            set
            {
                //_nbUniteJ2 = value;
                RaisePropertyChanged("NbUniteJ2");
                RaisePropertyChanged("HasUnitJ2");
            }
        }

        public void addUnit(UnitView unit, int player)
        {
            if (player == 0)
            {
                unitsJ1.Add(unit);
                RaisePropertyChanged("NbUniteJ1");
                RaisePropertyChanged("HasUnitJ1");}
            else
            {
                unitsJ2.Add(unit);
                RaisePropertyChanged("NbUniteJ2");
                RaisePropertyChanged("HasUnitJ2");
            }
        }

        public void removeUnit(UnitView unit, int player)
        {
            if (player == 0)
            {
                unitsJ1.Remove(unit);
                RaisePropertyChanged("NbUniteJ1");
                RaisePropertyChanged("HasUnitJ1");
            }
            else
            {
                unitsJ2.Remove(unit);
                RaisePropertyChanged("NbUniteJ2");
                RaisePropertyChanged("HasUnitJ2");
            }
        }

        public List<UnitView> getUnits(int player)
        {
            return (player == 0) ? unitsJ1 : unitsJ2;
        }
        public int Uid { get; set; }

        public BoxView(int row, int column, int i, CaseA box)
        {
            this.box = box;
            switch (box.getType())
            {
                case EnumCase.DESERT: this.Type = "Desert"; this.Src = "res/desert.png"; break;
                case EnumCase.FORET: this.Type = "Foret"; this.Src = "res/foret.png"; break;
                case EnumCase.MONTAGNE: this.Type = "Montagne"; this.Src = "res/ocean.png"; break; // à modifier
                case EnumCase.PLAINE: this.Type = "Plaine"; this.Src = "res/plaine.png"; break;
            }
            this.Row = row;
            this.Column = column;
            this.Uid = i;
            unitsJ1 = new List<UnitView>();
            unitsJ2 = new List<UnitView>();
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
    }
}
