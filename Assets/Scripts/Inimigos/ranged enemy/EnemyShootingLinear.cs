using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingLinear : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject[] players;
    private int currentPlayerIndex = 0;

    public int maxShots = 3;
    public float shotInterval = 2f;
    public float cooldownTime = 5f;

    private bool canStartShooting = false;
    private bool isShootingEnabled = true; // Flag to control shooting behavior

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
                // Check if shooting is enabled before firing
                if (isShootingEnabled)
                {
                    Shoot();
                }
                yield return new WaitForSeconds(shotInterval);
            }

            yield return new WaitForSeconds(cooldownTime);

            // Switch to the next player
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }
    }

    void Shoot()
    {
        // Set the shooting direction (you can customize this vector)
        Vector2 shootingDirection = Vector2.right;

        // Instantiate the bullet in the specified direction
        Instantiate(bullet, bulletPos.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = shootingDirection;
    }

    // Expose methods to enable and disable shooting
    public void EnableShooting()
    {
        isShootingEnabled = true;
    }

    public void DisableShooting()
    {
        isShootingEnabled = false;
    }
}
