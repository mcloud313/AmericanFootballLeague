using System;

namespace AFL_Simulation.Models
{
    public class Game
    {
        // Teams
        public Team HomeTeam {get; set; }
        public Team AwayTeam {get; set; }

        //Scoreboard
        public int HomeScore {get; set; }
        public int AwayScore {get; set; }

        //Field State
        public Team Possession {get; set; } // Who has the ball?
        public int BallOn {get; set; } // 0 to 100 (0 = Own Endzone, 50 = Midfield, 100 = TD)
        public int Down {get; set; } // 1, 2, 3, 4
        public int YardsToGo {get; set; } // Usually 10

        // Constructor sets up the coin toss (sort of)
        public Game(Team home, Team away)
        {
            HomeTeam = home;
            AwayTeam = away;
            HomeScore = 0;
            AwayScore = 0;

            // Default start: Home team gets ball at their own 20
            Possession = HomeTeam;
            BallOn = 20;
            Down = 1;
            YardsToGo = 10;
        }

        public void SwitchPossession()
        {
            if (Possession == HomeTeam) Possession = AwayTeam;
            else Possession = HomeTeam;

            // Flip field position (If I was at my 40, you are now at your 60...)
            // Simplified: Reset to 20 for now on turnover
            BallOn = 20;
            Down = 1;
            YardsToGo = 10;
        }

        public string GetSituation()
        {
            string loc = BallOn > 50 ? $"Opp {100 - BallOn}" : $"Own {BallOn}";
            return $"{Possession.City} Ball | {Down} & {YardsToGo} @ {loc}";
        }
    }
}