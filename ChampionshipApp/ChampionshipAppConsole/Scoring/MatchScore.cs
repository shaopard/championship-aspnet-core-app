using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionshipAppConsole.Scoring
{
    class MatchScore : IScore
    {
        public int FirstCompetitorScore { get; set; }
        public int SecondCompetitorScore { get; set; }

        private static MatchScore matchScoreInstance;
        private MatchScore() { }

        public static MatchScore GetInstance()
        {
            if (matchScoreInstance == null)
            {
                matchScoreInstance = new MatchScore();
            }

            return matchScoreInstance;
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
