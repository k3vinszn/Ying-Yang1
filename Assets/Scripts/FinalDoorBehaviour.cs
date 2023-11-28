using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorBehavior : MonoBehaviour
{
    public bool isDoorOpen = false;
    public int keysCollected = 0;
    public int keysRequired = 2;
    Vector3 doorClosedPos;
    Vector3 doorOpenPos;
    public float doorSpeed = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        doorClosedPos = transform.position;
        doorOpenPos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (keysCollected >= keysRequired && !isDoorOpen)
        {
            isDoorOpen = true;
        }

        if (isDoorOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        if (transform.position != doorOpenPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorOpenPos, doorSpeed * Time.deltaTime);
        }
    }

    void CloseDoor()
    {
        if (transform.position != doorClosedPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorClosedPos, doorSpeed * Time.deltaTime);
        }
    }
}
