using ChampionshipAppConsole.ScoreTracker;
using ChampionshipAppConsole.Scoring;
using ChampionshipAppConsole.Sport;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionshipAppConsole
{
    public class ScoreTracker<T> where T : ISport, new()
    {
        T sport;
        List<IScoreWatcher> scorewatchers;

        public ScoreTracker()
        {
            scorewatchers = new List<IScoreWatcher>();
            sport = new T();

            switch (sport.GetType().Name)
            {
                case nameof(Tenis):
                    InitializeTenisScoreWatchers();
                    InitializeTenisMatchScore();
                    break;
                default:
                    throw new ArgumentException("Scoring not supported for the given sport");
            }
        }

        public void PointScored()
        {
            foreach (var scorewatcher in scorewatchers)
            {
                scorewatcher.Notify();
            }
        }

        private void InitializeTenisScoreWatchers()
        {
            scorewatchers.Add(new GameEndTracker());
            scorewatchers.Add(new SetEndTracker());
            scorewatchers.Add(new MatchEndTracker());
        }

        private void InitializeTenisMatchScore()
        {
            GameScore.GetInstance().FirstCompetitorScore = 0;
            GameScore.GetInstance().SecondCompetitorScore = 0;
            SetScore.GetInstance().FirstCompetitorScore = 0;
            SetScore.GetInstance().SecondCompetitorScore = 0;
            MatchScore.GetInstance().FirstCompetitorScore = 0;
            MatchScore.GetInstance().SecondCompetitorScore = 0;
        }
    }
}
