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

        public override string ToString()
        {
            return $"{City} {Name} (Coach: {Coach}) - Roster Size: {Roster.Count}";
        }
    }
}