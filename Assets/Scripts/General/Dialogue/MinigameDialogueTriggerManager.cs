using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameDialogueTriggerManager : MonoBehaviour
{
    public enum SceneType {
        OddlySatisfying,
        MemoryGame,
        CraneGame
    }

    public SceneType sceneType;
    public GameObject originalTrigger;
    public GameObject revisitTrigger;

    void Start()
    {
        switch (sceneType)
        {
            case SceneType.OddlySatisfying:
                if (PlayerStatistics.completedOddlySatisfying)
                {
                    originalTrigger.SetActive(false);
                    revisitTrigger.SetActive(true);
                }
                else
                {
                    originalTrigger.SetActive(true);
                    revisitTrigger.SetActive(false);
                }
                break;
            case SceneType.MemoryGame:
                if (PlayerStatistics.completedMemoryGame)
                {
                    originalTrigger.SetActive(false);
                    revisitTrigger.SetActive(true);
                }
                else
                {
                    originalTrigger.SetActive(true);
                    revisitTrigger.SetActive(false);
                }
                break;
            case SceneType.CraneGame:
                if (PlayerStatistics.completedCraneGame)
                {
                    originalTrigger.SetActive(false);
                    revisitTrigger.SetActive(true);
                }
                else
                {
                    originalTrigger.SetActive(true);
                    revisitTrigger.SetActive(false);
                }
                break;
        }
    }

    public GameObject GetActiveDialogueTrigger()
    {
        switch (sceneType)
        {
            case SceneType.OddlySatisfying:
                if (PlayerStatistics.completedOddlySatisfying)
                {
                    return revisitTrigger;
                }
                else
                {
                    return originalTrigger;
                }
            case SceneType.MemoryGame:
                if (PlayerStatistics.completedMemoryGame)
                {
                    return revisitTrigger;
                }
                else
                {
                    return originalTrigger;
                }
            case SceneType.CraneGame:
                if (PlayerStatistics.completedCraneGame)
                {
                    return revisitTrigger;
                }
                else
                {
                    return originalTrigger;
                }
        }
        return null;
    }
}
