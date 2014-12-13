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
        public String carac
        {
            get { return unit.afficherCaracteristique(); }
        }

        public UnitView(UniteImp unit)
        {
            this.unit = unit;
            IsSelected = false;
            HasAlreadyPlayed = false;
        }
    }
}
