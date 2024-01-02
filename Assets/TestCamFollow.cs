using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamFollow : MonoBehaviour
{
    public Transform target; // The player's transform
    public float smoothSpeed = 0.125f; // How quickly the camera follows the player
    public Vector3 offset; // The offset from the player

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
