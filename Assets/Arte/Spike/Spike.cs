using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger) // Updated tag check and added isTrigger check
        {
            other.GetComponent<Mover>().TakeDamage(); // Call the TakeDamage method of the player
        }
    }
}
