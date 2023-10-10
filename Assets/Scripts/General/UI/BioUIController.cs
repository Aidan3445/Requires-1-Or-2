using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BioUIController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text statusText;
    public float cycleDelay = 2.0f;

    int index = 0;
    List<string> bios;

    void Start()
    {
        // Set the title text
        titleText.text = "Lab Maintainers:";

        // Populate the bios list
        bios = new List<string>();

        // Aidan
        string textToAdd = "";
        textToAdd += "Name: Aidan Weinberg\n";
        textToAdd += "Role: Laboratory Developer\n";
        textToAdd += "Specialty: Computer Science & Math\n";
        textToAdd += "Favorite Kyle: Dr. Kyle\n";
        bios.Add(textToAdd);

        // Chris
        textToAdd = "";
        textToAdd += "Name: Chris Swagler\n";
        textToAdd += "Role: Laboratory Technician\n";
        textToAdd += "Specialty: Computer Engineering & Computer Science\n";
        textToAdd += "Favorite Kyle: Statistician Kyle\n";
        bios.Add(textToAdd);

        // Hector
        textToAdd = "";
        textToAdd += "Name: Hector Benitez\n";
        textToAdd += "Role: Laboratory Mechanic\n";
        textToAdd += "Specialty: Computer Science & Game Development\n";
        textToAdd += "Favorite Kyle: Odd Kyle\n";
        bios.Add(textToAdd);

        InvokeRepeating("DisplayNextBio", 0f, cycleDelay);
    }

    void DisplayNextBio()
    {
        index = (index + 1) % bios.Count;
        statusText.text = bios[index];
    }
}
