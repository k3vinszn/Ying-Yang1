// SingleSwitchBehavior script
using UnityEngine;

public class SingleSwitchVerticallBehavior : MonoBehaviour
{
    [SerializeField] RepeatingDoorBehavior doorBehavior;
    [SerializeField] bool isDoorOpenSwitch;
    [SerializeField] bool isDoorClosedSwitch;

    bool hasBeenPressed = false;
    static int switchesPressedCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !hasBeenPressed)
        {
            hasBeenPressed = true;
            switchesPressedCount++;

            if (switchesPressedCount >= 1)
            {
                ToggleDoorState();
                switchesPressedCount = 0; // Reset the count for the next use
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player saiu de cima do trigger");
            hasBeenPressed = false; // Reset the switch when the player leaves
            switchesPressedCount = 0; // Reset the count if one switch is released
        }
    }

    void ToggleDoorState()
    {
        if (isDoorOpenSwitch)
        {
            doorBehavior.ToggleDoorState();
        }
        else if (isDoorClosedSwitch)
        {
            doorBehavior.ToggleDoorState();
        }
    }
}
