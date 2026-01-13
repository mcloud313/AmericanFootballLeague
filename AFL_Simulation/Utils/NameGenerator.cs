using System;
using System.Collections.Generic;

namespace AFL_Simulation.Utils
{
    public static class NameGenerator
    {
        private static Random _rand = new Random();

        // A simple "Set" to track names we have already handed out.
        // If we generate "Johnny Unitas" we add it here.
        // If we try to generate him again, we reject it and try again.
        private static HashSet<string> _usedNames = new HashSet<string>();

        // 1950s First Names
        private static string[] _firstNames =
        {
            "Johnny", "Dick", "Bronko", "Chuck", "Vince", "Hank", "Red", "Bubba",
            "Earl", "Frank", "Gino", "Lenny", "Mickey", "Norm", "Otto", "Pete",
            "Ray", "Mike", "Tom", "Sam", "Walt", "Zeke", "Y.A.", "Bill", "Bob",
            "Jim", "Jack", "Don", "Gary", "Ron", "Ken", "Larry", "Jerry"
        };

        //1950s Last Names
        private static string[] _lastNames =
        {
            "Unitas", "Butkus", "Starr", "Graham", "Nitschke", "Brown", "Huff", "Berry",
            "Van Brocklin", "Hirsch", "Bednarik", "Groza", "Tittle", "Layne", "Perry",
            "Matson", "Marchetti", "Schmidt", "Wilson", "Taylor", "Johnson", "Lewis",
            "Walker", "Robinson", "Young", "Allen", "King", "Wright", "Scott", "Hill"
        };

        public static string GetUniqueName()
        {
            //Try up to 10 times to find a unique name
            // (Prevents infinite loops if we run out of names)
            for (int i = 0; i < 10; i++)
            {
                string first = _firstNames[_rand.Next(_firstNames.Length)];
                string last = _lastNames[_rand.Next(_lastNames.Length)];
                string fullName = $"{first} {last}";

                // If this name hasn't been used yet...
                if (!_usedNames.Contains(fullName))
                {
                    _usedNames.Add(fullName); //Mark it as used
                    return fullName;
                }
            }

            //Fallback if we get really unlucky.
            return "John Doe";
        }

        public static void Reset()
        {
            _usedNames.Clear();
        }
    }
}