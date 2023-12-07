// RepeatingDoorXBehavior script
using System.Collections;
using UnityEngine;

public class RepeatingDoorXBehavior : MonoBehaviour
{
    private bool isDoorOpen = false;
    
    public float doorSpeed = 1f;
    public float doorStayTime = 3f;
    private bool isMoving = false;
    
    private Vector3 doorClosedPos;
    private Vector3 doorOpenPos;

    void Awake()
    {
        doorClosedPos = transform.position;
        doorOpenPos = new Vector3(transform.position.x - 15f, transform.position.y, transform.position.z);
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

        // Do not set isDoorOpen to false here; keep the door state unchanged

        // Wait for the specified time again before allowing the door to be moved
        yield return new WaitForSeconds(doorStayTime);

        isMoving = false;
    }

    IEnumerator MoveDoor(Vector3 targetPos)
    {
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, doorSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void ToggleDoorState()
    {
        isDoorOpen = !isDoorOpen;
        StartCoroutine(MoveDoorAndStay());
    }

    // Add any other methods or events you may need
}
