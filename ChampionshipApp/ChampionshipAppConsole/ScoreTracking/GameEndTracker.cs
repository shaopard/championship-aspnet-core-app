using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionshipAppConsole.ScoreTracking
{
    class GameEndTracker : IEndTracker
    {
        private List<IEndTracker> _gameEndObservers;

        public GameEndTracker()
        {
            _gameEndObservers = new List<IEndTracker>();
        }

        public void Attach(IEndTracker setEndTracker)
        {
            _gameEndObservers.Add(setEndTracker);
        }

        public void Detach(IEndTracker setEndTracker)
        {
            _gameEndObservers.Remove(setEndTracker);
        }

        // Notify observers of Game ending
        public void NotifyEnd()
        {
            foreach(var gameEndObserver in _gameEndObservers)
            {
                gameEndObserver.NotifyEnd();
            }
        }

        // Game Score update logic
        public void NotifyScoreChange(int winningPlayerIndex)
        {
            //GameScore.GetInstance().Increase(winningPlayerIndex);

            if(false)
            {
                NotifyEnd();
            }
        }
    }
}
