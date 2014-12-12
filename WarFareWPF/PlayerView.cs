using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleWar;
using System.Windows;

namespace WarFareWPF
{
    public class PlayerView : Notifier
    {
        public PeopleView peuple
        {
            get;
            set;
        }
        private Point initialPosition;
        public Point InitialPosition {
            get
            {
                return initialPosition;
            }
            set
            {
                initialPosition = value;
                RaisePropertyChanged("InitialPosition");
            }
        }

        public JoueurImp joueur { get; set; }
        public PlayerView(JoueurImp joueur, Point initialPosition)
        {
            this.joueur = joueur;
            peuple = new PeopleView(joueur.peuple);
            InitialPosition = initialPosition;
        }
    }
}
