using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbFloatBehavior : MonoBehaviour
{
    public float randomRotationStrength = 1.0f;

    Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        // TODO: this should probably only happen when not grabbed, so add check for if grabbed
        if (other.CompareTag("Orb"))
        {
            // When colliding with the orb, a rotation should be applied and
            // this axe should be kinematic
            //SetKinematic(true);
            _rigidbody.isKinematic = true;
            transform.Rotate(randomRotationStrength, randomRotationStrength, randomRotationStrength);
            // TODO: have the axe lerp to the center spawn position?
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            // When exiting collision with the orb, set kinematic back to false
            //SetKinematic(false);
            _rigidbody.isKinematic = false;
        }
    }

    public void SetKinematic(bool state)
    {
        _rigidbody.isKinematic = state;
    }
}
