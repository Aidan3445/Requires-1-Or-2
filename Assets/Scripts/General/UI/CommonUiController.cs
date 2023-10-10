using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace General.UI
{
    public class CommonUiController : MonoBehaviour
    {
        [SerializeField] private TMP_Text topText;
        [SerializeField] private TMP_Text middleText;
        [SerializeField] private TMP_Text leftButtonText;
        [SerializeField] private TMP_Text rightButtonText;
        
        [SerializeField] private InputActionReference toggleButton;

        [SerializeField] private bool startActive;
        
        private bool isActive = true;

        private bool toggleable = true;
        
        private void Awake()
        {
            toggleButton.action.started += ToggleOnPress;
            if (!startActive)
            {
                Toggle();
            }
        }

        private void OnDestroy()
        {
            toggleButton.action.started -= ToggleOnPress;
        }

        public void SetTopText(string text)
        {
            if (!topText) return;
            topText.text = text;
        }
        
        public void SetMidText(string text)
        {
            if (!middleText) return;
            middleText.text = text;
        }
        
        public void SetLeftBText(string text)
        {
            if (!leftButtonText) return;
            leftButtonText.text = text;
        }
        
        public void SetRightBText(string text)
        {
            if (!rightButtonText) return;
            rightButtonText.text = text;
        }

        private void ToggleOnPress(InputAction.CallbackContext callbackContext)
        {
            Toggle();
        }

        public void Toggle()
        {
            if (!toggleable) return;
            isActive = !isActive;
            gameObject.SetActive(isActive);
        }

        public bool GetActive()
        {
            return isActive;
        }

        public void ToggleToggleability()
        {
            toggleable = !toggleable;
        }
    }
}