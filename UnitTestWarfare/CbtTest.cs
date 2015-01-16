using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleWar;

namespace UnitTestWarfare
{
    /// <summary>
    /// Description résumée pour CbtTest
    /// </summary>
    [TestClass]
    public class CbtTest
    {
        public PartieImp game { get; set; }
        public CombatImp cbt { get; set; }
        public CbtTest()
        {
            DirecteurPartie dp = new DirecteurPartie();
            dp.definirMonteur(new MonteurNvllePartie());
            game = dp.creerPartie("j1", "j2", EnumCarte.DEMO, EnumPeuple.ELF, EnumPeuple.ORC);
            List<Unite> unitesDef = new List<Unite>();
            game.j2.peuple.unites.ForEach(u => unitesDef.Add(u));
            cbt = new CombatImp(game.j1.peuple.unites[0], unitesDef);
        }

        /*
         * Choose a defensive unit
         */
        [TestMethod]
        public void ChoixUnite()
        {
            UniteImp unite = null;
            game.j2.peuple.unites.ForEach(u =>
            {
                unite = (unite == null || unite.getDefEff() < u.getDefEff()) ? u : unite;
            });
            Assert.AreEqual<UniteImp>(unite, cbt.uniteDef);
        }

        /*
         * In this example, the success of the attack will be > 0.5
         */
        [TestMethod]
        public void ReussiteAttSupDef()
        {
            double r = cbt.calculerReussiteAtt();
            Assert.IsTrue(r > 0.5);
        }

    }
}
