using System;
using AFL_Simulation.Models;
using AFL_Simulation.Engine;
using AFL_simulation.Utils;

namespace AFL_Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- AFL Game Day Simulator (v0.4) ---");

            // Setup
            Team kc = new Team("Kansas City", "Plainsmen", "Coach Marty");
            kc.FillRosterWithScabs();
            Team ny = new Team("New York", "Empire", "Coach Parcells");
            ny.FillRosterWithScabs();

            // Create the Game State
            Game currentGame = new Game(kc, ny);

            Console.WriteLine($"KICKOFF: {kc.City} vs {ny.City}");
            Console.WriteLine("------------------------------------------------");

            // The GAME LOOP
            while (!currentGame.IsGameOver)
            {
                // 1. Print Header
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n[{currentGame.GetClockDisplay()}] SCORE: {kc.City} {currentGame.HomeScore} - {ny.City} {currentGame.AwayScore}");
                Console.ResetColor();

                // 2. Draw the Field
                FieldPrinter.DrawField(currentGame);

                // 3. Print Situation
                Console.WriteLine($"Situation: {currentGame.GetSituation()}");

                // 4. Simulate Play
                string result = PlayEngine.SimulatePlay(currentGame);
                Console.WriteLine($">> {result}");

                // 5. Speed Control
                if (currentGame.TimeRemaining == 900 && currentGame.CurrentQuarter > 1)
                {
                    Console.WriteLine("\n*** END OF QUARTER ***\n");
                    System.Threading.Thread.Sleep(2000);
                }
                else
                {
                    System.Threading.Thread.Sleep(1200); // Slightly slower to let you read the names
                }
            }

            //Post Game
            Console.WriteLine("\n=====================================");
            Console.WriteLine("FINAL SCORE");
            Console.WriteLine($"{kc.City}: {currentGame.HomeScore}");
            Console.WriteLine($"{ny.City}: {currentGame.AwayScore}");
            Console.WriteLine("=======================================");

            AFL_Simulation.Utils.BoxScore.PrintBoxScore(kc, ny);
        }

        static int GetTeamOvr(Team t)
        {
            if (t.Roster.Count == 0) return 0;
            int total = 0;
            foreach(var p in t.Roster) total += p.OverallRating;
            return total / t.Roster.Count;
        }
    }
}