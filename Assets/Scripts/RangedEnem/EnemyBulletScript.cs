using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float force;
    private float timer;

    private GameObject player;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if( timer >10)
        {
            Destroy(gameObject); // destroi o projetil depois de um determinado tempo na cena

        }
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
