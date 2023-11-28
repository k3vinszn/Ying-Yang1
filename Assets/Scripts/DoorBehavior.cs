using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{

    public bool isDoorOpen = false;
    Vector3 doorclosedPos;
    Vector3 doorOpenPos;
    public float doorSpeed = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        doorclosedPos = transform.position;//  embaixo tem o valor em que a porta se move pra cima
        doorOpenPos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
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
        if(transform.position != doorOpenPos)
        {
            transform.position = Vector3.MoveTowards(transform.position,doorOpenPos,doorSpeed* Time.deltaTime);
        }
    }

    void CloseDoor()
    {
        if (transform.position != doorclosedPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorclosedPos, doorSpeed * Time.deltaTime);
        }
    }
}
