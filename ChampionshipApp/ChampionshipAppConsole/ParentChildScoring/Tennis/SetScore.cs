using System;
using System.Linq;

namespace ChampionshipAppConsole.ParentChildScoring.Tennis
{
    class SetScore : Score<Point>
    {
        public SetScore(Score<Point> parentScore)
            : base(parentScore)
        {
            var newChild = new GameScore(this);
            newChild.InitializePlayerPoints();
            AddChild(newChild);
        }

        public override void CreateSibling()
        {
            var newSibling = new SetScore(ParentScore);
            newSibling.InitializePlayerPoints();

            ParentScore.AddChild(newSibling);
        }

        public override void Display()
        {
            Console.WriteLine($"Score for set { GetScorePositionInParent() }:");

            Console.WriteLine($"Player 1 game score: { PlayerPoints[0].Amount } ");
            Console.WriteLine($"Player 2 game score: { PlayerPoints[1].Amount } ");
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

        public override void PointScored(int winningPlayerID)
        {
            PlayerPoints[winningPlayerID].Amount++;

            if (IsWinPoint(winningPlayerID))
            {
                MarkComplete();
                UpdateParentScore(winningPlayerID);
                if (!ParentScore.IsComplete)
                {
                    CreateSibling();
                }
            }
        }

        private bool IsWinPoint(int winningPlayerID)
        {
            var loosingPlayerIndex = GetLoosingPlayerIndex(winningPlayerID);

            return (PlayerPoints[winningPlayerID].Amount == 6 && PlayerPoints[loosingPlayerIndex].Amount < 5) 
                || PlayerPoints[winningPlayerID].Amount == 7;
        }
    }
}
