using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadMinigame : MonoBehaviour
{

    public TextMeshProUGUI Minigame;
 
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(Minigame.text);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            Debug.Log("collide");
            LoadScene(Minigame.text);
        }
    }
}
