using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerCraneMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject xrorigin;
    public InputActionReference _xaxisReference;
    public InputActionReference _zaxisReference;
    public InputActionReference _yaxisReference;
    public float speed = 10f;
    void Start()
    {
      //  _xaxisReference.action.started += MoveX;
      //  _yaxisReference.action.started += MoveY;
      //  _zaxisReference.action.started += MoveZ;

    }

    // Update is called once per frame
    void Update()
    {

        
        Vector2 newMovevaluex = _xaxisReference.action.ReadValue<Vector2>();
        if(newMovevaluex.x != 0)
        {
            Debug.Log(newMovevaluex);
            Debug.Log(xrorigin.transform.position);
            Vector3 xaxismovement = new Vector3((xrorigin.transform.position.x + (newMovevaluex.x * 25)), xrorigin.transform.position.y, xrorigin.transform.position.z);

            //Vector3 xaxismovement = new Vector3((xrorigin.transform.position.x + newMovevaluex.x), xrorigin.transform.position.y, xrorigin.transform.position.z);
             xrorigin.transform.position = Vector3.Lerp(xrorigin.transform.position, xaxismovement, speed * Time.deltaTime);
            //xrorigin.transform.position = xaxismovement;
        }

        Vector2 newMovevaluey = _yaxisReference.action.ReadValue<Vector2>();

        if (newMovevaluey.y != 0)
        {
            Debug.Log(newMovevaluey);
            Debug.Log("move up");
            Vector3 yaxismovement = new Vector3(xrorigin.transform.position.x, (xrorigin.transform.position.y + (newMovevaluey.y * 25)), xrorigin.transform.position.z);

            xrorigin.transform.position = Vector3.Lerp(xrorigin.transform.position, yaxismovement, speed * Time.deltaTime);
        }

        Vector2 newMovevaluez = _zaxisReference.action.ReadValue<Vector2>();

        if (newMovevaluez.y != 0)
        {
            Debug.Log(newMovevaluez);
            Debug.Log("move forward");
            Vector3 zaxismovement = new Vector3(xrorigin.transform.position.x, xrorigin.transform.position.y, (xrorigin.transform.position.z + (newMovevaluez.y * 25)));

            xrorigin.transform.position = Vector3.Lerp(xrorigin.transform.position, zaxismovement, speed * Time.deltaTime);
        } 

    }
    
    /*
    public void MoveX(InputAction.CallbackContext context)
    {
        Vector2 newMovevalue = _xaxisReference.action.ReadValue<Vector2>();
        Debug.Log(newMovevalue);
        Vector3 xaxismovement = new Vector3(xrorigin.transform.position.x + newMovevalue.x, 0, 0);

        xrorigin.transform.position = Vector3.Lerp(xrorigin.transform.position, xaxismovement, speed * Time.deltaTime);

    }

    public void MoveY(InputAction.CallbackContext context)
    {
        Vector2 newMovevalue = _yaxisReference.action.ReadValue<Vector2>();
        Debug.Log(newMovevalue);
        Vector3 xaxismovement = new Vector3(0, xrorigin.transform.position.y + newMovevalue.y, 0);

        xrorigin.transform.position = Vector3.Lerp(xrorigin.transform.position, xaxismovement, speed * Time.deltaTime);

    }

    public void MoveZ(InputAction.CallbackContext context)
    {
        Vector2 newMovevalue = _zaxisReference.action.ReadValue<Vector2>();
        Debug.Log(newMovevalue);
        Vector3 xaxismovement = new Vector3(0, 0, xrorigin.transform.position.z + newMovevalue.y);

        xrorigin.transform.position = Vector3.Lerp(xrorigin.transform.position, xaxismovement, speed * Time.deltaTime);

    }
    */
    
}
