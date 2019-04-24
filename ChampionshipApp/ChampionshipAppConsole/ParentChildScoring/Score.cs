using System;
using System.Collections.Generic;
using System.Linq;
using ChampionshipAppConsole.ParentChildScoring.Tennis;

namespace ChampionshipAppConsole.ParentChildScoring
{
    abstract class Score<T> : ICompletableScore, IDisplayable, IParentComponent<T> where T : Point
    {
        public T[] PlayerPoints { get; set; }
        public Score<T> ParentScore { get; set; }
        public bool IsComplete { get; set; }

        protected List<Score<T>> children = new List<Score<T>>();
        public static readonly int NumberOfPlayers = 2;

        protected Score(Score<T> parentComponent)
        {
            ParentScore = parentComponent;
            PlayerPoints = new T[NumberOfPlayers];
        }

        public void MarkComplete()
        {
            IsComplete = true;
        }

        public void AddChild(Score<T> componentScore)
        {
            componentScore.ParentScore = this;
            children.Add(componentScore);
        }

        protected void RemoveChild(Score<T> score)
        {
            children.Remove(score);
        }

        protected void UpdateParentScore(int winningPlayerID)
        {
            if (ParentScore != null)
            {
                ParentScore.ScorePoint(winningPlayerID);
            }
        }

        protected int GetScorePositionInParent()
        {
            return (ParentScore != null) ? ParentScore.children.IndexOf(this) + 1 : 0;
        }

        protected bool IsRootScore() => ParentScore == null;

        protected bool IsLeafScore() => !children.Any();

        protected int GetLoosingPlayerIndex(int winningPlayerID) => (winningPlayerID == 0) ? 1 : 0;

        public abstract void Display();

        public abstract void ScorePoint(int winningPlayerID);

        public abstract void InitializePlayerPoints();

        public abstract void CreateSibling();

        public abstract T GetWinner();
    }
}
