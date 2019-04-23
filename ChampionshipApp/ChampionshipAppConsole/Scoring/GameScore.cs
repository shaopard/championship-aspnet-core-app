using System;
using System.Collections.Generic;

namespace ChampionshipAppConsole.Scoring
{
    class GameScore : IScore
    {
        public GamePoint[] PlayerScores { get; set; }

        private static readonly int NoOfPlayers = 2;
        private static GameScore gameScoreInstance;

        private GameScore() { }

        public static GameScore GetInstance()
        {
            if (gameScoreInstance == null)
            {
                gameScoreInstance = InitializeGameScore();
            }

            return gameScoreInstance;
        }

        public void GetWinner()
        {
            throw new NotImplementedException();
        }

        public void Increase(int winningPlayerNumber)
        {
            if (IsDeuce())
            {
                PlayerScores[winningPlayerNumber].HasAdvantage = true;
            }
            else if (IsDeuceSavePoint(winningPlayerNumber))
            {
                var losingPlayerIndex = GetLoosingPlayerIndex(winningPlayerNumber);
                PlayerScores[losingPlayerIndex].HasAdvantage = false;
            }
            else if (IsDeuceWinPoint(winningPlayerNumber))
            {
                //Game won
                // nu stiu sigut ce trebuie sa fac aici
            }
            else
            {
                PlayerScores[winningPlayerNumber].Amount = GetWinningPlayerUpdatedScore(winningPlayerNumber);
            }
        }

        private void GetWinningPlayerUpdatedScore(int winningPlayerNumber)
        {
            
        }

        private static GameScore InitializeGameScore()
        {
            gameScoreInstance = new GameScore();
            gameScoreInstance.PlayerScores = new GamePoint[NoOfPlayers];

            for(int i = 0; i < NoOfPlayers; i++)
            {
                gameScoreInstance.PlayerScores[i] = new GamePoint
                {
                    Amount = 0,
                    HasAdvantage = false
                };
            }

            return gameScoreInstance;
        }

        private bool IsDeuce() => PlayerScores[0].Amount == 40 && PlayerScores[1].Amount == 40;

        private bool IsDeuceSavePoint(int winningPlayerNumber)
        {
            int losingPlayerIndex = GetLoosingPlayerIndex(winningPlayerNumber);

            return PlayerScores[winningPlayerNumber].Amount == 40 && PlayerScores[losingPlayerIndex].HasAdvantage;
        }

        private bool IsDeuceWinPoint(int winningPlayerNumber)
        {
            int losingPlayerIndex = GetLoosingPlayerIndex(winningPlayerNumber);

            return PlayerScores[losingPlayerIndex].Amount == 40 && PlayerScores[winningPlayerNumber].HasAdvantage;
        } 

        private int GetLoosingPlayerIndex(int winningPlayerNumber) => (winningPlayerNumber == 0) ? 1 : 0;
    }
}
