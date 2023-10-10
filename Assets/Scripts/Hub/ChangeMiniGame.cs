using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeMiniGame : MonoBehaviour
{

    public TextMeshProUGUI Minigame;
    int sceneCount;
    string[] scenes;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        scenes = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
        }
        Minigame.text = scenes[0];
        
        print(scenes);

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void changeMinigame(bool increment)
    {
        if (increment)
        {
            index++;
            if(index >= scenes.Length)
            {
                  index = 0; 
            }
            Minigame.text = scenes[index];
        }
        else
        {
            index--;
            if(index < 0)
            {
                index = scenes.Length - 1;
            }
            Minigame.text = scenes[index];
        }
    }
}
