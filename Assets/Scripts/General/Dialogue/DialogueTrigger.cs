using System.Text.RegularExpressions;
using UnityEngine;

namespace General.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public DialogueManager dialogueManager;
        public TextAsset textFile;
        public bool repeatable = false;

        bool hasBeenTriggered;
        global::Dialogue dialogue;

        private void Awake()
        {
            hasBeenTriggered = false;
            InitializeDialogue();
        }

        void InitializeDialogue()
        {
            dialogue = new global::Dialogue();

            dialogue.repeatable = repeatable;

            string txt = textFile.text;
            string[] txtLines = Regex.Split(txt, "\n");

            // The first line is the NPC name
            dialogue.name = txtLines[0];

            // The remaining lines are dialogue
            for (int i = 1; i < txtLines.Length; i++)
            {
                string valueLine = txtLines[i];
                dialogue.sentences.Add(valueLine);
            }
        }

        public void TriggerDialogue()
        {
            dialogue.hasPlayed = hasBeenTriggered;
            dialogueManager.StartDialogue(dialogue);
            hasBeenTriggered = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GameController"))
            {
                TriggerDialogue();
            }
        }

        public bool TriggerStatus()
        {
            return hasBeenTriggered;
        }
    }
}
