using System;
using AFL_Simulation.Models; // Let's us see the Player Class we just made!

namespace AFL_Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---AFL Scout Report (1950) ---");

            //Create a random Quarterback using our helper method
            Player myQB = Player.CreateRandomPlayer(Position.QB);

            // Print the result
            Console.WriteLine("Scouting Report:");
            Console.WriteLine(myQB.ToString());

            // Simple logic test
            if (myQB.OverallRating > 90)
            {
                Console.WriteLine("Scout says: Draft this guy immediately! He's a legend!");
            }
            else if (myQB.OverallRating > 80)
            {
                Console.WriteLine("Scout says: Solid Starter.");
            }
            else
            {
                Console.WriteLine("Scout says: Need to see more tape. Might be a backup.");
            }
        }
    }
}