using System;
using AFL_Simulation.Models; 

namespace AFL_Simulation.Engine
{
    public static class PlayEngine
    {
        private static Random _rand = new Random();

        public static string SimulatePlay(Game game)
        {
            Team offense = game.Possession;
            Team defense = (game.Possession == game.HomeTeam) ? game.AwayTeam : game.HomeTeam;

            // 1. Simple AI: if 4th Down, Punt or Kick. Otherwise, random normal play.
            PlayType offPlay;
            if (game.Down == 4)
            {
                // If close enough (past midfield 65), try FG. Else Punt.
                if (game.BallOn > 65) offPlay = PlayType.FieldGoal;
                else offPlay = PlayType.Punt;
            }
            else
            {
                // Randomly pick Run/Pass for downs 1-3
                offPlay = GetRandomOffensivePlay();
                // Ensure we don't pick special teams plays on normal downs
                while (offPlay == PlayType.Punt || offPlay == PlayType.FieldGoal)
                {
                    offPlay = GetRandomOffensivePlay();
                }
            }

            DefensiveStrategy defStrat = GetRandomDefensivePlay();

            // 2. Handle Special Teams Separately
            if (offPlay == PlayType.Punt)
            {
                game.SwitchPossession();
                return $"{offense.City} punts. {defense.City} takes over.";
            }
            if (offPlay == PlayType.FieldGoal)
            {
                // Simple 80% success rate for now
                if (_rand.Next(1, 101) > 20)
                {
                    if (offense == game.HomeTeam) game.HomeScore += 3;
                    else game.AwayScore += 3;

                    string result = $"{offense.City} KICKS THE FIELD GOAL! IT IS GOOD!";
                    game.SwitchPossession(); // Kickoff (Simplified to touchback)
                    return result;
                }
                else
                {
                    game.SwitchPossession();
                    return $"{offense.City} missed the kick! Turnover.";
                }
            }

            // 3. Normal Play Logic (Run/Pass)
            double offStrength = GetTeamAverage(offense);
            double defStrength = GetTeamAverage(defense);

            // Calculate Gain
            int roll = _rand.Next(-5, 15);
            int yardsGained = (int)(2 + roll); // simplified math for testing

            // Update Game State
            game.BallOn += yardsGained;
            game.YardsToGo -= yardsGained;
            string playResult = $"{offPlay}: Gained {yardsGained} yards.";

            // Check for First Down
            if (game.YardsToGo <= 0)
            {
                game.Down = 1;
                game.YardsToGo = 10;
                playResult += " FIRST DOWN!";
            }
            else
            {
                game.Down++;
            }

            // Check for Touchdown
            if (game.BallOn >= 100)
            {
                playResult += $" TOUCHDOWN {offense.City}!";
                if (offense == game.HomeTeam) game.HomeScore += 7; // Auto-extra point for now
                else game.AwayScore += 7;
                game.SwitchPossession();
            }
            // Check for Turnover on Downs (failed 4th down)
            else if (game.Down > 4)
            {
                playResult += " Turnover on Downs!";
                game.SwitchPossession();
            }

            return playResult;
        }


        // Helper: Get Random play
        private static PlayType GetRandomOffensivePlay()
        {
            var values = Enum.GetValues(typeof(PlayType));
            return (PlayType)values.GetValue(_rand.Next(values.Length));
        }

        private static DefensiveStrategy GetRandomDefensivePlay()
        {
            var values = Enum.GetValues(typeof(DefensiveStrategy));
            return (DefensiveStrategy)values.GetValue(_rand.Next(values.Length));
        }

        private static double GetTeamAverage(Team t)
        {
            if (t.Roster.Count == 0) return 50; //Fallback for empty teams

            double total = 0;
            foreach (var p in t.Roster) total += p.OverallRating;
            return total / t.Roster.Count;
        }
    }
}