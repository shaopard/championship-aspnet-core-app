using ChampionshipAppConsole.ParentChildScoring;
using ChampionshipAppConsole.ParentChildScoring.Tennis;

namespace ChampionshipAppConsole.ScoreTracking
{
    class ScoreCreator
    {
        public Score CreateScoreTracker()
        {
            return new MatchScore();
        }
    }
}
