using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject[] players;

    private GameObject player;
    private float playerDistance;
    public int maxShots = 3;
    public float shotInterval = 2f;
    public float cooldownTime = 5f;

    private bool canStartShooting = false;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(5f);
        players = GameObject.FindGameObjectsWithTag("Player");
        canStartShooting = true;

        StartCoroutine(ShootLoop());
    }

    IEnumerator ShootLoop()
    {
        while (canStartShooting)
        {
            for (int shotsFired = 0; shotsFired < maxShots; shotsFired++)
            {
                Shoot();
                yield return new WaitForSeconds(shotInterval);
            }

            yield return new WaitForSeconds(cooldownTime);
        }
    }

    void Shoot()
    {
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
        if (playerDistance < 20)
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
    }
}