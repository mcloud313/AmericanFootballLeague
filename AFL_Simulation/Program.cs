using System;
using AFL_Simulation.Models;

namespace AFL_Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- AFL League Office (1950) --- ");

            // 1. Create a Team
            Team kc = new Team("Kansas City", "Plainsmen", "Coach Marty");
            Console.WriteLine($"Founded: {kc.ToString()}");

            // 2. Create our star rookie
            Player rookie = Player.CreateRandomPlayer(Position.QB);
            Console.WriteLine($"\nScouted Top Prospect: {rookie.ToString()}");

            // 3. Sign him!
            kc.SignPlayer(rookie);
            Console.WriteLine($"\nBREAKING NEWS: {kc.City} {kc.Name} sign {rookie.FirstName} {rookie.LastName}!");

            // 4. Fill the rest of the roster with randoms
            kc.FillRosterWithScabs();

            // 5. Print the Roster
            Console.WriteLine($"\n --- {kc.Name} Roster --- ");
            foreach (Player p in kc.Roster)
            {
                Console.WriteLine($"- {p.Position}: {p.FirstName} {p.LastName} ({p.OverallRating})");
            }
        }
    }
}