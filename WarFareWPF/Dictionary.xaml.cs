﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WarFareWPF
{
    public partial class Dictionary
    {
        public static GameWindow gw { get; set; }
        public void MoveUnit(object sender, MouseButtonEventArgs e)
        {
            gw.MoveUnit(sender, e);
        }

    }
}
