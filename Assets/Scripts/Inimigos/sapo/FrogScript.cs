using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private bool isAwake = false;
    public float Range = 4.2f;
    private Animator animator;
    private GameObject player;
    public float triggerRange;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandlePlayerDistance();
        CalculateDirection();
    }

    private void HandlePlayerDistance()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("PLAYER DISTANCE IS:" + playerDistance);

        if (playerDistance <= Range)
        {
            if (!isAwake)
            {
                animator.SetBool("activated", true);
                StartCoroutine(Wait2Seconds());
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
                animator.SetBool("iddle", true);
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

    IEnumerator Wait2Seconds()
    {
        yield return new WaitForSeconds(2.0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            other.GetComponent<Mover>().TakeDamage(); // Call the TakeDamage method of the player
            
        }

    }
}
