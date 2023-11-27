using UnityEngine;

public class Shield : MonoBehaviour
{
    public int maxBlockCount = 10; // Maximum number of times the shield can block bullets
    private int blockCount; // Current count of blocked bullets

    private void Start()
    {
        blockCount = 0;
    }

    // OnTriggerEnter2D is called when another collider enters the trigger collider attached to this GameObject
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Check if the shield can still block bullets
            if (blockCount < maxBlockCount)
            {
                // Block the bullet
                blockCount++;
                Debug.Log("Bullet Blocked! Remaining Blocks: " + (maxBlockCount - blockCount));
            }
            else
            {
                // Shield is broken, destroy it
                Destroy(gameObject);
                Debug.Log("Shield Broken!");
            }

            // Destroy the bullet
            Destroy(other.gameObject);
        }
    }
}
