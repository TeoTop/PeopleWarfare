﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeopleWar;

namespace WarFareWPF
{
    public class PeopleView : Notifier
    {
        public List<UnitView> units { get; set; }

        public UnitView selectedUnit { get; set; }

        public PeupleA peuple { get; set; }
        public string Src { get; set; }

        public string Info {
            get
            {
                return peuple.getInformation();
            }
        }
        public string Type { get; set; }
        public string Color { get; set; }
        public int nbUnite
        {
            get { return peuple.getNbUnite(); }
        }

        public PeopleView(PeupleA peuple)
        {
            this.peuple = peuple;
            units = new List<UnitView>();
            switch (peuple.getType())
            {
                case EnumPeuple.ELF: this.Type = "Elf"; this.Color = "#ff0000"; this.Src = "../res/elf"+ (peuple.skin+1) +".png"; break;
                case EnumPeuple.NAIN: this.Type = "Nain"; this.Color = "#00ff00"; this.Src = "../res/nain" + (peuple.skin+1) + ".png"; break;
                case EnumPeuple.ORC: this.Type = "Orc"; this.Color = "#0000ff"; this.Src = "../res/orc" + (peuple.skin+1) + ".png"; break;
                case EnumPeuple.CHEVALIER: this.Type = "Chevalier"; this.Color = "#0000ff"; this.Src = "../res/chevalier" + (peuple.skin + 1) + ".png"; break;
                case EnumPeuple.GOLEM: this.Type = "Golem"; this.Color = "#00ff00"; this.Src = "../res/golem" + (peuple.skin + 1) + ".png"; break;
            }
            for (int i = 0; i < peuple.getNbUnite(); i++)
            {
                units.Add(new UnitView((UniteImp)peuple.getUnite(i), Src));
            }
            selectedUnit = units[peuple.uniteActuel];
        }



        public void reset()
        {
            // reset units / pm and HasAlreadyPlayed
            units.ForEach(unit => unit.HasAlreadyPlayed = false);
            units.ForEach(unit => unit.unit.reset(peuple.getType()));
        }

        internal void destroy(UniteImp uniteImp)
        {
            UnitView unite = units.Where(unit => unit.unit == uniteImp).First();
            this.destroy(unite);
            peuple.destroy(unite.unit);
            RaisePropertyChanged("nbUnite");
        }

        private void destroy(UnitView unite)
        {
            units.Remove(unite);
            unite = null;
        }

        public List<UnitView> Select(List<Unite> list)
        {
            return this.units.Where(unit => list.Contains(unit.unit)).ToList();
        }
    }
}
