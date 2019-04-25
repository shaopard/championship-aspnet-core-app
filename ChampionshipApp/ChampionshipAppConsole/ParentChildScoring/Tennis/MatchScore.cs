using ChampionshipAppConsole.Model;
using System;
using System.Linq;

namespace ChampionshipAppConsole.ParentChildScoring.Tennis
{
    public class MatchScore : Score
    {
        public override bool IsComplete => PlayerPoints.Any(pp => pp.Amount > 2);

        public MatchScore() 
            : base(null)
        {
            var newChildScore = AddNewChildScore();
            childScores.Add(newChildScore);
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
                var winningPlayerID = winningPlayer.PlayerID;
                Console.WriteLine($"The match was won by player: { winningPlayerID }");
            }
            else
            {
                Console.WriteLine($"The match is ongoing.");
            }

            foreach (var setScore in childScores)
            {
                setScore.Display();
            }
        }
    }
}