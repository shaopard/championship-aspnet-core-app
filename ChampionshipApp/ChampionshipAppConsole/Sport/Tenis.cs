using ChampionshipAppConsole.ParentChildScoring;
using ChampionshipAppConsole.ScoreTracking;

namespace ChampionshipAppConsole.Sport
{
    public class Tenis : ISport
    {
        private IEndTracker _gameEndTracker;
        private IEndTracker _setEndTracker;
        private IEndTracker _matchEndTracker;

        private Score<Point> _scoreTracker;

        public Tenis()
        {
            GenerateScoringTrackers();
        }

        public void GenerateScoringTrackers()
        {
            _gameEndTracker = new GameEndTracker();
            _setEndTracker = new SetEndTracker();
            _matchEndTracker = new MatchEndTracker();
        }

        public void PointScored()
        {
            _gameEndTracker.NotifyEnd();
        }
    }
}
