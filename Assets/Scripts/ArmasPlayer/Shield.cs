using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int maxHits = 3;
    public int currentHits; // Made it public so that Mover script can access it

    private AudioManager audioManager; // Reference to the AudioManager script

    private void Start()
    {
        currentHits = 0;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Get the AudioManager component
    }

    private void OnEnable()
    {
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.ShieldTrigger);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))

        {
            audioManager.PlaySFX(audioManager.ShieldHit);

            Debug.Log("Shield TAKE HIT ");
            Destroy(other.gameObject);
            Debug.Log("Bullet destroyed by PlayerShield");
        }
    }
}