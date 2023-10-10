using System;
using System.Collections;
using General.Dialogue;
using General.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace General
{
    public class DoomRoomController : MonoBehaviour
    {
        [SerializeField] private GameObject entrance;
        [SerializeField] private GameObject roomClosed;
        
        [SerializeField] private GameObject falseFloor;
        [SerializeField] private GameObject fireParticles;
        [SerializeField] private GameObject fireFloor;

        [SerializeField] private AudioSource backgroundAudio;
        [SerializeField] private AudioClip doomSong;
        
        [SerializeField] private DoorController doorController;

        [SerializeField] private TeleportationArea[] hallwayTeleportationAreas;

        private QuestionnaireManager questionnaire;

        private static bool _opened;

        private void Awake()
        {
            questionnaire = GetComponent<QuestionnaireManager>();
        }

        private void Start()
        {
            roomClosed.SetActive(_opened);

            foreach (TeleportationArea area in hallwayTeleportationAreas)
            {
                area.enabled = false;
            }

            // If all games are completed and the doom room is unvisited, open
            int totalGames = PlayerStatistics.TotalGames();
            int completedGames = PlayerStatistics.CompletedGameCount();

            if (completedGames == totalGames && !PlayerStatistics.visitedDoomRoom)
            {
                OpenDoomRoom();
            }
        }

        public void OpenDoomRoom()
        {
            if (_opened) return;

            StartCoroutine(OpenDoor());
            _opened = true;
            
            foreach (TeleportationArea area in hallwayTeleportationAreas)
            {
                area.enabled = true;
            }
            
            StartCoroutine(WaitEvents());
        }

        IEnumerator OpenDoor()
        {
            float doorProgress = 0;

            Quaternion rotation = entrance.transform.rotation;
            Quaternion start = rotation;
            Quaternion end = rotation * Quaternion.Euler(0, 90, 0);
            while (doorProgress < 1)
            {
                entrance.transform.rotation = Quaternion.Lerp(start, end, doorProgress);
                doorProgress += Time.deltaTime / 2;
                yield return new WaitForSeconds(Time.deltaTime);

            }
        }

        IEnumerator WaitEvents()
        {
            yield return new WaitUntil(() => questionnaire.RevealFire());
            RevealFire();
            yield return new WaitUntil(() => questionnaire.SentencesLeft() == 0);
            PlayerStatistics.visitedDoomRoom = true;
            SceneManager.LoadScene("hub");
        }

        private void RevealFire()
        {
            backgroundAudio.clip = doomSong;
            backgroundAudio.Play();
            
            Instantiate(fireParticles,
                fireFloor.transform.position + new Vector3(0, 0.2f, 0), 
                Quaternion.LookRotation(Vector3.forward));

            IEnumerator Slide()
            {
                yield return new WaitForSeconds(3);
                
                float slideProgress = 0;
                Vector3 position = falseFloor.transform.position;
                Vector3 start = position;
                Vector3 stop = position + new Vector3(0, 0, 5.45f);

                while (slideProgress < 1)
                {
                    falseFloor.transform.position = Vector3.Lerp(start, stop, slideProgress);
                    slideProgress += Time.deltaTime / 3;
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            }

            StartCoroutine(Slide());
        }

        private void OnTriggerEnter(Collider other)
        {
            // When the doom room is triggered, deactivate the door
            doorController.SetEnable(false);
            GetComponent<QuestionnaireTrigger>().TriggerDialogue();
        }
    }
}