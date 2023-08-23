using UnityEngine;

public class MoveAwayOnApproach : MonoBehaviour
{
    public Transform controlledObject;
    public Transform targetObject;
    public float approachDistance = 2.0f;
    public float moveSpeed = 5.0f;
    public float damping = 5.0f;

    private Vector3 startingPosition;
    private bool movingAway = false;

    private void Start()
    {
        startingPosition = controlledObject.position;
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(controlledObject.position, targetObject.position);

        if (distanceToTarget <= approachDistance)
        {
            // The target object is close, so move away from it.
            movingAway = true;
        }
        else
        {
            // The target object is far, so move back to the starting position.
            movingAway = false;
        }

        // Move the object based on its current state.
        if (movingAway)
        {
            MoveAwayFromTarget();
        }
        else
        {
            MoveToStartingPosition();
        }
    }

    private void MoveAwayFromTarget()
    {
        Vector3 moveDirection = controlledObject.position - targetObject.position;
        controlledObject.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    private void MoveToStartingPosition()
    {
        Vector3 moveDirection = startingPosition - controlledObject.position;
        controlledObject.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);

        // Apply damping to make the movement smoother
        controlledObject.position = Vector3.Lerp(controlledObject.position, startingPosition, Time.deltaTime * damping);
    }
}
