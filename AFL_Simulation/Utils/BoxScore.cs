using System;
using AFL_Simulation.Models;

namespace AFL_Simulation.Utils
{
    public static class BoxScore
    {
        public static void PrintBoxScore(Team home, Team away)
        {
            Console.WriteLine("\n=====================================");
            Console.WriteLine("          FINAL BOX SCORE            ");
            Console.WriteLine("=====================================");

            PrintTeamStats(home);
            Console.WriteLine("\n-------------------------------------");
            PrintTeamStats(away);
        }

        private static void PrintTeamStats(Team t)
        {
            Console.WriteLine($"--- {t.City} {t.Name} ---");

            // Passing
            Player qb = t.GetStarter(Position.QB);
            Console.WriteLine($"PASSING: {qb.FirstName} {qb.LastName}: {qb.GameStats.Completions}/{qb.GameStats.PassAttempts} for {qb.GameStats.PassYards} yds, {qb.GameStats.PassTDs} TD");

            // Rushing (Find players with carries)
            Console.WriteLine("RUSHING:");
            foreach(var p in t.Roster)
            {
                if (p.GameStats.Carries > 0)
                {
                    Console.WriteLine($"  {p.LastName}: {p.GameStats.Carries} car, {p.GameStats.RushYards} yds, {p.GameStats.RushTDs} TD");
                }
            }

            // Receiving (Find players with catches)
            Console.WriteLine("RECEIVING:");
            foreach (var p in t.Roster)
            {
                if (p.GameStats.Receptions > 0)
                {
                    Console.WriteLine($"  {p.LastName}: {p.GameStats.Receptions} rec, {p.GameStats.RecYards} yds, {p.GameStats.RecTDs} TD");
                }
        }
    }
}
}