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

        public PlayerView(JoueurImp joueur, MapView map)
        {
            this.joueur = joueur;
            this.map = map;
            isMyTurn = false;
            peuple = new PeopleView(joueur.peuple);
        }

        public bool endTurn()
        {
            return peuple.units.Where(u => !u.HasAlreadyPlayed).Count() <= 0;
        }
    }
}
