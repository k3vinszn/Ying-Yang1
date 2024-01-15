using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField]
    private EnemyShootingLinear enemyShootingLinear;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is a player
        if (collision.CompareTag("Player"))
        {
            // Disable the assigned enemy shooting script when a player enters the bush
            if (enemyShootingLinear != null)
            {
                enemyShootingLinear.DisableShooting();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the colliding object is a player
        if (collision.CompareTag("Player"))
        {
            // Enable the assigned enemy shooting script when a player exits the bush
            if (enemyShootingLinear != null)
            {
                enemyShootingLinear.EnableShooting();
            }
        }
    }
}
