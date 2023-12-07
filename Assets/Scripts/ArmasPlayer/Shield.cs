using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int maxHits = 3;
    public int currentHits; // Made it public so that Mover script can access it

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
    }
}