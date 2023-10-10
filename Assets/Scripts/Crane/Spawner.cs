using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objects;
    public GameObject[] spawnpoints;
    public CraneGameManager gameManager;
    public float timeInterval = 10;
    float currtime = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.getIsActiveGame())
        {
            currtime += Time.deltaTime;

            if (currtime >= timeInterval)
            {
                currtime = 0;
                int spawnObject = Random.Range(0, objects.Length);
                int location = Random.Range(0, spawnpoints.Length);

                Instantiate(objects[spawnObject], spawnpoints[location].transform.position, objects[spawnObject].transform.rotation);
            }
        }

        
    }
}
