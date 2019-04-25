using ChampionshipAppConsole.ParentChildScoring;
using ChampionshipAppConsole.ScoreTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChampionshipAppConsole.UnitTests.ScoreTracking
{
    [TestClass]
    public class ScoreCreatorTests
    {
        [TestMethod]
        public void CreateScore_Called_ReturnScore()
        {
            var scoreCreator = new ScoreCreator();

            var result = scoreCreator.CreateScore();

            Assert.IsInstanceOfType(result, typeof(Score));
        }
    }
}
