using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace General
{
    public class HighlightObject : MonoBehaviour
    {
        [SerializeField] private Color highlightColor = Color.black;

        private MeshRenderer _renderer;
        private Color _originalColor;
        private XRBaseInteractable interactable;


        private bool isHighlighted;

        void Start()
        {
            // get a reference to the MeshRenderer
            _renderer = GetComponentInChildren<MeshRenderer>();
            // access and store the originalColor
            _originalColor = _renderer.material.color;
            // get reference to interactable
            interactable = GetComponent<XRBaseInteractable>();
        }

        void Highlight()
        {
            // set isHighlighted true
            isHighlighted = true;

            // if matching is on and interactors were found
            _renderer.material.color = highlightColor;
        }

        void Dehighlight()
        {
            // set isHighlighted false
            isHighlighted = false;
            // change the material color to originalColor
            _renderer.material.color = _originalColor;
        }

        /// TODO make compatible with 2 players!
        public void ToggleHighlight()
        {
            if (interactable.isHovered)
            {
                Highlight();
            }
            else
            {
                Dehighlight();
            }
        }

        // set the "original" color of the object
        public void SetOriginalColor(Color color)
        {
            _originalColor = color;
            if (!isHighlighted) Dehighlight();
        }

        // get the original color of the object
        public Color GetOriginalColor()
        {
            return _originalColor;
        }

        // get the highlight state from the object
        public bool GetHighlight()
        {
            return isHighlighted;
        }
    }
}