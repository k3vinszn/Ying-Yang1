using UnityEngine;
using System.Collections;

public class FrogScript : MonoBehaviour
{
    private bool isAwake = false;
    public float Range = 10.0f;
    public float PlayerDistance = 8.0f;
    private Animator animator;

    private void HandlePlayerDistance()
    {
        if (PlayerDistance <= Range)
        {
            if (isAwake==false) // enemy is not awake
            {
                animator.SetBool("triggerpar", true); // range detected animation
                StartCoroutine(Wait2Seconds()); // triggers courotine to wait 2 seconds
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
            animator.SetBool("idlepar", true);
            isAwake = false;
        }
    }

    IEnumerator Wait2Seconds() // courotine to wait 2 seconds
    {
        yield return new WaitForSeconds(2.0f);
   
    }
}
