using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleWar;
namespace UnitTestWarfare
{
    [TestClass]
    public class PartieTest
    {
        public PartieImp game
        {
            get;
            set;
        }
        public PartieTest()
        {
            DirecteurPartie dp = new DirecteurPartie();
            dp.definirMonteur(new MonteurNvllePartie());
            game = dp.creerPartie("j1", "j2", EnumCarte.DEMO, EnumPeuple.ELF, EnumPeuple.NAIN);
        }
        [TestMethod]
        public void ValidTourTest_Valid()
        {
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            Assert.IsTrue(game.isValidTour(2));
        }
        [TestMethod]
        public void ValidTourTest_NValidSup()
        {
            Assert.IsFalse(game.isValidTour(5));
        }
        [TestMethod]
        public void ValidTourTest_NValidInf()
        {
            Assert.IsFalse(game.isValidTour(-1));
        }
        [TestMethod]
        public void SwitcherJoueurTest()
        {
            int j = game.joueurCourant;
            game.switcherJoueur();
            Assert.AreEqual((j + 1) % 2, game.joueurCourant);
        }
        [TestMethod]
        public void FinPartieTest_J1Win()
        {
            game.j2.peuple.unites.Remove(game.j2.peuple.unites[0]);
            game.j2.peuple.unites.Remove(game.j2.peuple.unites[0]);
            game.j2.peuple.unites.Remove(game.j2.peuple.unites[0]);
            game.j2.peuple.unites.Remove(game.j2.peuple.unites[0]);
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            Assert.AreEqual<JoueurImp>(game.j1, (JoueurImp)game.verifierFinPartie());
        }
        [TestMethod]
        public void FinPartieTest_J2Win()
        {
            game.j1.peuple.unites.Remove(game.j1.peuple.unites[0]);
            game.j1.peuple.unites.Remove(game.j1.peuple.unites[0]);
            game.j1.peuple.unites.Remove(game.j1.peuple.unites[0]);
            game.j1.peuple.unites.Remove(game.j1.peuple.unites[0]);
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            game.tours.Add(new TourImp());
            Assert.AreEqual<JoueurImp>(game.j2, (JoueurImp)game.verifierFinPartie());
        }
    }
}
