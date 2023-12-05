using UnityEngine;

public class FollowerCollider : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float followSpeed = 5f; // Speed at which the collider follows the player

    void Update()
    {
        if (playerTransform != null)
        {
            // Follow the player along the X-axis only
            float targetX = playerTransform.position.x;
            Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

            // Move towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
