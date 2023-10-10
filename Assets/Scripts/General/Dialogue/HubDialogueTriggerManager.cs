using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDialogueTriggerManager : MonoBehaviour
{
    public List<GameObject> gameTriggers;
    public GameObject revisitTrigger;

    void Start()
    {
        int totalGames = PlayerStatistics.TotalGames();
        int completedGames = PlayerStatistics.CompletedGameCount();

        if (completedGames == totalGames && PlayerStatistics.visitedDoomRoom)
        {
            // Set all game triggers inactive
            SetAllButIndexInactive(-1);
            revisitTrigger.SetActive(true);
        }
        else
        {
            SetAllButIndexInactive(completedGames);
            revisitTrigger.SetActive(false);
        }
    }

    void SetAllButIndexInactive(int index)
    {
        for (int i = 0; i < gameTriggers.Count; i++)
        {
            if (i == index)
            {
                gameTriggers[i].SetActive(true);
            }
            else
            {
                gameTriggers[i].SetActive(false);
            }
        }
    }
}
