using ChampionshipAppConsole.ParentChildScoring;
using ChampionshipAppConsole.ParentChildScoring.Tennis;
using System.Runtime.CompilerServices;


namespace ChampionshipAppConsole.ScoreTracking
{
    internal class ScoreCreator
    {
        public Score CreateScore()
        {
            return new MatchScore();
        }
    }
}
