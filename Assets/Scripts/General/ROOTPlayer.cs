using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace General
{
    public class ROOTPlayer : MonoBehaviour
    {
        private AudioSource audioSource;

        [SerializeField] private Transform spawnPoint;
        private Camera _camera;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            _camera = GetComponentInChildren<Camera>();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += ResetPositionOnLoad;
        }
 
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= ResetPositionOnLoad;
        }

        public void PlayClip(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        public void BackToHub()
        {
            SceneManager.LoadScene("hub");
        }

        void ResetPositionOnLoad(Scene arg0, LoadSceneMode loadSceneMode)
        {
            StartCoroutine(ResetPosition());
        }

        private IEnumerator ResetPosition()
        {
            yield return new WaitForEndOfFrame();
            
            var rotationAngleY = spawnPoint.rotation.eulerAngles.y - _camera.transform.rotation.eulerAngles.y;
            transform.Rotate(0, rotationAngleY, 0);
        }
    }
}