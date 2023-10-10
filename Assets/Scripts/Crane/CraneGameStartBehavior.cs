using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General.Dialogue;
public class CraneGameStartBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public CraneGameManager gameManager;
    public MinigameDialogueTriggerManager dialogueTriggerManager;
    public DialogueManager dialogueManager;

    DialogueTrigger dialogueTrigger;
    bool started;

    void Start()
    {
        started = false;
        dialogueTrigger = dialogueTriggerManager.GetActiveDialogueTrigger().GetComponent<DialogueTrigger>();

    }

    // Update is called once per frame
    void Update()
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
