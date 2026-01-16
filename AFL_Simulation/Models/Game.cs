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
        public Team Possession {get; set; }
        public int BallOn {get; set; }
        public int Down {get; set; }

        public int YardsToGo {get; set; }

        // --- New: Time State ---
        public int CurrentQuarter {get; set; }
        public int TimeRemaining {get; set; }
        public bool IsGameOver {get; set; }

        public Game(Team home, Team away)
        {
            HomeTeam = home;
            AwayTeam = away;
            HomeScore = 0;
            AwayScore = 0;

            // Start of Game
            Possession = HomeTeam;
            BallOn = 20;
            Down = 1;
            YardsToGo = 10;

            // Clock setup
            CurrentQuarter = 1;
            TimeRemaining = 900; // 15 minutes
            IsGameOver = false;
        }

        public void SwitchPossession()
        {
            Possession = (Possession == HomeTeam) ? AwayTeam : HomeTeam;
            BallOn = 20; // Reset to 20 for now (Touchback/punt landing)
            Down = 1;
            YardsToGo = 10;
        }

        // --- NEW: Clock Management ---
        public void DecrementTime(int seconds)
        {
            TimeRemaining -= seconds;

            if (TimeRemaining <= 0)
            {
                EndQuarter();
            }
        }

        private void EndQuarter()
        {
            if (CurrentQuarter == 4)
            {
                // check for tie
                if (HomeScore == AwayScore)
                {
                    CurrentQuarter = 5; // Overtime
                    TimeRemaining = 900; // 15 more minutes
                    //In real life there is a coin toss, here we'll just keep playing
                }
                else
                {
                    TimeRemaining = 0;
                    IsGameOver = true;
                }
            }
            else if (CurrentQuarter >= 5)
            {
                // End of OT
                TimeRemaining = 0;
                IsGameOver = true;
            }
            else
            {
                CurrentQuarter++;
                TimeRemaining = 900; // reset to 15 minutes
            }
        }

        //Helper to show "14:05" instead of 845 seconds.
        public string GetClockDisplay()
        {
            int minutes = TimeRemaining / 60;
            int seconds = TimeRemaining % 60;
            return $"Q{CurrentQuarter} {minutes:D2}:{seconds:D2}";
        }

        public string GetSituation()
        {
            string loc = BallOn > 50 ? $"Opp {100 - BallOn}" : $"Own {BallOn}";
            return $"{GetClockDisplay()} | {Possession.City} Ball | {Down} & {YardsToGo} @ {loc}";
        }
    }
}