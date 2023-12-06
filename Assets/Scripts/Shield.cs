using UnityEngine;

public class Shield : MonoBehaviour
{
    public int maxHits = 3;
    private int currentHits;

    private void Start()
    {
        currentHits = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            HandleBulletHit();
        }
    }

    private void HandleBulletHit()
    {
        currentHits++;

        if (currentHits >= maxHits)
        {
            DisableShield();
        }
    }

    private void DisableShield()
    {
        gameObject.SetActive(false);
        Invoke("EnableShield", 3f); // Enable the shield after 3 seconds
    }

    private void EnableShield()
    {
        gameObject.SetActive(true);
        currentHits = 0; // Reset hit count
    }
}
