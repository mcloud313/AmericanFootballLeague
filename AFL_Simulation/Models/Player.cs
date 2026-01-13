using System;

namespace AFL_Simulation.Models 
{
    public class Player
    {
        // 1. Basic Info
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public int Age {get; set;}
        public Position Position {get; set;}

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

            // A simple list of 1950s style names
            string[] firstNames = { "Johnny", "Dick", "Bronko", "Chuck", "Vince", "Tom", "Hank", "Red", "Bubba", "Will", "Dwight", "Sam" };
            string[] lastNames = { "Unitas", "Butkus", "Starr", "Graham", "Nitschke", "Brown", "Huff", "Berry", "Green", "Black", "Miller" };

            string fName = firstNames[rand.Next(firstNames.Length)];
            string lName = lastNames[rand.Next(lastNames.Length)];

            // Generate a rating between 60 (Benchwarmer) and 85 (Legend)
            int rating = rand.Next(60, 100);

            return new Player(fName, lName, pos, rating);
        }

        // 5. A method to display the player neatly
        public override string ToString()
        {
            return $"{Position} {FirstName} {LastName} (OVR: {OverallRating})";
        }
    }
}