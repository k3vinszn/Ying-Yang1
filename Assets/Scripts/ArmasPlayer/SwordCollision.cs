using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    // Set the tag for the enemy objects in the Unity editor
    public string enemyTag = "Enemy";

    private AudioManager audioManager; // Reference to the AudioManager script

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Get the AudioManager component
    }

    private void OnEnable()
    {
        // Play the "Sword" sound effect when the GameObject is enabled
        audioManager.PlaySFX(audioManager.Sword);
    }

    // This method is called when the sword collides with another collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the specified enemy tag
        if (other.CompareTag(enemyTag))
        {
            // Handle enemy collision as needed
        }
    }
}
