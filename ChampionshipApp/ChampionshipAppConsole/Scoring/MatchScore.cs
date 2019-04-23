using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionshipAppConsole.Scoring
{
    class MatchScore : IScore
    {
        public Point[] PlayerScores { get; set; }

        private static MatchScore matchScoreInstance;
        private static readonly int NoOfPlayers = 2;

        public static MatchScore GetInstance()
        {
            if (matchScoreInstance == null)
            {
                matchScoreInstance = new MatchScore();
            }

            return matchScoreInstance;
        }

        public void GetWinner()
        {
            throw new NotImplementedException();
        }

        public void Increase(int winningPlayerNumber)
        {
            PlayerScores[winningPlayerNumber].Amount++;

            // not sure what to do when Match Is won
        }

        private static MatchScore InitializeGameScore()
        {
            matchScoreInstance = new MatchScore();
            matchScoreInstance.PlayerScores = new Point[NoOfPlayers];

            for (int i = 0; i < NoOfPlayers; i++)
            {
                matchScoreInstance.PlayerScores[i] = new Point { Amount = 0 };
            }

            return matchScoreInstance;
        }
    }
}
