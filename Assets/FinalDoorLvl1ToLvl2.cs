using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorLvl1ToLvl2 : MonoBehaviour
{
    public string targetSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the collider
        if (other.CompareTag("Player"))
        {
            // Load the target scene
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
