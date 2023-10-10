using UnityEngine;

namespace General
{
    public class DoorController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int CharacterNearby = Animator.StringToHash("character_nearby");
        private bool _enabled = true;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_enabled)
            {
                _animator.SetBool(CharacterNearby, true);
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (_enabled)
            {
                _animator.SetBool(CharacterNearby, false);
            }
        }

        public void SetEnable(bool enable)
        {
            _enabled = enable;
        }
    }
}
