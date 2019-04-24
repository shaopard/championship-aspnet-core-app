using System;
using System.Linq;

namespace ChampionshipAppConsole.ParentChildScoring.Tennis
{
    class MatchScore : Score<Point>
    {
        public MatchScore(Score<Point> parentComponent) 
            : base(parentComponent)
        {
            var newChild = new SetScore(this);
            newChild.InitializePlayerPoints();
            AddChild(newChild);
        }

        public override void CreateSibling()
        {
            throw new Exception("Cannot create sibling for Match score");
        }

        public override void Display()
        {
            var winningPlayer = GetWinner().PlayerID + 1;

            Console.WriteLine($"The match was won by player: { winningPlayer }");

            foreach(var setScore in children)
            {
                setScore.Display();
            }
        }

        public override Point GetWinner()
        {
            return (IsComplete)
                ? PlayerPoints.OrderByDescending(point => point.Amount).First()
                : null;
        }

        public override void InitializePlayerPoints()
        {
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                PlayerPoints[i] = new Point
                {
                    PlayerID = i,
                    Amount = 0
                };
            }
        }

        public override void ScorePoint(int winningPlayerID)
        {
            PlayerPoints[winningPlayerID].Amount++;
            MarkComplete();
        }
    }
}