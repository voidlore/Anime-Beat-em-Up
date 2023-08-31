using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMatchRotation : MonoBehaviour
{
    public Transform target; // The target transform whose rotation we want to match
    public float forceMultiplier = 1.0f; // Adjust the strength of the force applied

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Quaternion targetRotation = target.rotation;
        Quaternion currentRotation = rb.rotation;

        // Calculate the rotation difference between the current rotation and the target rotation
        Quaternion rotationDifference = targetRotation * Quaternion.Inverse(currentRotation);

        // Convert the rotation difference to an angle-axis representation
        rotationDifference.ToAngleAxis(out float angle, out Vector3 axis);

        // Apply a torque force to the rigidbody based on the angle-axis representation
        Vector3 torqueForce = axis * angle * forceMultiplier;
        if (torqueForce.magnitude < 1000)
            rb.AddTorque(torqueForce);
    }
}
