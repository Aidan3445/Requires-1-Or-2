using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameStatusUIController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text statusText;
    public float cycleDelay = 2.0f;

    int index = 0;
    List<string> statuses;

    void Start()
    {
        // Set the title text
        titleText.text = "Test Status:";

        // Populate the statuses list
        statuses = new List<string>();

        string oddlySatisfyingStatus = "";
        // Oddly satisfying
        oddlySatisfyingStatus += "Oddly Satisfying:\n";
        oddlySatisfyingStatus += "Status: " + (PlayerStatistics.completedOddlySatisfying ? "Complete" : "Incomplete") + "\n";
        oddlySatisfyingStatus += "High Score: " + PlayerStatistics.highScoreOddlySatisfying + "\n";
        statuses.Add(oddlySatisfyingStatus);

        // Memory game
        string memoryGameStatus = "";
        memoryGameStatus += "Memory Game:\n";
        memoryGameStatus += "Status: " + (PlayerStatistics.completedMemoryGame ? "Complete" : "Incomplete") + "\n";
        memoryGameStatus += "High Score: " + PlayerStatistics.highScoreMemoryGame + "\n";
        statuses.Add(memoryGameStatus);

        // Crane game
        string craneGameStatus = "";
        craneGameStatus += "Crane Game:\n";
        craneGameStatus += "Status: " + (PlayerStatistics.completedCraneGame ? "Complete" : "Incomplete") + "\n";
        craneGameStatus += "High Score: " + PlayerStatistics.highScoreCraneGame + "\n";
        statuses.Add(craneGameStatus);

        InvokeRepeating("DisplayNextStatus", 0f, cycleDelay);
    }

    void DisplayNextStatus()
    {
        index = (index + 1) % statuses.Count;
        statusText.text = statuses[index];
    }
}
