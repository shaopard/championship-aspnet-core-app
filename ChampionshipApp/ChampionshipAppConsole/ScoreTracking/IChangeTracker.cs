namespace ChampionshipAppConsole.ScoreTracking
{
    public interface IChangeTracker
    {
        void NotifyScoreChange(int winningPlayerIndex);
    }
}
