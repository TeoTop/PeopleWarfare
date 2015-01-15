using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleWar;
namespace UnitTestWarfare
{
    [TestClass]
    public class PartieTest
    {
        private PartieImp _game;
        public PartieImp game
        {
            get
            {
                if (_game == null)
                {
                    DirecteurPartie dp = new DirecteurPartie();
                    dp.definirMonteur(new MonteurNvllePartie());
                    _game = dp.creerPartie("j1", "j2", EnumCarte.DEMO, EnumPeuple.ELF, EnumPeuple.ORC);

                }
                return _game;
            }
        }
        [TestMethod]
        public void ValidTourTest_Valid()
        {
            Assert.Equals(2, game.nbTourMax);
        }
        [TestMethod]
        public void ValidTourTest_NValidSup()
        {
            Assert.Equals(6, game.nbTourMax);
        }
        [TestMethod]
        public void ValidTourTest_NValidInf()
        {
            Assert.Equals(-1, game.nbTourMax);
        }
        [TestMethod]
        public void SwitcherJoueurTest()
        {
            int j = game.joueurCourant;
            game.switcherJoueur();
            Assert.Equals((j + 1) % 2, game.joueurCourant);
        }
        [TestMethod]
        public void FinPartieTest_J1Win()
        {
            game.j1.nbPoints = 4;
            game.j2.nbPoints = 3;
            Assert.Equals(game.verifierFinPartie(), game.j1);
        }
        [TestMethod]
        public void FinPartieTest_J2Win()
        {
            game.j1.nbPoints = 3;
            game.j2.nbPoints = 4;
            Assert.Equals(game.verifierFinPartie(), game.j2);
        }
    }
}
