using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateJump : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isJumping = false;
    private float jumpHeight = 0.8f;
    private float jumpDuration = 0.2f;

    void Start()
    {
        // Store the original position of the GameObject
        originalPosition = transform.position;
    }

    void Update()
    {
        // Check if the space key is pressed and the player is not already jumping
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            // Start the jump coroutine
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        isJumping = true;

        // Move upwards
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = originalPosition + Vector3.up * jumpHeight;

        while (elapsedTime < jumpDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        // Pause at the top for a moment (you can adjust the duration)
        yield return new WaitForSeconds(0.1f);

        // Move downwards
        elapsedTime = 0f;
        startPos = transform.position;
        endPos = originalPosition;

        while (elapsedTime < jumpDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        isJumping = false;
    }
}
