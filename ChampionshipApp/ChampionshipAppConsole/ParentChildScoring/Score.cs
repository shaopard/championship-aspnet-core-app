using ChampionshipAppConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChampionshipAppConsole.ParentChildScoring
{
    public abstract class Score : IDisplayable, IParentComponent, IScoreWatcher
    {
        public static readonly int NumberOfPlayers = 2;

        public Point[] PlayerPoints { get; set; }
        public Player[] Players { get; set; }
        public Score ParentScore { get; set; }
        public bool IsBottomScore => !childScores.Any();
        public bool HasParent => ParentScore != null;
        public abstract bool IsComplete { get; }

        protected virtual Point InitPoint() => new Point();
        protected List<Score> childScores = new List<Score>();

        private Score LastChildScore => childScores.FirstOrDefault(score => !score.IsComplete);
        private List<IScoreWatcher> _watchers = new List<IScoreWatcher>();

        protected Score(Score parentScore)//Factory cu input type of Point.cs
        {
            ParentScore = parentScore;
            PlayerPoints = new Point[NumberOfPlayers];
            Players = new Player[NumberOfPlayers];

            for (int i = 0; i < NumberOfPlayers; i++)
            {
                PlayerPoints[i] = InitPoint();
                Players[i] = new Player { PlayerID = i };
            }
        }

        public abstract void Display();

        public void AddChildScore(Score score)
        {
            childScores.Add(score);
        }

        public virtual Player GetWinner()
        {
            var winningPoint = (IsComplete)
               ? PlayerPoints.OrderByDescending(point => point.Amount).First()
               : null;

            return (winningPoint != null)
                ? Players.FirstOrDefault(player => player.PlayerID == winningPoint.PlayerID)
                : null;
        }

        public virtual void InitializePlayerPoints()
        {
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                PlayerPoints[i] = InitializePlayerPoint(i);
            }
        }

        public void Notify(Score score)
        {
            if (score.IsComplete)
            {
                var winningPlayer = score.GetWinner();
                Increase(winningPlayer.PlayerID);
                if (!IsComplete)
                {
                    var newScore = GetNewChildScore();
                    childScores.Add(newScore);
                }
                else if (IsComplete && HasParent && !ParentScore.HasParent) 
                {
                    Console.WriteLine("MatchComplete");
                    ParentScore.Display();
                }
            }
        }

        public virtual void ScorePoint(int winningPlayer)
        {
            ScoreUpdate(winningPlayer);

            NotifyWatchers();
        }

        protected virtual void ScoreUpdate(int winningPlayer)
        {
            LastChildScore.ScorePoint(winningPlayer);
        }

        protected virtual void Increase(int winningPlayer)
        {
            PlayerPoints[winningPlayer].Amount++;
        }

        protected virtual int GetScorePositionInParent() 
            => (ParentScore != null) ? (ParentScore.childScores.IndexOf(this)) + 1 : 0;

        protected abstract Score CreateNewChildScore();

        protected void NotifyWatchers()
        {
            foreach (var watcher in _watchers)
            {
                watcher.Notify(this);
            }
        }

        protected Score GetNewChildScore()
        {
            Score child = CreateNewChildScore();
            child.AttachScoreWatcher(this);

            return child;
        }

        private void AttachScoreWatcher(IScoreWatcher score)
        {
            _watchers.Add(score);
        }

        private Point InitializePlayerPoint(int playerID)
            => new Point { PlayerID = playerID, Amount = 0 };
    }
}
