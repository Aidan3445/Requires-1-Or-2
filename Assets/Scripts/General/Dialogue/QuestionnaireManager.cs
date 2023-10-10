using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace General.Dialogue
{
    public class QuestionnaireManager : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;
        public Animator dialogueAnimator;
        public InputActionReference continueButton;
        public RobotBehavior robot;

        public float minViewAngle = 45;

        protected Queue<string> sentences = new();

        protected void Awake()
        {
            continueButton.action.started += ContinueButtonEvent;
        }

        protected void OnDestroy()
        {
            continueButton.action.started -= ContinueButtonEvent;
        }

        public void StartDialogue(global::Dialogue dialogue)
        {
            if (dialogue.repeatable || !dialogue.hasPlayed)
            {
                dialogueAnimator.SetBool("isOpen", true);

                nameText.text = dialogue.name;

                sentences.Clear();

                foreach (string sentence in dialogue.sentences)
                {
                    sentences.Enqueue(sentence);
                }

                DisplayNextSentence();
            }
        }

        public void DisplayNextSentence()
        {
            if (currentQuestion is not null) return;
            
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();

            if (sentence.Length > 3 && sentence.Substring(0, 2) == "**")
            {
                currentQuestion = Instantiate(questions[int.Parse(sentence.Substring(2, 1))], transform);
                currentQuestion.SetManager(this);
                sentence = sentence.Substring(3);
            }
            
            StopAllCoroutines();

            if (robot)
            {
                robot.Talk();
                StartCoroutine(TypeSentenceRobot(sentence));
            }
            else
            {
                StartCoroutine(TypeSentence(sentence));
            }
        }

        protected IEnumerator TypeSentenceRobot(string sentence)
        {
            robot.PlayAudio();
            yield return StartCoroutine(TypeSentence(sentence));
            robot.StopAudio();
        }

        protected IEnumerator TypeSentence(string sentence)
        {
            dialogueText.text = "";

            foreach (char letter in sentence)
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        protected void EndDialogue()
        {
            dialogueAnimator.SetBool("isOpen", false);

            if (robot)
            {
                robot.Idle();
            }
        }

        protected void ContinueButtonEvent(InputAction.CallbackContext callbackContext)
        {
            Camera cam = Camera.main;
            if (cam is null) return;
            float angle = Math.Abs(Vector3.Angle(cam.transform.forward, nameText.transform.forward));
            if (angle <= minViewAngle) DisplayNextSentence();
        }

        public int SentencesLeft()
        {
            return sentences.Count;
        }
        [SerializeField] private List<QuestionnaireQuestion> questions;
        private QuestionnaireQuestion currentQuestion;

        public void QuestionAnswered()
        {
            currentQuestion = null;
            DisplayNextSentence();
        }

        public bool RevealFire()
        {
            return sentences.Count == 9;
        }
    }
}