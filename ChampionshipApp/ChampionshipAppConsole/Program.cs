using ChampionshipAppConsole.ParentChildScoring;
using ChampionshipAppConsole.ScoreTracking;
using System;
using System.Linq;

namespace ChampionshipAppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int option;
            Score score;
            score = new ScoreCreator().CreateScore();

            do
            {
                GenerateUI();
                string input = Console.ReadLine();
                if (!int.TryParse(input, out option))
                {
                    Console.WriteLine("Invalid option. Try again");
                    GenerateUI();
                }
                else
                {
                    switch (option)
                    {
                        case 0:
                            Console.WriteLine("Quitting");
                            break;
                        case 1:
                            var winningPlayer = score.PlayerPoints.FirstOrDefault(pp => pp.Amount > 0);
                            if (winningPlayer != null)
                            {
                                Console.WriteLine($"Match was already won by player {winningPlayer.PlayerID}");
                            }
                            else
                            {
                                score.ScorePoint(new Random().Next(0, 1));
                            }
                            break;
                        case 2:
                            score.Display();
                            break;
                        default:
                            GenerateUI();
                            break;
                    }
                }
            } while (option > 0);
        }

        private static void GenerateUI()
        {
            Console.WriteLine("1. Generate point");
            Console.WriteLine("2. See score");
            Console.WriteLine("0. Quit");
        }
    }
}
