using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLoadingScreen2 : MonoBehaviour
{
    public float parallaxSpeed = 1.0f;
    private Vector3 initialPosition;

    void Awake()
    {
        // Save the initial position of the object
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the parallax offset based on time and speed
        float offset = Time.time * parallaxSpeed;

        // Apply the offset to the layer's position, keeping the initial Y and Z values
        transform.position = new Vector3(initialPosition.x + offset, initialPosition.y, initialPosition.z);
    }
}
