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
            Debug.Log("Shield TAKE HIT ");
            Destroy(other.gameObject);
            Debug.Log("Bullet destroyed by PlayerShield");
        }
    }

    
}
