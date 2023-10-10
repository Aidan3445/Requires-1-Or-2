using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using General.UI;
using UnityEngine.Events;

public class MemoryGameManager : MonoBehaviour
{
    public GridBehavior gridBehavior;
    public float challengeTime = 90.0f;
    public int penalty = -5;
    public int success = 15;
    public CommonUiController mainMenu;
    public AudioClip winSound;
    public AudioClip loseSound;
    public ScoreboardUIController scoreboard;
    public UnityEvent cheer;
    public UnityEvent degrade;

    bool isActiveGame;
    float countDown;
    bool penalized;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        isActiveGame = false;
        penalized = false;
        countDown = challengeTime;
        score = 0;

        UpdateScore(score);
        UpdateScoreboardBody("Memory Game");
        scoreboard.UpdateHighScore(PlayerStatistics.highScoreMemoryGame);
        SetTimerText();

    }

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

    public void Success()
    {
        UpdateScore(success);
        cheer.Invoke();
    }

    public void Penalize()
    {
        penalized = true;
        degrade.Invoke();
    }

    public void StartGame()
    {
        if (!isActiveGame)
        {
            score = 0;
            gridBehavior.HideAllCellText();
            isActiveGame = true;

            // hide menu
            if (mainMenu.GetActive())
            {
                mainMenu.Toggle();
            }
        }
    }

    public void GameOver()
    {
        // Game is over
        isActiveGame = false;

        if (score > PlayerStatistics.highScoreMemoryGame)
        {
            scoreboard.SetBody("New High Score!");
            PlayerStatistics.highScoreMemoryGame = score;
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
            PlayerStatistics.completedMemoryGame = true;
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

    public bool GetGameStatus()
    {
        return isActiveGame;
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
}
