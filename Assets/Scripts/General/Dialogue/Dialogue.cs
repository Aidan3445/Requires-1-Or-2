using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public List<string> sentences;
    public bool repeatable;
    public bool hasPlayed;

    public Dialogue()
    {
        name = "";
        sentences = new List<string>();
        repeatable = false;
        hasPlayed = false;
    }
}
