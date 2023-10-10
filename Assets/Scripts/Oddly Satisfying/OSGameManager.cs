using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using General;
using General.Dialogue;
using General.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Oddly_Satisfying
{
    public class OSGameManager : MonoBehaviour
    {
        [SerializeField] private List<OSTarget> targets;

        [SerializeField] private int targetCount = 15;

        [SerializeField] private float targetDelay = 2f;

        [SerializeField] private float travelSpeed = .1f;

        [SerializeField] private float platformLength = 10f;

        [SerializeField] private float spawnRadius = 0.25f;

        private List<OSTarget> liveTargets;

        private int count;

        private int score;

        private bool playing;

        private List<ROOTPlayer> players;

        private DialogueTrigger dialogue;

        [SerializeField] private InputActionReference swapSpots;
        [SerializeField] private InputActionReference resetObjects;

        [SerializeField] private ParticleSystem targetSpawnParticle;

        [SerializeField] private AudioClip winSound;
        [SerializeField] private AudioClip loseSound;
        [SerializeField] private AudioClip highScoreSound;

        [SerializeField] private UnityEvent cheer;
        [SerializeField] private UnityEvent degrade;

        [SerializeField] private MinigameDialogueTriggerManager dialogueTriggerManager;

        [SerializeField] private CommonUiController mainMenu;
        [SerializeField] private ScoreboardUIController scoreboard;

        private void Awake()
        {
            swapSpots.action.started += SwapSpots;
            resetObjects.action.started += ResetObjects;

            players = FindObjectsOfType<ROOTPlayer>().ToList();
        }

        private void OnDestroy()
        {
            swapSpots.action.started -= SwapSpots;
            resetObjects.action.performed -= ResetObjects;
        }

        private void Start()
        {
            // Set the high score at the start
            scoreboard.UpdateHighScore(PlayerStatistics.highScoreOddlySatisfying);

            // Get the correct reference to the active dialogue
            dialogue = dialogueTriggerManager.GetActiveDialogueTrigger().GetComponent<DialogueTrigger>();

            StartCoroutine(StartDialogue());

            int i = 0;
            foreach (ROOTPlayer player in players)
            {
                float x = i % 2 == 0 ? 0.5f : -0.5f;
                player.transform.position = new Vector3(x, 8.63999939f, 0);
                i++;
            }
        }

        // Update is called once per frame
        void Update()
        {
            players = FindObjectsOfType<ROOTPlayer>().ToList();

            if (playing)
            {
                // keep track of killed targets to remove from live targets after iteration is complete
                List<OSTarget> deadTargets = new List<OSTarget>();

                foreach (var target in liveTargets)
                {
                    // move the target
                    target.Move(transform.position, platformLength, travelSpeed);

                    // if target reaches the end of the play area then update the score and UI
                    if (Math.Abs(target.transform.position.z - transform.position.z) < 0.01f)
                    {
                        // add target to the deadTargets array to be removed later
                        deadTargets.Add(target);
                    }
                }

                // remove killed targets
                foreach (var target in deadTargets)
                {
                    liveTargets.Remove(target);
                    target.Remove();
                    if (target.GetState() == OSTarget.TargetState.Pending) UpdateScore(-10, "Missed");
                }

                // end game if lost or won
                if (count >= targetCount && liveTargets.Count == 0)
                {
                    GameOver();
                }
            }
        }

        // teleport between the two player points
        private void SwapSpots(InputAction.CallbackContext obj)
        {
            foreach (ROOTPlayer player in players)
            {
                Vector3 pos = player.transform.position;

                pos.x = pos.x < 0 ? 0.5f : -0.5f;
                player.transform.position = pos;
            }
        }
        
        // reset the objects that are not in hand to starting location
        private void ResetObjects(InputAction.CallbackContext obj)
        {
            if (!playing) return;
            foreach (OSObject osObject in FindObjectsOfType<OSObject>())
            {
                osObject.ResetPosition();
            }
        }

        // handle game over sequence
        private void GameOver()
        {
            playing = false;

            // stop spawning and destroy targets one by one with delay
            StopCoroutine(SpawnNext());

            liveTargets.Clear();

            // TODO switch to game sound effects coming from game-sound-source object instead of players
            // play win/loss sound

            if (score > PlayerStatistics.highScoreOddlySatisfying)
            {
                scoreboard.SetBody("New High Score!");
                PlayerStatistics.highScoreOddlySatisfying = score;
                scoreboard.UpdateHighScore(score);

                foreach (var player in players)
                {
                    player.PlayClip(highScoreSound);
                }
            }
            else
            {
                scoreboard.SetBody("Game Over");

                foreach (var player in players)
                {
                    player.PlayClip(score > 0 ? winSound : loseSound);
                }
            }

            if (score > 0)
            {
                PlayerStatistics.completedOddlySatisfying = true;
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

        // setup and start the game 
        public void StartGame()
        {
            // start new game if not playing
            if (!playing)
            {
                // reset targets, score, lives and counter
                liveTargets = new List<OSTarget>();
                count = 0;
                score = 0;
                playing = true;

                // start spawning coroutine loop
                StartCoroutine(SpawnNext());

                // update menu and scoreboard UI
                scoreboard.UpdateScore(0, 0);
                int timeLimit = (int)((targetCount - 1) * targetDelay + 1 / travelSpeed);
                StartCoroutine(StartTimer(timeLimit));


                // hide menu
                if (mainMenu.GetActive())
                {
                    mainMenu.Toggle();
                }
            }
        }

        // update the game score
        public void UpdateScore(int points, string bodyText)
        {
            if (points > 0)
            {
                cheer.Invoke();
            }
            else if (points < 0)
            {
                degrade.Invoke();
            }
            score += points;
            scoreboard.UpdateScore(score, points);
            scoreboard.SetBody(bodyText);
        }

        // start game after countdown
        public void Countdown()
        {
            IEnumerator StartAfterCountdown()
            {
                // update scoreboard countdown
                for (int i = 5; i > 0; i--)
                {
                    scoreboard.SetBody($"{i}");
                    yield return new WaitForSeconds(1f);
                }

                scoreboard.SetBody("GO!");
            
                // start game
                StartGame();
            }

            StartCoroutine(StartAfterCountdown());
        }

        // start dialogue
        private IEnumerator StartDialogue()
        {
            // start dialogue
            dialogue.TriggerDialogue();

            // wait for dialogue to finish
            yield return new WaitWhile(() => dialogue.dialogueManager.SentencesLeft() > 0);

            Countdown();
        }

        // handle spawning new targets
        private void AddTarget(int index)
        {
            // only spawn til count is met
            if (index >= targets.Count) return;

            // get random offset for target spawn
            // TODO replace with spawn from randomly spinning wheel
            Vector3 randV3 = (Random.insideUnitSphere +
                              new Vector3(-0.5f, -0.5f, platformLength - 0.5f)) *
                             spawnRadius;
            Vector3 spawnPoint = transform.position + randV3;

            spawnPoint.x = Random.Range(0, 2) == 0 ? -0.5f : 0.5f;
            
            Quaternion randomRot = Quaternion.Euler(0, 0, Random.value * 360);

            // spawn new target and add it to the live targets array
            OSTarget target = Instantiate(
                targets[index],
                spawnPoint,
                Quaternion.LookRotation(Vector3.forward) * randomRot);
            liveTargets.Add(target);

            // spawn particles
            Instantiate(
                targetSpawnParticle,
                spawnPoint + new Vector3(0, 0, 8),
                Quaternion.LookRotation(Vector3.up));

            // assign random color to target
            float rand = Random.value;
            Color color = rand < 0.2f ? Color.green : rand < 0.6f ? Color.yellow : Color.red;
            target.SetColor(color);

            // increase target count
            count++;
        }

        // spawn targets on interval
        IEnumerator SpawnNext()
        {
            // spawn randomly with bias towards provided shape order at first
            int nextTarget = Math.Min(count, Random.Range(0, targets.Count));
            AddTarget(nextTarget);

            // prep spawn again if still playing
            if (count >= targetCount) yield break;
            yield return new WaitForSeconds(targetDelay);
            StartCoroutine(SpawnNext());
        }

        // start game timer
        private IEnumerator StartTimer(int seconds)
        {
            while (seconds >= 0)
            {
                scoreboard.UpdateTimer(seconds);
                yield return new WaitForSeconds(1);
                seconds--;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            other.transform.position = transform.position + new Vector3(0, 0, 2);
        }
    }
}