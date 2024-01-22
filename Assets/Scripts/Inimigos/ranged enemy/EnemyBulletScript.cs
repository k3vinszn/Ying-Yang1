using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float force;
    private float timer;
    private GameObject player;
    private GameObject[] players;
    private Rigidbody2D rb;
    private float playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        players = GameObject.FindGameObjectsWithTag("Player");

        float player1Distance = Vector3.Distance(transform.position, players[0].transform.position);
        float player2Distance = Vector3.Distance(transform.position, players[1].transform.position);

        if (player1Distance <= player2Distance)
        {
            player = players[0];
            playerDistance = player1Distance;
        }
        else
        {
            player = players[1];
            playerDistance = player2Distance;
        }

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);


    }

    // Update is called once per frame
    void Update()
    {


        timer += Time.deltaTime;

        if (timer > 3)
        {
            Destroy(gameObject); // destroy the projectile after a certain time in the scene
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            other.GetComponent<Mover>().TakeDamage(); // Call the TakeDamage method of the player
            Destroy(gameObject); // destroy the bullet
        }
    }
}
