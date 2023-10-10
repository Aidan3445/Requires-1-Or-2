using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace General.UI
{
    public class DebugUiController : MonoBehaviour
    {
        [SerializeField] private TMP_Text debugText;
        
        [SerializeField] private InputActionReference toggleUIReference;

        [SerializeField] private bool startActive;

        private bool isActive = true;

        private void Awake()
        {
            toggleUIReference.action.started += ToggleOnPress;
            if (!startActive)
            {
                Toggle();
            }
        }

        private void OnDestroy()
        {
            toggleUIReference.action.started -= ToggleOnPress;
        }

        private void DebugLn(String text, bool withTime = false)
        {
            string prefix = withTime ? $"{Time.realtimeSinceStartup}: " : "";
            string debugMessage = $"\n{prefix}{text}";
            debugText.text += debugMessage;
            print(debugMessage);
        }

        public void DebugLnWithTime(String text)
        {
            DebugLn(text, true);
        }
        
        public void DebugLnNoTime(String text)
        {
            DebugLn(text);
        }

        private void Debug(String text, bool withTime = false)
        {
            string prefix = withTime ? $"{Time.realtimeSinceStartup}: " : "";
            string debugMessage= $"\n{prefix}{text}";
            debugText.text = debugMessage;
            print(debugMessage);
        }
        
        public void DebugWithTime(String text)
        {
            Debug(text, true);
        }
        
        public void DebugNoTime(String text)
        {
            Debug(text);
        }

        public void ClearDebug()
        {
            debugText.text = "";
        }

        private void ToggleOnPress(InputAction.CallbackContext callbackContext)
        {
            Toggle();
        }

        public void Toggle()
        {
            isActive = !isActive;
            gameObject.SetActive(isActive);

            foreach (var button in GetComponentsInChildren<HighlightObject>())
            {
                if (button.GetHighlight())
                {
                    button.ToggleHighlight();
                }
            }
        }
    }
}