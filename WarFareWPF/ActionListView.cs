using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleWar;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace WarFareWPF
{
    public class ActionListView : Notifier
    {
        public List<TourImp> tours { get; set; }
        public class ExpanderView : Notifier
        {
            public String Header { get; set; }

            private ObservableCollection<String> _Labels;
            public ObservableCollection<String> Labels
            {
                get
                {
                    return _Labels;
                }
                set
                {
                    _Labels = value;
                    RaisePropertyChanged("Labels");
                }
            }
            public ExpanderView(String header)
            {
                Header = header;
                Labels = new ObservableCollection<String>();
            }
        }
        private ObservableCollection<ExpanderView> _actions;
        public ObservableCollection<ExpanderView> actions
        {
            get
            {
                return _actions;
            }
            set
            {
                _actions = value;
                RaisePropertyChanged("actions");
            }
        }

        public ActionListView(List<TourImp> tours)
        {
            this.tours = tours;
            actions = new ObservableCollection<ExpanderView>();
            if (tours.Count != 0)
            {
                tours.ForEach(t => loadTurn(t.mouvements));
            }
            else
            {
                this.addTurn();
            }
        }

        public void addAction(MoveImp move)
        {
            tours.Last().mouvements.Add(move);
            loadAction(move);
        }

        public void addTurn()
        {
            tours.Add(new TourImp());
            String header = "Tour x de joueur y";
            actions.Add(new ExpanderView(header));
        }

        public void loadAction(MoveImp move)
        {
            String label = "null";
            if (move.combat != null)
            {
                label = "Combat entre x et y";
            }
            else
            {
                label = "Déplacement de x";
            }

            actions.Last().Labels.Add(label);
        }

        // IIIIIIIIIIIIIICCCCCCCCCCCCCCCCIIIIIIIIIIIIIII

        public void loadTurn(List<MoveImp> moves)
        {
            //addTurn();  remplacer par ce qu'il y en dessous
            String header = "Tour x de joueur y";
            actions.Add(new ExpanderView(header));

            moves.ForEach(m => loadAction(m));
        }
    }
}