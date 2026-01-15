using System;
using System.Collections.Generic; // Required for using Lists.

namespace AFL_Simulation.Models
{
    public class Team
    {
        public string Name {get; set; }
        public string City {get; set; }
        public string Coach {get; set; }
        public List<Player> Roster {get; set; } = new List<Player>();
        public Team(string city, string name, string coach)
        {
            City = city;
            Name = name;
            Coach = coach;
        }

        // Helper: Sign a player to the team
        public void SignPlayer(Player p)
        {
            Roster.Add(p);
        }

        //Helper: Generate a full roster of random players (for testing)
        public void FillRosterWithScabs()
        {
            // Add 1 QB
            Roster.Add(Player.CreateRandomPlayer(Position.QB));

            // Add 2 RBs
            Roster.Add(Player.CreateRandomPlayer(Position.RB));
            Roster.Add(Player.CreateRandomPlayer(Position.RB));

            // Add 3 WRs
            Roster.Add(Player.CreateRandomPlayer(Position.WR));
            Roster.Add(Player.CreateRandomPlayer(Position.WR));
            Roster.Add(Player.CreateRandomPlayer(Position.WR));
        }

        public Player GetStarter(Position pos)
        {
            // LINQ would be shorter, but let's stick to simple loops for clarity
            Player? best = null;

            foreach (var p in Roster)
            {
                if (p.Position == pos)
                {
                    if (best == null || p.OverallRating > best.OverallRating)
                    {
                        best = p;
                    }
                }
            }

            // If we have no player at that position, return a generic "Scab" so the game doesn't crash
            if (best == null) return new Player("Scab", "Player", pos, 50);

            return best;
        }

        //Helper: get a random target for passing (WR, TE, RB)
        public Player GetRandomReceiver()
        {
            var receivers = new List<Player>();
            foreach(var p in Roster)
            {
                if (p.Position == Position.WR || p.Position == Position.RB)
                {
                    receivers.Add(p);
                }
            }

            if (receivers.Count == 0) return new Player("Scab", "Receiver", Position.WR, 50);

            var rand = new Random();
            return receivers[rand.Next(receivers.Count)];
        }

        public override string ToString()
        {
            return $"{City} {Name} (Coach: {Coach}) - Roster Size: {Roster.Count}";
        }
    }
}