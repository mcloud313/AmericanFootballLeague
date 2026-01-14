using System;
using AFL_Simulation.Models;

namespace AFL_simulation.Utils
{
    public static class FieldPrinter
    {
        public static void DrawField(Game game)
        {
            // We want a bar 50 chars wide representing 100 yards.
            // Each char = 2 yards.

            int absolutePosition;

            //1. Convert "Offense Relative" position to "Absolute (Left-to-Right)" Position
            //Let's assume Home Team is LEFT (0) and Away team is RIGHT (100).
            if (game.Possession == game.HomeTeam)
            {
                absolutePosition = game.BallOn; // e.g. Own 20 -> 30
            }
            else
            {
                absolutePosition = 100 - game.BallOn; // e.g. Own 20 - 80
            }

            int markerPos = absolutePosition / 2;

            // Clamp marker to be safe
            if (markerPos < 0) markerPos = 0;
            if (markerPos > 49) markerPos = 49;

            // 3. Draw the field
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[");

            for (int i = 0; i < 50; i++)
            {
                if (i == markerPos)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("O"); // The Ball
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (i == 25)
                {
                    Console.Write("|"); // Midfield Line
                }
                else
                {
                   Console.Write("-"); 
                }
            }
            Console.Write("]");
            Console.ResetColor();
            Console.WriteLine();

            // 4. Draw Endzones Labels
            Console.WriteLine($"{game.HomeTeam.City.PadRight(25)} {game.AwayTeam.City.PadLeft(26)}");
        }
    }
}