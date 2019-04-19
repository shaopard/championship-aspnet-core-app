using ChampionshipAppConsole.Sport;
using System;

namespace ChampionshipAppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int option;

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
                    var scoreTracker = new ScoreTracker<Tenis>();
                    switch (option)
                    {
                        case 0:
                            Console.WriteLine("Quitting");
                            break;
                        case 1:
                            scoreTracker.PointScored();
                            break;
                        case 2:
                            Console.WriteLine("Display score. TODO");
                            break;
                        default:
                            GenerateUI();
                            break;
                    }
                }
            } while (option > 0);

            Console.ReadLine();
        }

        private static void GenerateUI()
        {
            Console.WriteLine("1. Generate point");
            Console.WriteLine("2. See score");
            Console.WriteLine("0. Quit");
        }
    }
}
