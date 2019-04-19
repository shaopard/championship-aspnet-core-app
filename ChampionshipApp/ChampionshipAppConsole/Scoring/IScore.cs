using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionshipAppConsole.Scoring
{
    public interface IScore
    {
        int FirstCompetitorScore { get; set; }
        int SecondCompetitorScore { get; set; }

        void Add(IScore score);

        void Increase();

        void GetWinner();
    }
}
