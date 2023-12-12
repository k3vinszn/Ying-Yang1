using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    public Camera mainCamera;
    public float padding = 1f; // Adjust padding as needed

    private float minX, maxX, minY, maxY;

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not assigned!");
            return;
        }

        CalculateBounds();
    }

    void Update()
    {
        // Clamp player position within the calculated bounds
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void CalculateBounds()
    {
        if (mainCamera.orthographic)
        {
            float cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
            float cameraHalfHeight = mainCamera.orthographicSize;

            minX = mainCamera.transform.position.x - cameraHalfWidth + padding;
            maxX = mainCamera.transform.position.x + cameraHalfWidth - padding;
            minY = mainCamera.transform.position.y - cameraHalfHeight + padding;
            maxY = mainCamera.transform.position.y + cameraHalfHeight - padding;
        }
        else
        {
            Debug.LogError("Main Camera is not orthographic!");
        }
    }
}
