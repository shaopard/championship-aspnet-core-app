using System;
using System.Collections.Generic;

namespace ChampionshipAppConsole.ScoreTracking
{
    class MatchEndTracker : IEndTracker
    {
        private List<IEndTracker> _matchEndObservers;

        public MatchEndTracker()
        {
            _matchEndObservers = new List<IEndTracker>();
        }

        public void Attach(IEndTracker matchEndTracker)
        {
            _matchEndObservers.Add(matchEndTracker);
        }

        public void Detach(IEndTracker matchEndTracker)
        {
            _matchEndObservers.Remove(matchEndTracker);
        }

        // Notify observers of Match ending
        public void NotifyEnd()
        {
            foreach (var matchEndOberver in _matchEndObservers)
            {
                matchEndOberver.NotifyEnd();
            }
        }

        // Match Score update logic
        public void NotifyScoreChange(int winningPlayerIndex)
        {
            //MatchScore.GetInstance().PlayerScores[winningPlayerIndex].Amount++;

            if (false)
            {
                NotifyEnd();
            }
        }
    }
}
