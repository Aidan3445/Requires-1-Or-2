using System;
using General;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Oddly_Satisfying
{
    public class OSObject : MonoBehaviour
    {
        public OSType type;

        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        public void ResetPosition()
        {
            transform.position = startPosition;
        }
        
    }
}