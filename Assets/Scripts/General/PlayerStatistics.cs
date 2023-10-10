public static class PlayerStatistics
{
    // Hub
    public static bool visitedHub { get; set; } = false;

    // Oddly Satisfying
    public static bool completedOddlySatisfying { get; set; } = false;
    public static int highScoreOddlySatisfying { get; set; } = 0;

    // Memory Game
    public static bool completedMemoryGame { get; set; } = false;
    public static int highScoreMemoryGame { get; set; } = 0;

    // Crane Game
    public static bool completedCraneGame { get; set; } = false;
    public static int highScoreCraneGame { get; set; } = 0;

    // Doom Room
    public static bool visitedDoomRoom { get; set; } = false;

    public static int TotalGames()
    {
        return 3;
    }

    public static int CompletedGameCount()
    {
        int count = 0;

        if (completedOddlySatisfying)
        {
            count++;
        }
        if (completedMemoryGame)
        {
            count++;
        }
        if (completedCraneGame)
        {
            count++;
        }

        return count;
    }
}
