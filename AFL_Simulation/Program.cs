using System;
using AFL_Simulation.Models;
using AFL_Simulation.Engine; 

namespace AFL_Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- AFL Drive Simulator (v0.2) ---");

            // Setup
            Team kc = new Team("Kansas City", "Plainsmen", "Coach Marty");
            kc.FillRosterWithScabs();
            Team ny = new Team("New York", "Empire", "Coach Parcells");
            ny.FillRosterWithScabs();

            // Create the Game State
            Game currentGame = new Game(kc, ny);

            Console.WriteLine("Kickoff! Kansas City starts at their own 20.");
            Console.WriteLine("------------------------------------------------");

            // Loop for 10 plays or until score changes
            for (int i = 1; i <= 15; i++)
            {
                Console.WriteLine($"\nPlay {i} | {currentGame.GetSituation()}");

                string result = PlayEngine.SimulatePlay(currentGame);
                Console.WriteLine($">> {result}");

                // Scoreboard Check
                if (currentGame.HomeScore > 0 || currentGame.AwayScore > 0)
                {
                    Console.WriteLine($"\nSCORE CHANGE! KC: {currentGame.HomeScore} - NY: {currentGame.AwayScore}");
                    break;
                }

                System.Threading.Thread.Sleep(1000);
            }
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