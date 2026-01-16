using System;
using AFL_Simulation.Utils;

namespace AFL_Simulation.Models 
{
    public class Player
    {
        // 1. Basic Info
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public int Age {get; set;}
        public Position Position {get; set;}
        public PlayerStats GameStats {get; set; } = new PlayerStats();

        // 2. Attributes (0-100 Scale)
        // We keep it simple for v0.1: One rating to rule them all.
        // Later we will break this into Speed, Strength, etc.
        public int OverallRating {get; set; }

        // 3. Constructor (How we create a new player)
        public Player(string firstName, string lastName, Position position, int rating)
        {
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Age = 21; // Rookies start at 21
            OverallRating = rating;
        }

        // 4. The "Generator" - A helper to make a random player instantly
        public static Player CreateRandomPlayer(Position pos)
        {
            var rand = new Random();

            // Get a unique full name from our generator
            string fullName = NameGenerator.GetUniqueName();

            //Split it back into First/Last
            string[] parts = fullName.Split(' ');
            string fName = parts[0];
            //Handle last names that may have spaces like Van Brocklin
            string lName = parts.Length > 2 ? $"{parts[1]} {parts[2]}" : parts[1];
            
            // --- BETTER RATING LOGIC ---
            // We roll a 100-sided die to determine the "Tier" of the player.
            // This prevents too many 90+ rated rookies

            int roll = rand.Next(1, 101);
            int rating;

            if (roll <= 5) //5% chance: "Bust" / Camp Body
                rating = rand.Next(50, 59);
            else if (roll <= 60) // 55% Average Rookie
                rating = rand.Next(60, 74);
            else if (roll <= 90) // 30% chance: Good starter potential
                rating = rand.Next(75, 84);
            else if (roll <= 98) // 8% chance Star Potential
                rating = rand.Next(85, 89);
            else
                rating = rand.Next(90, 95);
            return new Player(fName, lName, pos, rating);
        }

        // 5. A method to display the player neatly
        public override string ToString()
        {
            return $"{Position} {FirstName} {LastName} (OVR: {OverallRating})";
        }
    }

    public class PlayerStats
    {
        //Passing
        public int PassAttempts {get; set; }
        public int Completions {get; set; }
        public int PassYards {get; set; }
        public int PassTDs {get; set; }
        public int Interceptions {get; set; }
        public int SacksTaken {get; set; }

        //Rushing
        public int Carries {get; set; }
        public int RushYards {get; set; }
        public int RushTDs {get; set; }
        public int Fumbles {get; set; }

        //Receiving
        public int Receptions {get; set; }
        public int RecYards {get; set; }
        public int RecTDs {get; set; }

        //Defense
        public int Tackles {get; set; }
        public int SacksRecorded {get; set; }
        public int InterceptionsCaught {get; set; }
    }
}