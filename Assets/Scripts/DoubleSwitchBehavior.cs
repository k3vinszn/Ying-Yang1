using UnityEngine;

public class DoubleSwitchBehavior : MonoBehaviour
{
    [SerializeField] RepeatingDoorBehavior doorBehavior; // Change the type to RepeatingDoorBehavior
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

            if (switchesPressedCount >= 2)
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
        if (isDoorOpenSwitch && !doorBehavior.isDoorOpen)
        {
            doorBehavior.isDoorOpen = true;
        }
        else if (isDoorClosedSwitch && doorBehavior.isDoorOpen)
        {
            doorBehavior.isDoorOpen = false;
        }
    }
}
