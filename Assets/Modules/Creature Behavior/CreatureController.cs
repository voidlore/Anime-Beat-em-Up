using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public float health, tickRate, moveForce = 5.0f, wanderRadius = 5.0f, duckHeight = 1.5f, targetOffset = 1.4f;
    
    private Vector3 randomPoint;
    private Rigidbody rb;
    public SpawnPointManager spawnPointManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Wander());
    }

    private void Update()
    {
        if (randomPoint == null)
            return;
        Vector3 direction = ((randomPoint + Vector3.up * targetOffset) - transform.position).normalized;
        rb.AddForce(direction * moveForce * Time.deltaTime * (moveForce - rb.velocity.magnitude));
    }
    public IEnumerator Wander()
    {
        Vector2 tempvector = Random.insideUnitCircle;
        randomPoint = (new Vector3(tempvector.x, 0, tempvector.y) * wanderRadius);
        yield return new WaitForSeconds(tickRate);
        StartCoroutine(Wander());
    }
}
