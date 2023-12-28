using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject[] players;
    private int currentPlayerIndex = 0;

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
        yield return new WaitForSeconds(10f);
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

            // Switch to the next player
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }
    }

    void Shoot()
    {
        float distance = Vector2.Distance(transform.position, players[currentPlayerIndex].transform.position);

        if (distance < 20)
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
    }
}
