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
        public string Type { get; set; }
        public string Color { get; set; }

        public PeopleView(PeupleA peuple)
        {
            this.peuple = peuple;
            units = new List<UnitView>();
            for (int i = 0; i < peuple.getNbUnite(); i++)
            {
                units.Add(new UnitView((UniteImp)peuple.getUnite(i)));
            }
            selectedUnit = units[peuple.uniteActuel];
            switch (peuple.getType())
            {
                case EnumPeuple.ELF: this.Type = "Elf"; this.Color = "#ff0000"; this.Src = "res/elf.png"; break;
                case EnumPeuple.NAIN: this.Type = "Nain"; this.Color = "#00ff00"; this.Src = "res/nain.png"; break;
                case EnumPeuple.ORC: this.Type = "Orc"; this.Color = "#0000ff"; this.Src = "res/orc.png"; break;
            }
        }


    }
}
