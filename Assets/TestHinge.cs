using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHinge : MonoBehaviour
{
    private HingeJoint2D hingeJoint;
    private float maxAngle = 30f; // Adjust this according to your max angle limit
    private bool movingForward = true;

    void Start()
    {
        hingeJoint = GetComponent<HingeJoint2D>();
        // Ensure the motor is enabled
        hingeJoint.useMotor = true;
    }

    void Update()
    {
        float currentAngle = hingeJoint.jointAngle;

        // Check if the current angle exceeds the max angle limit
        if (Mathf.Abs(currentAngle) >= maxAngle)
        {
            // Toggle the direction
            movingForward = !movingForward;

            // Reverse the motor to move back
            JointMotor2D motor = hingeJoint.motor;
            motor.motorSpeed = -motor.motorSpeed; // Reverse the motor speed
            hingeJoint.motor = motor;
        }
    }
}
