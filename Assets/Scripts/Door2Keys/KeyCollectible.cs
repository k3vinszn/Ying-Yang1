using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectible : MonoBehaviour
{

    AudioManager audioManager;


    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            // Assuming the key is collected when the player touches it
            CollectKey();
        }
    }

    public void CollectKey()
    {
        audioManager.PlaySFX(audioManager.KeyCollect);

        Invoke("DeactivateKey", 1f);
    }

    private void DeactivateKey()
    {
        // Deactivate the GameObject after the delay
        gameObject.SetActive(false);
    }
}
