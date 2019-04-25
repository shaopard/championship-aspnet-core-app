using ChampionshipAppConsole.Model;
using System;
using System.Linq;

namespace ChampionshipAppConsole.ParentChildScoring.Tennis
{
    class SetScore : Score
    {
        public override bool IsComplete 
            => PlayerPoints.Any(pp => pp.Amount == 2)
                || (PlayerPoints.Count(pp => pp.Amount >= 6) == 1 && PlayerPoints.Count(pp => pp.Amount <= 4) == 1);

        public SetScore(Score parentScoreNode)
            : base(parentScoreNode)
        {
            var newChildScore = AddNewChildScore();
            childScores.Add(newChildScore);
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
                Console.WriteLine($"Player 1 game score: { PlayerPoints[0].Amount } ");
                Console.WriteLine($"Player 2 game score: { PlayerPoints[1].Amount } ");
            }
            else
            {
                Console.WriteLine($"Set { GetScorePositionInParent() } is ongoing.");
            }

            foreach(var gameScore in childScores)
            {
                gameScore.Display();
            }
        }
    }
}