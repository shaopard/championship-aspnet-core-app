namespace ChampionshipAppConsole.ScoreTracking
{
    class ScoreTrackerCreator
    {
        public ScoreTracker CreateScoreTracker()
        {
            var scoreTracker = new ScoreTracker();

            var gameEndTracker = new GameEndTracker();
            var setEndTracker = new SetEndTracker();
            var matchEndTracker = new MatchEndTracker();

            setEndTracker.Attach(matchEndTracker);
            gameEndTracker.Attach(setEndTracker);
            scoreTracker.Attach(gameEndTracker);
            return scoreTracker;
        }
    }
}
