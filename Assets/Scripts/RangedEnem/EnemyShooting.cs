using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private float timer;
    private bool canStartShooting = false;

    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(10f);
        // Find the player object with the "Player" tag dynamically
        player = GameObject.FindGameObjectWithTag("Player");
        canStartShooting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canStartShooting)
            return;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

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

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
