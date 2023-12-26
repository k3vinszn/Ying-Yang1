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
    }
    private void HandlePlayerDistance()
    {
        float PlayerDistance;

        PlayerDistance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("PLAYER DISTANCE IS:");
        Debug.Log(PlayerDistance);


        if (PlayerDistance <= Range) // when player in range for the attack
        {
            Debug.Log("player in range");
            if (isAwake == false) // enemy is not awake
            {
                animator.SetBool("activated", true); // range detect animation
                StartCoroutine(Wait2Seconds()); // trigger courotine to wait for 2 seconds
                animator.SetBool("slapattack", true);
                isAwake = true;

            }
            else
            {
                animator.SetBool("slapattack", true);
            }
        }

        else // when out of range
        {

            if (PlayerDistance <= triggerRange) // range for the red eyes animation
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger) // Updated tag check and added isTrigger check
        {
            other.GetComponent<Mover>().TakeDamage(); // Call the TakeDamage method of the player
        }
    }


    IEnumerator Wait2Seconds()// waits 2 seconds
    {
        yield return new WaitForSeconds(2.0f);
    }
}
