using System;
using System.Linq;

namespace ChampionshipAppConsole.ParentChildScoring.Tennis
{
    public class MatchScore : Score
    {
        private readonly int SetsForMatchWin = 2;

        public override bool IsComplete => PlayerPoints.Any(pp => pp.Amount == SetsForMatchWin);

        public MatchScore() 
            : base(null)
        {
            var newChildScore = GetNewChildScore();
            AddChildScore(newChildScore);
        }

        protected override Score CreateNewChildScore()
        {
            var newChild = new SetScore(this);
            newChild.ParentScore = this;
            newChild.InitializePlayerPoints();

            return newChild;
        }

        public override void Display()
        {
            var winningPlayer = GetWinner();

            if (winningPlayer != null)
            {
                var winningPlayerID = winningPlayer.PlayerID++;
                Console.WriteLine($"The match was won by player: { winningPlayerID }");
            }
           
            foreach (var setScore in childScores)
            {
                setScore.Display();
            }
        }
    }
}