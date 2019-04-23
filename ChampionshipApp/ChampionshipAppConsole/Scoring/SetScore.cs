using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionshipAppConsole.Scoring
{
    class SetScore : IScore
    {
        public Point[] PlayerScores { get; set; }

        private static SetScore setScoreInstance;
        private static readonly int NoOfPlayers = 2;

        public static SetScore GetInstance()
        {
            if (setScoreInstance == null)
            {
                setScoreInstance = InitializeSetScore();
            }

            return setScoreInstance;
        }

        public void GetWinner()
        {
            throw new NotImplementedException();
        }

        public void Increase(int winningPlayerNumber)
        {
            PlayerScores[winningPlayerNumber].Amount++;

            // not sure what to do when Set Is won
        }

        private static SetScore InitializeSetScore()
        {
            setScoreInstance = new SetScore();
            setScoreInstance.PlayerScores = new Point[NoOfPlayers];

            for (int i = 0; i < NoOfPlayers; i++)
            {
                setScoreInstance.PlayerScores[i] = new Point { Amount = 0 };
            }

            return setScoreInstance;
        }
    }
}
