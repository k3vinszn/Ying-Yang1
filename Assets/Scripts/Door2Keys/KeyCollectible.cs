using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Assuming the key is collected when the player touches it
            CollectKey();
        }
    }

    public void CollectKey()
    {
        // You can add any specific logic here when the key is collected,
        // such as playing a sound, disabling the key object, etc.

        // For example, you can deactivate the GameObject when the key is collected:
        gameObject.SetActive(false);
    }
}
