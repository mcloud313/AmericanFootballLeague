namespace AFL_Simulation.Models
{
    public enum PlayType
    {
        RunInside, // Strong, low risk, low reward
        RunOutside, // Fast, medium risk, medium reward
        ShortPass, // Safe, high completion %
        LongPass, // Risky, low completion %, massive reward
        Punt,
        FieldGoal
    }

    public enum DefensiveStrategy
    {
        Base43, // Balanced
        RunBlitz, // Good vs Run, Bad vs Pass
        PassCover, // Good vs Pass, Bad vs Run
        AllOutBlitz // High Risk / High Reward
    }
}