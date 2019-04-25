using ChampionshipAppConsole.Model;
using System;
using System.Linq;

namespace ChampionshipAppConsole.ParentChildScoring.Tennis
{
    internal class GameScore : Score
    {
        public override bool IsComplete
        {
            get
            {
                return PlayerPoints.Cast<GamePoint>().ToList().Any(pp => pp.IsFinished);
            }
        }

        public GameScore(Score parentScoreNode) 
            : base(parentScoreNode) { }

        protected override Point InitPoint() => new GamePoint();

        protected override Score CreateNewChildScore()
        {
            throw new Exception($"Game scores do not have any children!");
        }

        public override void Display()
        {
            Console.WriteLine($"Score for game { GetScorePositionInParent() }:");

            if ((PlayerPoints[0] as GamePoint).HasAdvantage )
            {
                Console.WriteLine($"Player 1 game score: A ");
            }
            else if((PlayerPoints[1] as GamePoint).HasAdvantage)
            {
                Console.WriteLine($"Player 2 game score: A ");
            }
            else
            {
                Console.WriteLine($"Player 1 game score: { PlayerPoints[0].Amount } ");
                Console.WriteLine($"Player 2 game score: { PlayerPoints[1].Amount } ");
            }
        }

        public override Player GetWinner()
        {
            Player winningPlayer;

            if (!IsComplete)
            {
                winningPlayer = null;
            }
            else
            {
                var winningPoint = PlayerPoints.Cast<GamePoint>().ToList()
                    .OrderByDescending(playerPoint => playerPoint.Amount)
                    .FirstOrDefault(point => point.HasAdvantage || point.Amount == 40);

                winningPlayer = Players.FirstOrDefault(player => player.PlayerID == winningPoint.PlayerID);
            }

            return winningPlayer;
        }

        public override void InitializePlayerPoints()
        {
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                PlayerPoints[i] = new GamePoint
                {
                    PlayerID = i,
                    Amount = 0,
                    HasAdvantage = false
                };
            }
        }
        public override void ScorePoint(int winningPlayerID)
        {
            Increase(winningPlayerID);

            NotifyWatchers();
        }

        protected override void Increase(int winningPlayerID)
        {
            if (IsDeuce())
            {
                (PlayerPoints[winningPlayerID] as GamePoint).HasAdvantage = true;
            }
            else if (IsDeuceSavePoint(winningPlayerID))
            {
                (PlayerPoints.Cast<GamePoint>().ToList())
                    .FirstOrDefault(pp => pp.HasAdvantage)
                    .HasAdvantage = false;
            }
            else if (IsWinPoint(winningPlayerID))
            {
                (PlayerPoints[winningPlayerID] as GamePoint).IsFinished = true;
            }
            else
            {
                UpdateWinningPlayerScore(winningPlayerID);
            }
        }

        private bool IsSimpleScoringUpdate()
        {
            return (PlayerPoints.Cast<GamePoint>().ToList()).All(pp => pp.Amount <= 40 && !pp.HasAdvantage)
                && PlayerPoints.Any(pp => pp.Amount < 40);
        }

        private bool IsDeuce() => 
            PlayerPoints[0].Amount == 40 
            && PlayerPoints[1].Amount == 40 
            && !(PlayerPoints[0] as GamePoint).HasAdvantage 
            && !(PlayerPoints[1] as GamePoint).HasAdvantage;

        private bool IsDeuceSavePoint(int winningPlayerID)
        {
            return PlayerPoints[winningPlayerID].Amount == 40 
                && (PlayerPoints.Cast<GamePoint>().ToList()).Any(pp => pp.HasAdvantage);
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
            else if (PlayerPoints[winningPlayerID].Amount == 40)
            {
                PlayerPoints[winningPlayerID].Amount = 50;
            }
        }


        private bool IsWinPoint(int winningPlayerNumber)
        {
            int losingPlayerIndex = GetLoosingPlayerIndex(winningPlayerNumber);

            return
                (PlayerPoints[losingPlayerIndex].Amount == 40 && (PlayerPoints[winningPlayerNumber]as GamePoint).HasAdvantage)
                || (PlayerPoints[losingPlayerIndex].Amount < 40 && PlayerPoints[winningPlayerNumber].Amount == 40);
        }

        private int GetLoosingPlayerIndex(int winningPlayerNumber) => (winningPlayerNumber == 0) ? 1 : 0;
    }
}
