using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private bool isAwake = false;
    public float Range = 4.2f;
    private Animator animator;
    private GameObject[] players;
    private GameObject player;
    private float playerDistance;
    public float triggerRange;
    AudioManager audioManager;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandlePlayerDistance();
        CalculateDirection();
    }

    private void HandlePlayerDistance()
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

        if (playerDistance <= Range)
        {
            if (!isAwake)
            {
                animator.SetBool("activated", true);
                animator.SetBool("slapattack", true);
                isAwake = true;
            }
            else
            {
                animator.SetBool("slapattack", true);
            }
        }
        else
        {
            if (playerDistance <= triggerRange)
            {
                animator.SetBool("activated", true);
                animator.SetBool("slapattack", false);
                animator.SetBool("iddle", false);
                isAwake = false;
            }
            else
            {
                animator.SetBool("slapattack", false);
                animator.SetBool("activated", false);
                animator.SetBool("iddle", true);
                isAwake = false;
            }
        }
    }

    void CalculateDirection()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance <= triggerRange)
        {
            Vector2 direction = player.transform.position - transform.position;

            if (direction.x > 0)
            {
                // Flip frog to the right
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0)
            {
                // Flip frog to the left
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            // If direction.x is 0, do nothing (preserving the current scale)
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            // Check if the Shield game object is active
            GameObject shield = GameObject.FindWithTag("Shield");
            if (shield != null && shield.activeSelf)
            {
                animator.SetBool("slapattack", false);
            }
            else
            {
                // Shield is not active, call the TakeDamage method of the player
                other.GetComponent<Mover>().TakeDamage();
            }
        }

        if (other.gameObject.CompareTag("Sword"))
        {
            animator.SetTrigger("deadfrog");
        }
    }




    private void FrogDestroy()
    {
        Destroy(gameObject);
    }

    private void AtaqueSFX()
    {
        audioManager.PlaySFX(audioManager.Frogattack);

    }
    private void TriggerSFX()
    {
        audioManager.PlaySFX(audioManager.Frogtrigger);
    }

    private void DeathSFX()
    {
        audioManager.PlaySFX(audioManager.Froghit);
    }


}
