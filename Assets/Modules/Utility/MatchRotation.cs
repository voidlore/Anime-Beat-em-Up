using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    public Transform targetTransform;
    public float torqueStrength = 1.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Quaternion rotationDifference = targetTransform.rotation * Quaternion.Inverse(rb.rotation);
        Vector3 torqueAxis;
        float torqueAngle;
        rotationDifference.ToAngleAxis(out torqueAngle, out torqueAxis);
        Vector3 torque = torqueStrength * torqueAngle * Mathf.Deg2Rad * torqueAxis;
        rb.AddTorque(torque, ForceMode.Force);
    }
}
