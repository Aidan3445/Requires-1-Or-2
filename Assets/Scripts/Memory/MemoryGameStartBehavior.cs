using System.Collections;
using System.Collections.Generic;
using General.Dialogue;
using UnityEngine;

public class MemoryGameStartBehavior : MonoBehaviour
{
    public MemoryGameManager gameManager;
    public MinigameDialogueTriggerManager dialogueTriggerManager;
    public DialogueManager dialogueManager;

    DialogueTrigger dialogueTrigger;
    bool started;

    private void Start()
    {
        started = false;
        dialogueTrigger = dialogueTriggerManager.GetActiveDialogueTrigger().GetComponent<DialogueTrigger>();
    }

    private void Update()
    {
        if (!started)
        {
            if (dialogueTrigger.TriggerStatus() && dialogueManager.SentencesLeft() == 0)
            {
                gameManager.StartGame();
                started = true;
            }
        }
    }
}
