using System;
using System.Diagnostics;
using UnityEngine;

namespace Oddly_Satisfying
{
    [Serializable]
    public enum OSType
    {
        Circle,
        Square,
        Triangle,
        Star,
        Heart
    }
    
    public class OSTarget : MonoBehaviour
    {
        public OSType type;

        private Color targetColor;

        [SerializeField] private float bufferDist;
        [SerializeField] private float bufferAngle;

        [SerializeField] private int rotationalSym;

        private Renderer _renderer;

        private TargetState state;
        
        private float _targetProgress;


        public enum TargetState
        {
            Pending,
            Fail,
            Success
        }

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        public void SetColor(Color color)
        {
            _renderer.material.color = color;
            targetColor = color;
        }

        public TargetState GetState()
        {
            return state;
        }

        private void OnTriggerStay(Collider other)
        {
            if (state == TargetState.Pending && other.TryGetComponent(out OSObject osObject))
            {
                // convert 3D positions to 2D in XY plane
                Vector2 tgtPos = XYPlane(transform.position);
                Vector2 objPos = XYPlane(osObject.transform.position);

                // calc dist and angle difference
                float dist = Vector2.Distance(tgtPos, objPos);
                
                float symAngle = 360f / rotationalSym;
                float angle = Math.Abs(transform.rotation.eulerAngles.z - osObject.transform.rotation.eulerAngles.z) 
                              % symAngle;
                angle = Math.Min(angle, symAngle - angle);
                
                // fails if types dont match or position/angle is misaligned
                if (osObject.type != type || dist > bufferDist || angle > bufferAngle)
                {
                    state = TargetState.Fail;
                    
                    // update all scoreboard instances with score messages
                    string bodyText;
                    if (osObject.type != type)
                    {
                        bodyText = "Wrong Shape";
                        FindObjectOfType<OSGameManager>().UpdateScore(-100, bodyText);
                    } else if (dist > bufferDist)
                    {
                        bodyText = "Bad Alignment";
                        FindObjectOfType<OSGameManager>().UpdateScore(-50, bodyText);
                    }
                    else
                    {
                        bodyText = "Bad Rotation";
                        FindObjectOfType<OSGameManager>().UpdateScore(-25, bodyText);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (state != TargetState.Pending || !other.TryGetComponent(out OSObject osObject) ||
                osObject.type != type) return;
            state = TargetState.Success;

            // 50 points for correct player * color multiplier: 1x red, 2x yellow, 3x green
            int multiplier = targetColor == Color.red ? 1 : targetColor == Color.yellow ? 2 : 3;
            FindObjectOfType<OSGameManager>().UpdateScore(100 * multiplier, "Perfect!");
            
        }

        // flatten Vector3 into XY plane
        private Vector2 XYPlane(Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }

        // move the target towards the end of the platform
        public void Move(Vector3 gamePosition, float platformLength, float travelSpeed)
        {
            float interp = _targetProgress / platformLength;
            Vector3 position = transform.position;
            Vector3 finalPosition = new Vector3(position.x, position.y, gamePosition.z);
            Vector3 startPosition = finalPosition;
            startPosition.z += platformLength;
            transform.position = Vector3.Lerp(startPosition, finalPosition, interp);
            _targetProgress += platformLength * Time.deltaTime * travelSpeed;
        }
    }
}