using System.Collections;
using UnityEngine;

public class CheckAngle : MonoBehaviour
{
    public Transform targetTransLeft, targetTransformRight;        // The target transform to compare angles with
    public float angleThreshold = 45.0f;     // The angle threshold in degrees
    public bool isWithinAngle = false;       // Will be set to true if the angle condition is met
    public float updateFrequency = 0.1f;

    private void Start()
    {
        StartCoroutine(CoroutineUpdate());
    }

    public IEnumerator CoroutineUpdate()
    {
        yield return new WaitForSeconds(updateFrequency);
        // Calculate the angle between the two transforms
        Vector3 directionToTarget = targetTransLeft.position - targetTransformRight.position;
        float angleToTarget = Vector3.Angle(targetTransformRight.forward, directionToTarget);

        // Check if the angle is within the specified threshold
        isWithinAngle = Mathf.Abs(angleToTarget - 180.0f) <= angleThreshold;
        StartCoroutine(CoroutineUpdate());
    }
}
