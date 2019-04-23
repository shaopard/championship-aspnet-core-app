using System.Collections.Generic;

namespace ChampionshipAppConsole.ScoreTracking
{
    public class ScoreTracker : IChangeTracker
    {
        private List<IEndTracker> _endObservers;

        public ScoreTracker()
        {
            _endObservers = new List<IEndTracker>();
        }

        public void PointScored(int winningPlayerIndex)
        {
            NotifyScoreChange(winningPlayerIndex);
        }

        public void Attach(IEndTracker gameEndTracker)
        {
            _endObservers.Add(gameEndTracker);
        }

        public void Detach(IEndTracker gameEndTracker)
        {
            _endObservers.Remove(gameEndTracker);
        }

        public void NotifyScoreChange(int winningPlayerIndex)
        {
            foreach (var gameEndObserver in _endObservers)
            {
                gameEndObserver.NotifyScoreChange(winningPlayerIndex);
            }
        }
    }
}
