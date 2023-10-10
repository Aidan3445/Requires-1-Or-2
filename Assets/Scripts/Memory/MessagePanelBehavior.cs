using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagePanelBehavior : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DisplayMessage(string text)
    {
        messageText.text = text;

        animator.SetBool("isOpen", true);
        GetComponent<BoxCollider>().enabled = true;
    }

    public void CloseMessage()
    {
        animator.SetBool("isOpen", false);
        GetComponent<BoxCollider>().enabled = false;
    }
    
}
