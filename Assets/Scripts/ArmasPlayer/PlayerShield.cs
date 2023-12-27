using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    // This method is called when a Collider2D enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider has the tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // Destroy the bullet GameObject
            Destroy(other.gameObject);

            // You can add additional effects or logic here if needed

            Debug.Log("Bullet destroyed by PlayerShield");
        }
    }
}
