using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlsUIController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text statusText;
    public float cycleDelay = 2.0f;

    int index = 0;
    List<string> controls;

    void Start()
    {
        // Set the title text
        titleText.text = "Controls:";

        // Populate the controls list
        controls = new List<string>();

        // Skip Dialogue
        string textToAdd = "";
        textToAdd += "Location: Anywhere\n";
        textToAdd += "Action: Skip Dialogue\n";
        textToAdd += "Press the A button (right controller) or X button (left controller).\n";
        controls.Add(textToAdd);

        // Hub Navigation
        textToAdd = "";
        textToAdd += "Location: Hub\n";
        textToAdd += "Action: Move Around / Teleport\n";
        textToAdd += "Point with the left controller and press trigger.";
        controls.Add(textToAdd);

        // Scene start
        textToAdd = "";
        textToAdd += "Location: Hub\n";
        textToAdd += "Action: Begin Test\n";
        textToAdd += "Point at one of the tests with the left controller and press trigger.\n";
        controls.Add(textToAdd);

        // Scene return to hub
        textToAdd = "";
        textToAdd += "Location: Any Test\n";
        textToAdd += "Action: Return to Hub\n";
        textToAdd += "Press the left joystick and use the right controller trigger to select the return button.\n";
        controls.Add(textToAdd);

        // Oddly Satisfying Switch Spot
        textToAdd = "";
        textToAdd += "Location: Oddly Satisfying\n";
        textToAdd += "Action: Switch Spots\n";
        textToAdd += "Press the left or right trigger.\n";
        controls.Add(textToAdd);

        // Grabbing objects
        textToAdd = "";
        textToAdd += "Location: Anywhere\n";
        textToAdd += "Action: Grab Object\n";
        textToAdd += "Hover over an object and press the grip button.\n";
        controls.Add(textToAdd);

        // Crane
        textToAdd = "";
        textToAdd += "Location: Crane Game\n";
        textToAdd += "Action: Movement\n";
        textToAdd += "Use the left joystick to move laterally and the right joystick to move vertically.\n";
        controls.Add(textToAdd);

        InvokeRepeating("DisplayNextControl", 0f, cycleDelay);
    }

    void DisplayNextControl()
    {
        index = (index + 1) % controls.Count;
        statusText.text = controls[index];
    }
}
