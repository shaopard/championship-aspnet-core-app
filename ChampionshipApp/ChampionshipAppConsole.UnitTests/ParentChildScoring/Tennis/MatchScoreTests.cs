using ChampionshipAppConsole.ParentChildScoring.Tennis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChampionshipAppConsole.UnitTests.ParentChildScoring.Tennis
{
    [TestClass]
    class MatchScoreTests
    {
        private MatchScore _matchScore;
        //private Mock<SetScore> _setScore;

        [TestInitialize]
        public void TestInitialize()
        {
            _matchScore = new MatchScore();
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
