using System;
using System.Linq;

namespace ChampionshipAppConsole.ParentChildScoring.Tennis
{
    // Using GameScore as an instance of Score<Point> as using the more specialized GamePoint will not allow to add a ParentScore as the parent(set) 
    // is an instance of Set<Score>. Also Score now features the "HasAdvantage" flag.
    internal class GameScore : Score<Point>
    {
        public GameScore(Score<Point> parentScore) 
            : base(parentScore) { }

        public override void CreateSibling()
        {
            var newSibling = new GameScore(ParentScore);
            newSibling.InitializePlayerPoints();

            ParentScore.AddChild(newSibling);
        }

        public override void Display()
        {
            Console.WriteLine($"Score for game { GetScorePositionInParent() }:");

            if (PlayerPoints[0].HasAdvantage)
            {
                Console.WriteLine($"Player 1 game score: A ");
            }
            else if(PlayerPoints[1].HasAdvantage)
            {
                Console.WriteLine($"Player 2 game score: A ");
            }
            else
            {
                Console.WriteLine($"Player 1 game score: { PlayerPoints[0].Amount } ");
                Console.WriteLine($"Player 2 game score: { PlayerPoints[1].Amount } ");
            }
        }

        public override Point GetWinner()
        {
            if (!IsComplete)
            {
                return null;
            }

            return PlayerPoints.Single(point => point.HasAdvantage || point.Amount == 40);
        }

        public override void InitializePlayerPoints()
        {
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                PlayerPoints[i] = new Point
                {
                    PlayerID = i,
                    Amount = 0,
                    HasAdvantage = false
                };
            }
        }

        public override void PointScored(int winningPlayerID)
        {
            if (IsDeuce())
            {
                PlayerPoints[winningPlayerID].HasAdvantage = true;
            }
            else if (IsDeuceSavePoint(winningPlayerID))
            {
                var losingPlayerIndex = GetLoosingPlayerIndex(winningPlayerID);
                PlayerPoints[losingPlayerIndex].HasAdvantage = false;
            }
            else if (IsWinPoint(winningPlayerID))
            {
                MarkComplete();
                UpdateParentScore(winningPlayerID);
                if (!ParentScore.IsComplete)
                {
                    CreateSibling();
                }
            }
            else
            {
                UpdateWinningPlayerScore(winningPlayerID);
            }
        }

        private bool IsDeuce() => PlayerPoints[0].Amount == 40 && PlayerPoints[1].Amount == 40 && !PlayerPoints[0].HasAdvantage && !PlayerPoints[1].HasAdvantage;

        private bool IsDeuceSavePoint(int winningPlayerID)
        {
            int losingPlayerIndex = GetLoosingPlayerIndex(winningPlayerID);

            return PlayerPoints[winningPlayerID].Amount == 40 && PlayerPoints[losingPlayerIndex].HasAdvantage;
        }

        private bool IsWinPoint(int winningPlayerNumber)
        {
            int losingPlayerIndex = GetLoosingPlayerIndex(winningPlayerNumber);

            return 
                (PlayerPoints[losingPlayerIndex].Amount == 40 && PlayerPoints[winningPlayerNumber].HasAdvantage) 
                ||(PlayerPoints[losingPlayerIndex].Amount < 40 && PlayerPoints[winningPlayerNumber].Amount == 40);
        }

        private void UpdateWinningPlayerScore(int winningPlayerID)
        {
            if (PlayerPoints[winningPlayerID].Amount == 0)
            {
                PlayerPoints[winningPlayerID].Amount = 15;
            }
            else if (PlayerPoints[winningPlayerID].Amount == 15)
            {
                PlayerPoints[winningPlayerID].Amount = 30;
            }
            else if (PlayerPoints[winningPlayerID].Amount == 30)
            {
                PlayerPoints[winningPlayerID].Amount = 40;
            }
        }
    }
}
