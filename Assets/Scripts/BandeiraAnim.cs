
using UnityEngine;
public class FlagScript : MonoBehaviour
{
    private bool player1Crossed = false;
    private bool player2Crossed = false;

    public Animator flagAnimator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        string playerName = other.gameObject.name;

        if (playerName.Equals("Player 1"))
        {
            player1Crossed = true;
        }
        else if (playerName.Equals("Player 2"))
        {
            player2Crossed = true;
        }

        // will call the right animations after identifying the players
        CheckFlagStatus();
    }

    private void CheckFlagStatus()
    {
        // if both players triggered the checkpoint, play the animation accordingly
        if (player1Crossed && player2Crossed)
        {
            flagAnimator.SetTrigger("BothPassaram");
        }
        // Player 1 crossed the flag
        else if (player1Crossed)
        {
            flagAnimator.SetTrigger("YangPassou");
        }
        // Player 2 crossed the flag
        else if (player2Crossed)
        {
            flagAnimator.SetTrigger("YinPassou");
        }
    }
}

