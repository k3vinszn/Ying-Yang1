using UnityEngine;
using System.Collections;

public class FrogScript : MonoBehaviour
{
    public float detectionRange = 5f;
    public LayerMask playerLayer;

    private Transform player;
    private bool isPlayerDetected = false;

    void Update()
    {
        DetectPlayer();

        if (isPlayerDetected)
        {
            // Perform actions when the player is detected
            // For example, you can call a method to attack the player
            AttackPlayer();
        }
    }

    void DetectPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, playerLayer);

        // Check if any of the colliders belong to the player
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // Player detected
                isPlayerDetected = true;
                player = collider.transform;
                return;
            }
        }

        // Player not detected
        isPlayerDetected = false;
        player = null;
    }

    void AttackPlayer()
    {
        // Implement your logic to attack the player here
        // For example, you can reduce the player's health or perform any other action
        Debug.Log("Player in range! Attack!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger) // Updated tag check and added isTrigger check
        {
            other.GetComponent<Mover>().TakeDamage(); // Call the TakeDamage method of the player
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
