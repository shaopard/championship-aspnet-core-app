namespace ChampionshipAppConsole.Scoring
{
    interface IScore
    {
        void Increase(int winningPlayerNumber);

        void GetWinner();
    }
}
