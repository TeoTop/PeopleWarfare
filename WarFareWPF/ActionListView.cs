using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleWar;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using PeopleWar;

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

        public ActionListView(PartieImp partie)
        {
            this.tours = partie.tours;
            actions = new ObservableCollection<ExpanderView>();
            if (tours.Count != 0)
            {
                tours.ForEach(t => loadTurn(t.mouvements, partie));
            }
            else
            {
                this.addTurn(partie);
            }
        }

        public void addAction(MoveImp move)
        {
            tours.Last().mouvements.Add(move);
            loadAction(move);
        }

        public void addTurn(PartieImp partie)
        {
            tours.Add(new TourImp());
            JoueurImp j;
            if(partie.joueurCourant == 0){
                j = partie.j1;
            } else {
                j = partie.j2;
            }
            String header = "Tour "+ partie.getNbTour() +" de " + j.nom;
            actions.Add(new ExpanderView(header));
        }

        public void loadAction(MoveImp move)
        {
            String label = "null";
            if (move.combat != null)
            {
                String att = move.combat.uniteAtt.nom;
                String def = move.combat.uniteDef.nom;
                label = att +" attaque " + def;
                actions.Last().Labels.Add(label);

                if (move.combat.resultat == EnumBattle.CBT_VICTORY_MOVE || move.combat.resultat == EnumBattle.CBT_VICTORY_NOMOVE)
                {
                    def = move.combat.uniteDef.nom;
                    label = def + " est mort";
                    actions.Last().Labels.Add(label);
                }

                if (move.combat.resultat == EnumBattle.CBT_LOSS)
                {
                    att = move.combat.uniteAtt.nom;
                    label = att + " est mort";
                    actions.Last().Labels.Add(label);
                }
                
            }
            else
            {
                String dep = move.uniteDep.nom;
                label = "Déplacement de " + dep;
                actions.Last().Labels.Add(label);
            }
        }

        public void loadTurn(List<MoveImp> moves, PartieImp partie)
        {
            JoueurImp j;
            if (partie.joueurCourant == 0)
            {
                j = partie.j1;
            }
            else
            {
                j = partie.j2;
            }
            String header = "Tour " + partie.getNbTour() + " de " + j.nom;
            actions.Add(new ExpanderView(header));
            moves.ForEach(m => loadAction(m));
        }
    }
}