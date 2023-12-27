using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private float timer;
    private bool canStartShooting = false;
    private int shotsFired = 0; // Track the number of shots fired
    public int maxShots = 3; // Maximum number of shots before cooldown
    public float cooldownTime = 5f; // Cooldown time in seconds

    public GameObject bullet;
    public Transform bulletPos;
    private GameObject[] players; // Array of players

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(10f);
        players = GameObject.FindGameObjectsWithTag("Player"); // Find all game objects with the tag
        canStartShooting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canStartShooting)
            return;

        foreach (GameObject player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < 20)
            {
                timer += Time.deltaTime;

                if (timer > 2)
                {
                    timer = 0;
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        shotsFired++;

        if (shotsFired >= maxShots)
        {
            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator StartCooldown()
    {
        canStartShooting = false; // Disable shooting during cooldown
        yield return new WaitForSeconds(cooldownTime);
        shotsFired = 0; // Reset the shot counter after the cooldown
        canStartShooting = true; // Enable shooting after cooldown
    }
}
