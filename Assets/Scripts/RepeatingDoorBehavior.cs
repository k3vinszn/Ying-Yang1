using System.Collections;
using UnityEngine;

public class RepeatingDoorBehavior : MonoBehaviour
{
    public bool isDoorOpen = false;
    Vector3 doorClosedPos;
    Vector3 doorOpenPos;
    public float doorSpeed = 1f;
    public float doorStayTime = 3f; // Time the door stays open
    private bool isMoving = false;

    void Awake()
    {
        doorClosedPos = transform.position;
        doorOpenPos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
    }

    void Update()
    {
        if (isDoorOpen && !isMoving)
        {
            StartCoroutine(MoveDoorAndStay());
        }
    }

    IEnumerator MoveDoorAndStay()
    {
        isMoving = true;
        // Move the door up
        yield return MoveDoor(doorOpenPos);

        // Wait for the specified time
        yield return new WaitForSeconds(doorStayTime);

        // Move the door down
        yield return MoveDoor(doorClosedPos);

        isDoorOpen = false; // Set the door state to closed
        isMoving = false; // Allow the door to be moved again
    }

    IEnumerator MoveDoor(Vector3 targetPos)
    {
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, doorSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
