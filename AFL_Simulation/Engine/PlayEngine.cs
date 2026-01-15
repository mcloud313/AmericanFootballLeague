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

           // 1. Identify Key Actors (The Stars of the Show)
           Player qb = offense.GetStarter(Position.QB);
           Player rb = offense.GetStarter(Position.RB);
           Player target = offense.GetRandomReceiver();

           //2. Play Selection
           PlayType offPlay;
           if (game.Down == 4)
            {
                if (game.BallOn > 65) offPlay = PlayType.FieldGoal;
                else offPlay = PlayType.Punt;
            }
            else
            {
                if (game.YardsToGo < 3) offPlay = PlayType.RunInside;
                else offPlay = GetRandomOffensivePlay();

                while (offPlay == PlayType.Punt || offPlay == PlayType.FieldGoal)
                {
                    offPlay = GetRandomOffensivePlay();
                }
            }

            // 3. Handle Special Teams (Simplified for Now)
            if (offPlay == PlayType.Punt)
            {
                game.DecrementTime(10);
                game.SwitchPossession();
                //Get the Kicker if you have one, or just the QB punts (old school style)
                return $"{qb.LastName} punts. {defense.City} takes over.";
            }
            if (offPlay == PlayType.FieldGoal)
            {
                game.DecrementTime(5);
                Player kicker = offense.GetStarter(Position.K);

                // Use Kicker's rating later? For now, standard RNG
                if (_rand.Next(1, 101) > 20)
                {
                    if (offense == game.HomeTeam) game.HomeScore += 3;
                    else game.AwayScore += 3;
                    game.SwitchPossession();
                    return $"{kicker.FirstName} {kicker.LastName} KICKS THE FIELD GOAL! IT IS GOOD!";
                }
                else
                {
                    game.SwitchPossession();
                    return $"{kicker.LastName} missed the kick! Turnover.";
                }
            }

            // 4. Resolve Normal Plays (Narrative Style)
            int timeConsumed = 0;
            int yardsGained = 0;
            string narrative = "";

            if (offPlay == PlayType.RunInside || offPlay == PlayType.RunOutside)
            {
                // RUN PLAY
                timeConsumed = 35;

                // Calculate gain based on RB rating vs Defense Average
                // (We will make this more complex later)
                int baseRoll = _rand.Next(-3, 8); // -3 to +7 base
                int skillBonus = (rb.OverallRating - 70) / 5; //Bonus for good RBs
                yardsGained = baseRoll + skillBonus;

                //Narrative
                if (yardsGained <0) narrative = $"{rb.LastName} is stuffed in the backfield for a loss of {-yardsGained}!";
                else if (yardsGained == 0) narrative = $"{rb.LastName} is tackled at the line of scrimmage.";
                else if (yardsGained > 10) narrative = $"{rb.LastName} BREAKS FREE! A huge run for {yardsGained} yards!";
                else narrative = $"{rb.LastName} carries the ball for {yardsGained} yards.";

                //NEW: Record Stats
                rb.GameStats.Carries++;
                rb.GameStats.RushYards += yardsGained;
                if (yardsGained > 0 && (game.BallOn + yardsGained >= 100))
                    rb.GameStats.RushTDs++;
            }
            else
            {
                // PASS PLAY
                bool isIncomplete = _rand.Next(0, 10) > 6; // 30% incompletion rate TODO: This shouldn't be hard coded should be based on the QB's skill vs Safeties/Cornerbacks

                if (isIncomplete)
                {
                    yardsGained = 0;
                    timeConsumed = 6;
                    narrative = $"{qb.LastName} looks deep... Incomplete! Intended for {target.LastName}.";
                }
                else
                {
                    timeConsumed = 30;
                    int baseRoll = _rand.Next(2, 15);
                    int skillBonus = (qb.OverallRating - 70) / 5;
                    yardsGained = baseRoll + skillBonus;

                    if (yardsGained > 20) narrative = $"{qb.LastName} connects with {target.LastName} for a BIG GAIN of {yardsGained}!";
                    else narrative = $"{qb.LastName} finds {target.LastName} for {yardsGained} yards.";

                    //NEW: Record Stats
                    qb.GameStats.Completions++;
                    qb.GameStats.PassYards += yardsGained;

                    target.GameStats.Receptions++;
                    target.GameStats.RecYards += yardsGained;

                    if (yardsGained > 0 && (game.BallOn + yardsGained >= 100))
                    {
                        qb.GameStats.PassTDs++;
                        target.GameStats.RecTDs++;
                    }
                }
                
            }

            // 5. Apply Results
            game.DecrementTime(timeConsumed);
            game.BallOn += yardsGained;
            game.YardsToGo -= yardsGained;

            // Append Situation info
            string resultLog = $"[{offPlay}] {narrative}";

            // Check Down/Score
            if (game.YardsToGo <= 0)
            {
                game.Down = 1;
                game.YardsToGo = 10;
                resultLog += " FIRST DOWN!";
            }
            else
            {
                game.Down++;
            }

            if (game.BallOn >= 100)
            {
                resultLog += $" TOUCHDOWN {offense.City}!";
                if (offense == game.HomeTeam) game.HomeScore += 7;
                else game.AwayScore += 7;
                game.SwitchPossession();
            }
            else if (game.Down > 4)
            {
                resultLog += " Turnover on Downs!";
                game.SwitchPossession();
            }

            return resultLog;
        }


        // Helper: Get Random play
        private static PlayType GetRandomOffensivePlay()
        {
            var values = Enum.GetValues<PlayType>(); 
            return values[_rand.Next(values.Length)];
        }

        private static DefensiveStrategy GetRandomDefensivePlay()
        {
            var values = Enum.GetValues<DefensiveStrategy>();
            return values[_rand.Next(values.Length)];
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