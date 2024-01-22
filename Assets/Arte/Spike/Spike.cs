using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger) // Updated tag check and added isTrigger check
        {
            other.GetComponent<Mover>().TakeDamage(); // Call the TakeDamage method of the player
        }
    }

    private void SpikeSFX()
    {
        audioManager.PlaySFX(audioManager.Spike);
    }

}
