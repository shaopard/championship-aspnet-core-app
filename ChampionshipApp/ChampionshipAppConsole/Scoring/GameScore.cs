using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionshipAppConsole.Scoring
{
    class GameScore : IScore
    {
        public int FirstCompetitorScore { get; set; }
        public int SecondCompetitorScore { get; set; }

        private static GameScore gameScoreInstance;
        private GameScore() { }

        public static GameScore GetInstance()
        {
            if (gameScoreInstance == null)
            {
                gameScoreInstance = new GameScore();
            }

            return gameScoreInstance;
        }

        public void Add(IScore score)
        {
            throw new NotImplementedException();
        }

        public void GetWinner()
        {
            throw new NotImplementedException();
        }

        public void Increase()
        {
            throw new NotImplementedException();
        }
    }
}
