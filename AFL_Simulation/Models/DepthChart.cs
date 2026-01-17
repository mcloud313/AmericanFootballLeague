using System.Collections.Generic;
using System.Linq;

namespace AFL_Simulation.Models
{
    public class DepthChart
    {
        // Maps a position to a ranked list of players (Index 0 = Starter, 1 = Backup)
        public Dictionary<Position, List<Player>> Chart { get; set; } = new Dictionary<Position, List<Player>>();

        // Dedicated Slots for Specialists
        public Player? Kicker {get; set; }
        public Player? Punter {get; set; }

        public void SetStarter(Position pos, Player p)
        {
            if (!Chart.ContainsKey(pos)) Chart[pos] = new List<Player>();

            // Remove him if he's already in the lsit elsewhere
            Chart[pos].Remove(p);

            //Insert at the top
            Chart[pos].Insert(0, p);

        }
        // The AI Logic: Auto-sort everyone by rating
        public void AutoReorder(List<Player> fullRoster)
        {
            // 1. Group players by position
            foreach (Position pos in System.Enum.GetValues(typeof(Position)))
            {
                var playersAtPos = fullRoster
                    .Where(p => p.Position == pos)
                    .OrderByDescending(p => p.OverallRating) // Best rating first
                    .ToList();

                Chart[pos] = playersAtPos;
            }

            // 2. Set Specialists (Best Kicker becomes the Kicker)
            var kickers = fullRoster.Where(p => p.Position == Position.K).OrderByDescending(p => p.OverallRating).ToList();
            if (kickers.Count > 0) Kicker = kickers[0];
            
            // For now, Punter is also the Kicker or a backup QB if no K exists
            Punter = Kicker; 
        }

        public Player GetPlayerForSnap(Position pos)
        {
            if (!Chart.ContainsKey(pos) || Chart[pos].Count == 0) return null;

            List<Player> depth = Chart[pos];
            Player starter = depth[0];

            //Substitution logic
            // If starter is tired (Energy <50) and we have backup, play the backup
            if (starter.Energy < 50 && depth.Count > 1)
            {
                return depth[1];
            }

            return starter;
        }
    }
}