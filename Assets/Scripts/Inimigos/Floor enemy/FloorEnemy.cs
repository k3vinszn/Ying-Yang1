using System.Collections;
using UnityEngine;

public class CustomizableEnemyAttack : MonoBehaviour
{
    public float detectionRange = 5f;
    public float moveSpeed = 5f;
    public float moveUpDistance = 0.5f; // Customize the distance to move up
    private GameObject player;
    private Vector3 originalPosition;
    private bool isMoving = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        originalPosition = transform.position;
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not found. Make sure the player has the 'Player' tag.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if the player is within the specified detection range
        if (distanceToPlayer < detectionRange && !isMoving)
        {
            StartCoroutine(MoveUpAndDown());
        }
    }

    IEnumerator MoveUpAndDown()
    {
        isMoving = true;

        // Move up by the specified distance in local space
        Vector3 targetPosition = transform.TransformPoint(Vector3.up * moveUpDistance);
        while (transform.position.y < targetPosition.y) // Move while the current position is below the target
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Move back down to the original position
        while (transform.position.y > originalPosition.y + 0.1f) // Adjust the threshold for reaching the original position
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }
}
