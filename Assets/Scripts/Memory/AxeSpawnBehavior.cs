using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxeSpawnBehavior : MonoBehaviour
{
    public GameObject axePrefab;
    public Transform spawnLocation;
    public TextMeshProUGUI debugText;
    public AudioClip axeSpawnSFX;

    private HashSet<GameObject> axes = new HashSet<GameObject>();

    private void FixedUpdate()
    {
        // If there are no axes in the spawner, we should spawn one
        if (axes.Count == 0)
        {
            SpawnAxe();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Add the axe gameobject to the hashset - hashset handles duplicates
        if (other.CompareTag("Axe"))
        {
            // The "Axe"-tagged collider is the axe gameobject itself
            axes.Add(other.gameObject);
        }
        else if (other.CompareTag("AxeTip"))
        {
            // The "AxeTip"-tagged collider is a 1 level child of the axe
            // This is kinda gross to get the parent gameobject but it'll do
            axes.Add(other.gameObject.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // On exiting the trigger, remove the axe gameobject from the hashset
        if (other.CompareTag("Axe"))
        {
            // The "Axe"-tagged collider is the axe gameobject itself
            axes.Remove(other.gameObject);
        }
        else if (other.CompareTag("AxeTip"))
        {
            // The "AxeTip"-tagged collider is a 1 level child of the axe
            axes.Remove(other.gameObject.transform.parent.gameObject);
        }
    }

    public void SpawnAxe()
    {
        // Instantiate the axe prefab at the spawn location
        Instantiate(axePrefab, spawnLocation.position, spawnLocation.rotation);

        // Play the spawn audio clip
        AudioSource.PlayClipAtPoint(axeSpawnSFX, spawnLocation.position);
    }

    private void Debug(string message)
    {
        if (debugText)
        {
            debugText.text = message;
        }
    }
}
