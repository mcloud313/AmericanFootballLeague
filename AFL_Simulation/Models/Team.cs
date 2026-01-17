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
        public DepthChart DepthChart {get; set; } = new DepthChart();
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
            DepthChart.AutoReorder(Roster);
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

            DepthChart.AutoReorder(Roster);
        }

        public Player GetStarter(Position pos)
        {
            Player p = DepthChart.GetPlayerForSnap(pos);

            //Safety check: if chart is empty return a temp scab
            if (p == null) return new Player("Scab", "Player", pos, 50);
            return p;
        }

        public Player GetKicker()
        {
            if (DepthChart.Kicker != null) return DepthChart.Kicker;
            return new Player("Scab", "Kicker", Position.K, 50);
        }

        public Player GetPunter()
        {
            if (DepthChart.Punter != null) return DepthChart.Punter;
            return new Player("Scab", "Punter", Position.K, 50);
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