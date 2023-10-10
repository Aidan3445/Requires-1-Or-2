using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General.UI;
using UnityEngine.Events;
using General.Dialogue;

public class CraneGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float challengeTime = 90.0f;
    public int penalty = -5;
    public int success = 15;
    public CommonUiController mainMenu;
    public AudioClip winSound;
    public AudioClip loseSound;
    public ScoreboardUIController scoreboard;
    private DialogueTrigger dialogue;
    public UnityEvent cheer;
    public UnityEvent degrade;
    [SerializeField] private MinigameDialogueTriggerManager dialogueTriggerManager;

    bool isActiveGame;
    float countDown;
    bool penalized;
    int score;

    void Start()
    {
        isActiveGame = false;
        penalized = false;
        countDown = challengeTime;
        score = 0;

        UpdateScore(score);
        UpdateScoreboardBody("Crane Game");
        scoreboard.UpdateHighScore(PlayerStatistics.highScoreCraneGame);
        SetTimerText();
        dialogue = dialogueTriggerManager.GetActiveDialogueTrigger().GetComponent<DialogueTrigger>();

        StartCoroutine(StartDialogue());


    }

    // Update is called once per frame
    void Update()
    {

        if (isActiveGame)
        {
            if (countDown > 0)
            {
                // Decrement by the time passed
                countDown -= Time.deltaTime;

                // Apply penalty if necessary and reset flag
                if (penalized)
                {
                    UpdateScore(penalty);
                    penalized = false;
                }
            }
            else
            {
                countDown = 0.0f;
                GameOver();
            }
            SetTimerText();
        }

    }

    public bool GetGameStatus()
    {
        return isActiveGame;
    }

    public void Success()
    {
        UpdateScore(success);
        cheer.Invoke();
        UpdateScoreboardBody("Matched!");
    }

    public void Penalize()
    {
        penalized = true;
        degrade.Invoke();
        UpdateScoreboardBody("Incorrect!");
    }

    public void GameOver()
    {
        // Game is over
        // isActiveGame = false;
        //  string message = GetGameOverMessage();
        //  scoreboard.SetBody(message);
        // TODO: play different sounds based on bracket
        //    AudioSource.PlayClipAtPoint(winSound, transform.position);

        isActiveGame = false;

        if (score > PlayerStatistics.highScoreCraneGame)
        {
            scoreboard.SetBody("New High Score!");
            PlayerStatistics.highScoreCraneGame = score;
            scoreboard.UpdateHighScore(score);
        }
        else
        {
            scoreboard.SetBody("Game Over");
        }
        AudioSource.PlayClipAtPoint(score > 0 ? winSound : loseSound, transform.position);

        if (score > 0)
        {
            cheer.Invoke();
            PlayerStatistics.completedCraneGame = true;
        }
        else
        {
            degrade.Invoke();
        }
        
        
        IEnumerator OpenMenu()
        {
            yield return new WaitForSeconds(5f);
            if (!mainMenu.GetActive())
            {
                mainMenu.Toggle();
            }
        }

        // open menu after game
        StartCoroutine(OpenMenu());
    }

    public void StartGame()
    {
        if (!isActiveGame)
        {
            score = 0;
            isActiveGame = true;
            
            // hide menu
            if (mainMenu.GetActive())
            {
                mainMenu.Toggle();
            }
        }
    }



    private string GetGameOverMessage()
    {

         if (score >= 0)
        {
            return "Game Over";
        }
        else
        {
            return "Yikes... Try Again";
        }
    }

    void SetTimerText()
    {
        scoreboard.UpdateTimer((int)countDown);
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreboard.UpdateScore(score, points);
    }

    public void UpdateScoreboardBody(string bodyText)
    {
        scoreboard.SetBody(bodyText);
    }

    public bool getIsActiveGame()
    {
        return isActiveGame;
    }

    private IEnumerator StartDialogue()
    {
        // start dialogue
        dialogue.TriggerDialogue();

        // wait for dialogue to finish
        yield return new WaitWhile(() => dialogue.dialogueManager.SentencesLeft() > 0);

    }
}
