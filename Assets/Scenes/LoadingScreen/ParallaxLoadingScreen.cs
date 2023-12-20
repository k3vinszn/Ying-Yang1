using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLoadingScreen : MonoBehaviour
{
    public float parallaxSpeed = 1.0f;

    void Update()
    {
        // Calculate the parallax offset based on time and speed
        float offset = Time.time * parallaxSpeed;

        // Apply the offset to the layer's position
        transform.position = new Vector3(offset, transform.position.y, transform.position.z);
    }
}
