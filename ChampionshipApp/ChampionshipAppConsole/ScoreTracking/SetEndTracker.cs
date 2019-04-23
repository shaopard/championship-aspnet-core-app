using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionshipAppConsole.ScoreTracking
{
    class SetEndTracker : IEndTracker
    {
        private List<IEndTracker> _setEndObservers;

        public SetEndTracker()
        {
            _setEndObservers = new List<IEndTracker>();
        }

        public void Attach(IEndTracker setEndObserver)
        {
            _setEndObservers.Add(setEndObserver);
        }

        public void Detach(IEndTracker setEndObserver)
        {
            _setEndObservers.Remove(setEndObserver);
        }

        // Notify observers of Game ending
        public void NotifyEnd()
        {
            foreach (var setEndObserver in _setEndObservers)
            {
                setEndObserver.NotifyEnd();
            }
        }

        // Set Score update logic
        public void NotifyScoreChange(int winningPlayerIndex)
        {
            //SetScore.GetInstance().Increase(winningPlayerIndex);

            if (false)
            {
                NotifyEnd();
            }
        }
    }
}
