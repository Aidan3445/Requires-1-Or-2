using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    [System.Serializable]
    public class  VRMap
    {
        public Transform vrTarget;
        public Transform rigTarget;
        public Vector3 trackingPositionOffset;
        public Vector3 trackingRotationOffset;

        public void Map()
        {
            rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
            rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
        }
    }

    public class VRRig : MonoBehaviour
    {
        public VRMap head;
        public VRMap lefthand;
        public VRMap rightHand;
        public Transform headConstraint;
        public Vector3 headBodyOffset;
        [SerializeField] private float turnSmooth;

        // Start is called before the first frame update
        void Start()
        {
            headBodyOffset = transform.position - headConstraint.position;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = headConstraint.position + headBodyOffset;
            transform.forward = Vector3.Lerp(transform.forward,Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmooth);
            head.Map();
            lefthand.Map();
            rightHand.Map();
        }
    }
}