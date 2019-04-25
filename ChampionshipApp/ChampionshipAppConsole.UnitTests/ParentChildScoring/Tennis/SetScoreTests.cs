using ChampionshipAppConsole.ParentChildScoring.Tennis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChampionshipAppConsole.UnitTests.ParentChildScoring.Tennis
{
    [TestClass]
    class SetScoreTests
    {
        private SetScore _setScore;
        private Mock<MatchScore> _matchScore;

        [TestInitialize]
        public void TestInitialize()
        {
            _matchScore = new Mock<MatchScore>();
            _setScore = new SetScore(_matchScore.Object);
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
