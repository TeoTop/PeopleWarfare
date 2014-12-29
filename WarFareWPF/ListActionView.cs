using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeopleWar;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WarFareWPF
{
    public class ListActionView : Notifier
    {
        public List<TourImp> tours { get; set; }

        public StackPanel panel { get; set; }

        public String autoSave { get; set; }

        public ListActionView(List<TourImp> tours, String autoSave)
        {
            this.tours = tours;
            this.autoSave = autoSave;

            panel = new StackPanel();
            foreach (var t in tours)
            {
                Label l = new Label();
                l.Content = t.ToString();
                l.FontSize = 13;
                l.Height = 20;
                l.Padding = new Thickness(10,0,0,0);
                //l.MouseDoubleClick();
                panel.Children.Add(l);
            }
        }
    }
}
