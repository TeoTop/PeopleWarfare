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
        public int nbPoints
        {
            get
            {
                joueur.calculerNbPoint(map.carte);
                return joueur.nbPoints;
            }
        }
        public MapView map { get; set; }
        private bool isMyTurn;
        public bool IsMyTurn
        {
            get { return isMyTurn; }
            set
            {
                isMyTurn = value;
                RaisePropertyChanged("IsMyTurn");
            }
        }
        public PlayerView(JoueurImp joueur, Point initialPosition, MapView map)
        {
            this.joueur = joueur;
            this.map = map;
            isMyTurn = false;
            peuple = new PeopleView(joueur.peuple);
            InitialPosition = initialPosition;
        }
    }
}
