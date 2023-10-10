using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class MatchObjects : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI valueText;
    public string tagName;
    int score = 0;
    public string scenename;
    public CraneGameManager gameManager;
    void Start()
    {
        valueText.text = tagName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tagName)
        {
            if (other.gameObject.GetComponent<MeshRenderer>() != null)
            {
                //player put object in correct spot so highlight object green, add to score and delete object
                other.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                Destroy(other.gameObject, .5f);
                //score++;
                gameManager.Success();

            }
            else
            {
                MeshRenderer[] children = other.gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach(MeshRenderer child in children)
                {
                    child.material.color = Color.green;
                }
                Destroy(other.gameObject, .5f);
                //score++;
                gameManager.Success();

            }
        }

        else if (other.gameObject.tag != "XROrigin" && other.gameObject.tag != "GameController")
        {
            if (other.gameObject.GetComponent<MeshRenderer>() != null)
            {
                //player guessed wrong highlight object red, subtract points and delete object
                other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Destroy(other.gameObject, 1.5f);
                gameManager.Penalize();

            }
            else
            {
                MeshRenderer[] children = other.gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer child in children)
                {
                    child.material.color = Color.red;
                }
                Destroy(other.gameObject, .5f);
                gameManager.Penalize();

            }

        }
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == tagName)
        {
            if (other.gameObject.GetComponent<MeshRenderer>() != null)
            {
                //player put object in correct spot so highlight object green, add to score and delete object
                other.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                Destroy(other.gameObject, 1.5f);
                gameManager.Success();
                Debug.Log("correct");

            }
            else
            {
                MeshRenderer[] children = other.gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer child in children)
                {
                    child.material.color = Color.green;
                }
                Destroy(other.gameObject, .5f);
                gameManager.Success();

            }

        }

        else if(other.gameObject.tag != "XROrigin" && other.gameObject.tag != "GameController")
        {
            if (other.gameObject.GetComponent<MeshRenderer>() != null)
            {
                //player guessed wrong highlight object red, subtract points and delete object
                other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Destroy(other.gameObject, 1.5f);
                gameManager.Penalize();
           
            }
            else
            {
                MeshRenderer[] children = other.gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer child in children)
                {
                    child.material.color = Color.red;
                }
                Destroy(other.gameObject, .5f);
                gameManager.Penalize();

            }

        }

    }
}
