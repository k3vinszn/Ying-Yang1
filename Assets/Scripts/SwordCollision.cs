using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    // Set the tag for the enemy objects in the Unity editor
    public string enemyTag = "Enemy";

    // This method is called when the sword collides with another collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the specified enemy tag
        if (other.CompareTag(enemyTag))
        {
            // Destroy the enemy game object
            Destroy(other.gameObject);
        }
    }
}
