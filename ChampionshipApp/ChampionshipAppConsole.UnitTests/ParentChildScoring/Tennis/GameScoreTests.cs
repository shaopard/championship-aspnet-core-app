using ChampionshipAppConsole.ParentChildScoring.Tennis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChampionshipAppConsole.UnitTests.ParentChildScoring.Tennis
{
    [TestClass]
    class GameScoreTests
    {
        private GameScore _gameScore;
        private Mock<SetScore> _setScore;

        [TestInitialize]
        public void TestInitialize()
        {
            _setScore = new Mock<SetScore>();
            _gameScore = new GameScore(_setScore.Object);
        }

        [TestMethod]
        public void Display__() { }

        [TestMethod]
        public void AddChildScore__() { }

        [TestMethod]
        public void GetWinner__() { }

        [TestMethod]
        public void InitializePlayerPoints__() { }

        [TestMethod]
        public void Notify__() { }

        [TestMethod]
        public void ScorePoint__() { }

    }
}
