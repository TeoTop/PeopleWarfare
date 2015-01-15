using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleWar;
namespace WarFareWPF
{
    public class BattleCmd: Notifier
    {
        public UnitView unitAtt { get; set; }
        public UnitView unitDef { get; set; }
        public BoxView box { get; set; }
        public CombatImp fight { get; set; }
        public List<RoundView> rounds { get; set; }
        public GameView gv { get; set; }

        
        public BattleCmd(UnitView unit, List<UnitView> list, BoxView box, GameView gv)
        {
            List<Unite> unitesDef = new List<Unite>();
            list.ForEach(l => unitesDef.Add(l.unit));

            this.fight = new CombatImp(unit.unit, unitesDef);

            this.unitAtt = unit;
            this.unitDef = list.Where(l => l.unit == fight.uniteDef).First();

            this.gv = gv;
            this.rounds = new List<RoundView>();

            this.box = box;
        }

        public EnumBattle attaquer()
        {
            EnumBattle battle = this.fight.attaquer();
            EnumBattleAnim battleAnim = new EnumBattleAnim();
            RoundView round = new RoundView();
            String message = "";
            switch (battle)
            {
                case EnumBattle.CBT_DRAW_VICTORY:
                    message = "Défenseur perd 1 pv";
                    battleAnim = EnumBattleAnim.DP1;
                    break;
                case EnumBattle.CBT_DRAW_LOSS:
                    message = "Attaquant perd 1 pv";
                    battleAnim = EnumBattleAnim.AP1;
                    break;
                case EnumBattle.CBT_LOSS:
                    if (!fight.uniteAtt.survive(gv.getCurrentPlayer().peuple.peuple.getType()))
                    {
                        message = "Attaquant meurt";
                        battleAnim = EnumBattleAnim.AM;
                    }
                    else
                    {
                        battle = EnumBattle.CBT_DRAW;
                        message = "Attaquant survit avec 1 pv";
                        battleAnim = EnumBattleAnim.AS;
                    }
                    break;
                case EnumBattle.CBT_VICTORY_MOVE:
                    if (!fight.uniteDef.survive(gv.getOtherPlayer().peuple.peuple.getType()))
                    {
                        message = "Défenseur meurt et l'attaquant prend sa place";
                        battleAnim = EnumBattleAnim.DMAPSP;
                    }
                    else
                    {
                        battle = EnumBattle.CBT_DRAW;
                        message = "Défenseur survit avec 1 pv";
                        battleAnim = EnumBattleAnim.DS;
                    }
                    break;
                case EnumBattle.CBT_VICTORY_NOMOVE:
                    if (!fight.uniteDef.survive(gv.getOtherPlayer().peuple.peuple.getType()))
                    {
                        message = "Défenseur meurt. L'attaquant ne prend pas sa place";
                        battleAnim = EnumBattleAnim.DMANPSP;
                    }
                    else
                    {
                        battle = EnumBattle.CBT_DRAW;
                        message = "Défenseur survit avec 1 pv";
                        battleAnim = EnumBattleAnim.DS;
                    }
                    break;
            }
            round.setTitle(message);
            round.setStateBatle(battleAnim);
            round.setStateUnitAtt(unitAtt);
            round.setStateUnitDef(unitDef);
            rounds.Add(round);
            return battle;
        }

        public void Do()
        {
            EnumBattle battle;
            bool res;
            do
            {
                this.fight.incCbt();

                battle = this.attaquer();

                res = battle == EnumBattle.CBT_VICTORY_MOVE || battle == EnumBattle.CBT_VICTORY_NOMOVE || battle == EnumBattle.CBT_LOSS;
            } while (this.fight.nbCbt < this.fight.nbCbtMax && !res);

            if (!res)
            {
                battle = EnumBattle.CBT_DRAW;
            }

            gv.battle = battle;
        }
    }
}
