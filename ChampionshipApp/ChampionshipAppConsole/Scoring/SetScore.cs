using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionshipAppConsole.Scoring
{
    class SetScore : IScore
    {
        public int FirstCompetitorScore { get; set; }
        public int SecondCompetitorScore { get; set; }

        private static SetScore setScoreInstance;
        private SetScore() { }

        public static SetScore GetInstance()
        {
            if (setScoreInstance == null)
            {
                setScoreInstance = new SetScore();
            }

            return setScoreInstance;
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
