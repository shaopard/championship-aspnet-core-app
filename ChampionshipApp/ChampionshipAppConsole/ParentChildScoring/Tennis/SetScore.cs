using ChampionshipAppConsole.Model;
using System;
using System.Linq;

namespace ChampionshipAppConsole.ParentChildScoring.Tennis
{
    class SetScore : Score
    {
        private readonly int MinimumGamesForSetWin = 6;

        public override bool IsComplete 
            => PlayerPoints.Any(pp => pp.Amount == MinimumGamesForSetWin)
                || (PlayerPoints.Count(pp => pp.Amount >= MinimumGamesForSetWin) == 1 && PlayerPoints.Count(pp => pp.Amount <= (MinimumGamesForSetWin - 2)) == 1);

        public SetScore(Score parentScoreNode)
            : base(parentScoreNode)
        {
            var newChildScore = GetNewChildScore();
            AddChildScore(newChildScore);
        }

        protected override Score CreateNewChildScore()
        {
            var newChild = new GameScore(this);
            newChild.InitializePlayerPoints();

            return newChild;
        }

        public override void Display()
        {
            var winningPlayer = GetWinner();

            if (winningPlayer != null)
            {
                Console.WriteLine($"Player 1 set score: { PlayerPoints[0].Amount } ");
                Console.WriteLine($"Player 2 set score: { PlayerPoints[1].Amount } ");
            }

            foreach(var gameScore in childScores)
            {
                gameScore.Display();
            }
        }
    }
}