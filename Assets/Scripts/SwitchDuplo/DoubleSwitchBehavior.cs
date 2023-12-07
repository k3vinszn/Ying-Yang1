// DoubleSwitchBehavior script
using UnityEngine;

public class DoubleSwitchBehavior : MonoBehaviour
{
    [SerializeField] RepeatingDoorBehavior doorBehavior;
    [SerializeField] bool isDoorOpenSwitch;
    [SerializeField] bool isDoorClosedSwitch;

    float switchSizeY;

    float switchSpeed = 1f;
    bool hasBeenPressed = false;
    static int switchesPressedCount = 0;  
    
    Vector3 switchUpPos;
    Vector3 switchDownPos;


    void Awake()
    {
        switchSizeY = transform.localScale.y / 2;
        switchUpPos = new Vector3(transform.position.x, transform.position.y + switchSizeY, transform.position.z);
        switchDownPos = transform.position;
    }

    void Update()
    {
        if (hasBeenPressed)
        {
            MoveSwitchDown();
        }
        else
        {
            MoveSwitchUp();
        }
    }

    void MoveSwitchDown()
    {
        if (transform.position != switchDownPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, switchDownPos, switchSpeed * Time.deltaTime);
        }
    }

    void MoveSwitchUp()
    {
        if (transform.position != switchUpPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, switchUpPos, switchSpeed * Time.deltaTime);
        }
    }
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
