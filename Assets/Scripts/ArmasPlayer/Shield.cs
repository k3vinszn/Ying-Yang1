using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int maxHits = 3;
    public int currentHits; // Made it public so that Mover script can access it

    private Mover mover; // Reference to the Mover script

    private void Start()
    {
        currentHits = 0;
        mover = transform.parent.GetComponent<Mover>(); // Assuming the Shield is a child of the Mover
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
            ResetHits();
        }
    }

    private void DisableShield()
    {
        gameObject.SetActive(false);
        mover.DisableFire(); // Call the DisableFire method in the Mover script
    }

    public void ResetHits()
    {
        currentHits = 0;
    }
}
