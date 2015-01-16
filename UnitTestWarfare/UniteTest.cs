using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleWar;
using Wrapper;

namespace UnitTestWarfare
{
    [TestClass]
    public class UniteTest
    {
        public UniteImp unite { get; set; }
        public UniteTest()
        {
            unite = new UniteImp(0, "unit");
        }
        [TestMethod]
        public void AttEffTest()
        {
            Assert.IsTrue(unite.getAttEff() == 2);
            unite.vie = 4;
            Assert.IsTrue(unite.getAttEff() == (2*4)/5.0F);
        }
        [TestMethod]
        public void DefEffTest()
        {
            Assert.IsTrue(unite.getDefEff() == 1);
            unite.vie = 1;
            Assert.IsTrue(unite.getDefEff() == (1 * 1) / 5.0F);
        }
        [TestMethod]
        public void MoveTest()
        {
            unite.move(1);
            Assert.IsTrue(unite.pos == 1);
        }
        [TestMethod]
        public void ResetTest(EnumPeuple peuple)
        {
            unite.reset(peuple);
            Assert.IsTrue(unite.pm == 1);
        }
        [TestMethod]
        public void DeplacerTestV()
        {
            DirecteurPartie dp = new DirecteurPartie();
            dp.definirMonteur(new MonteurNvllePartie());
            PartieImp game = dp.creerPartie("j1", "j2", EnumCarte.DEMO, EnumPeuple.ORC, EnumPeuple.NAIN);
            game.j1.peuple.unites[0].pos = 0;
            game.j2.peuple.unites.ForEach(u => u.pos = 3);
            game.j1.peuple.unites[0].seDeplacer(0, 1, EnumPeuple.ORC, game.carte, game.j2.peuple);
            Assert.IsTrue(game.j1.peuple.unites[0].pos == 1);
        }
        [TestMethod]
        public void DeplacerTestF()
        {
            DirecteurPartie dp = new DirecteurPartie();
            dp.definirMonteur(new MonteurNvllePartie());
            PartieImp game = dp.creerPartie("j1", "j2", EnumCarte.DEMO, EnumPeuple.ORC, EnumPeuple.NAIN);
            int posinit = game.j1.peuple.unites[0].pos;
            game.j1.peuple.unites[0].seDeplacer(0, 3, EnumPeuple.ORC, game.carte, game.j2.peuple);
            Assert.IsTrue(game.j1.peuple.unites[0].pos == posinit);
        }
    }
}
