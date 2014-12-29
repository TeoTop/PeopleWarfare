using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleWar;

namespace WarFareWPF
{
    public class UnitView : Notifier
    {
        public UniteImp unit;
        private bool hasAlreadyPlayed;
        public bool HasAlreadyPlayed
        {
            get
            {
                return hasAlreadyPlayed;
            }
            set
            {
                hasAlreadyPlayed = value;
                RaisePropertyChanged("HasAlreadyPlayed");
            }
        }
        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }
        public int vie
        {
            get
            {

                return unit.vie;
            }
        }
        public int attaque
        {
            get
            {
                return unit.attaque;
            }
        }
        public int defense
        {
            get
            {
                return unit.defense;
            }
        }
        public Double pm
        {
            get
            {
                return unit.pm;
            }
        }
        public String carac
        {
            get { return unit.afficherCaracteristique(); }
        }
        public PeopleView people { get; set; }

        public UnitView(UniteImp unit, PeopleView people)
        {
            this.unit = unit;
            this.people = people;
            IsSelected = false;
            HasAlreadyPlayed = false;
        }

    }
}
