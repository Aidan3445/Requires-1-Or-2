using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class RobotBehavior : MonoBehaviour
    {
        public Animator animator;

        [SerializeField] private float reactRate = 0.1f;

        [SerializeField] private List<AudioClip> talkAudioClips;
        [SerializeField] private List<AudioClip> cheerAudioClips;
        [SerializeField] private List<AudioClip> degradeAudioClips;

        // This verbal field is only for the cheering/degrading audio.
        // It doesn't affect the talking clips
        [SerializeField] private bool verbal = true;

        private AudioSource audioSource;
        private static readonly int NpcAnimState = Animator.StringToHash("npcAnimState");
        // The NPC idle states are: 0, 4, 5
        private List<int> npcIdleStates = new List<int> { 0, 4, 5 };
        // The NPC talk states are: 1
        private List<int> npcTalkStates = new List<int> { 1 };
        // The NPC cheer states are: 2, 6, 7
        private List<int> npcCheerStates = new List<int> { 2, 6, 7 };
        // The NPC degrade states are: 3, 8, 9, 10
        private List<int> npcDegradeStates = new List<int> { 3, 8, 9, 10 };

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Idle()
        {
            int randomIdleState = npcIdleStates[Random.Range(0, npcIdleStates.Count)];
            animator.SetInteger(NpcAnimState, randomIdleState);
        }

        public void Talk()
        {
            int randomTalkState = npcTalkStates[Random.Range(0, npcTalkStates.Count)];
            animator.SetInteger(NpcAnimState, randomTalkState);
            audioSource.clip = talkAudioClips[Random.Range(0, talkAudioClips.Count)];
        }

        public void Cheer()
        {
            if (Random.value <= reactRate)
            {
                int randomCheerState = npcCheerStates[Random.Range(0, npcCheerStates.Count)];
                animator.SetInteger(NpcAnimState, randomCheerState);
                if (verbal)
                {
                    audioSource.PlayOneShot(cheerAudioClips[Random.Range(0, cheerAudioClips.Count)]);
                }
                StartCoroutine(IdleAfterAudio());
            }
        }

        public void Degrade()
        {
            if (Random.value <= reactRate)
            {
                int randomDegradeState = npcDegradeStates[Random.Range(0, npcDegradeStates.Count)];
                animator.SetInteger(NpcAnimState, randomDegradeState);
                if (verbal)
                {
                    audioSource.PlayOneShot(degradeAudioClips[Random.Range(0, degradeAudioClips.Count)]);
                }
                StartCoroutine(IdleAfterAudio());
            }
        }

        public void PlayAudio()
        {
            audioSource.Play();
        }

        public void StopAudio()
        {
            audioSource.Stop();
        }

        private IEnumerator IdleAfterAudio()
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            Idle();
        }
    }
}