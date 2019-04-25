using ChampionshipAppConsole.Model;
using System;
using System.Linq;

namespace ChampionshipAppConsole.ParentChildScoring.Tennis
{
    internal class GameScore : Score
    {
        private readonly int MininumPointsToWinGame = 40;

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
                    .FirstOrDefault(point => point.HasAdvantage || point.Amount == MininumPointsToWinGame);

                winningPlayer = Players.FirstOrDefault(player => player.PlayerID == winningPoint.PlayerID);
            }

            return winningPlayer;
        }

        public override void InitializePlayerPoints()
        {
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                PlayerPoints[i] = InitializePlayerPoint(i);
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
            return (PlayerPoints.Cast<GamePoint>().ToList()).All(pp => pp.Amount <= MininumPointsToWinGame && !pp.HasAdvantage)
                && PlayerPoints.Any(pp => pp.Amount < MininumPointsToWinGame);
        }

        private bool IsDeuce() => 
            PlayerPoints[0].Amount == MininumPointsToWinGame
            && PlayerPoints[1].Amount == MininumPointsToWinGame
            && !(PlayerPoints[0] as GamePoint).HasAdvantage 
            && !(PlayerPoints[1] as GamePoint).HasAdvantage;

        private bool IsDeuceSavePoint(int winningPlayerID)
        {
            return PlayerPoints[winningPlayerID].Amount == MininumPointsToWinGame
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
        }

        private bool IsWinPoint(int winningPlayerNumber)
        {
            int losingPlayerIndex = GetLoosingPlayerIndex(winningPlayerNumber);

            return
                (PlayerPoints[losingPlayerIndex].Amount == MininumPointsToWinGame && (PlayerPoints[winningPlayerNumber]as GamePoint).HasAdvantage)
                || (PlayerPoints[losingPlayerIndex].Amount < MininumPointsToWinGame && PlayerPoints[winningPlayerNumber].Amount == MininumPointsToWinGame);
        }

        private int GetLoosingPlayerIndex(int winningPlayerNumber) => (winningPlayerNumber == 0) ? 1 : 0;

        private GamePoint InitializePlayerPoint(int playerID)
            => new GamePoint { PlayerID = playerID, Amount = 0, HasAdvantage = false };
    }
}
