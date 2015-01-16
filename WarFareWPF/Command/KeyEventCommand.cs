using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WarFareWPF;

namespace WarFareWPF.Command
{
    public class KeyEventCommand
    {

        public Dictionary<KeyClass, Action> actions { get; set; }

        public KeyEventCommand()
        {
            KeyEquality keyEquality = new KeyEquality();
            actions = new Dictionary<KeyClass, Action>(keyEquality);
        }

        public void addAction(KeyClass key, Action action)
        {
            actions.Add(key, action);
        }

        public void addNumPadAction(GameView game, KeyClass key, Func<int,int,int,int> calcul)
        {
            actions.Add(key, () =>
            {
                int nbCase = game.map.carte.nbCase;
                int nbRow = (int)Math.Sqrt(nbCase);
                Func <int,int,int,int> action = (x, y, z) => calcul(x, y, z);
                if (game.map.SelectedBoxForUnit != null && game.map.SelectedBoxForUnit.SelectedUnit != null)
                {
                    int c = action(game.map.SelectedBox.Uid, nbRow, nbCase);
                    if (c < 0 || c >= nbCase) return;
                    // move unit
                    game.MoveUnit(null, null, game.map.SelectedBoxForUnit, c);
                }
                else if (game.map.SelectedBox != null)
                {
                    int c = action(game.map.SelectedBox.Uid, nbRow, nbCase);
                    if (c < 0 || c >= nbCase) return;
                    // move selectedBox
                    game.map.MoveSelectedBox(action(game.map.SelectedBox.Uid, nbRow, nbCase));
                }
                else
                {
                    if (game.map.cases.Count > 0)
                    {
                        game.map.SelectedBox = game.map.cases[0];
                    }
                }
            });
        }
    }
}
