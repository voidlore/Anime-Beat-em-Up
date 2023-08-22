using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class flyMovement : MonoBehaviour
    {
    public Transform    goal, target;
    private Vector3     randomPoint;
    private Vector3 storedPoint;
    public float       timer, moveForce = 5.0f, buzzUnitSphere = 5.0f;
    private bool        moveAgain;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAgain = true;
        StartCoroutine(GetNewRandomPointAroundTarget());
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //randomPoint = (Random.insideUnitSphere * 1.5f);
    //timer++;
    //if (timer % 75 == 0)
    //    {
    //    Debug.Log("new position set for now");
    //    storedPoint = randomPoint;
    //    transform.position = Vector3.MoveTowards(transform.position, storedPoint, Time.deltaTime * 6f);
    //    moveAgain = false;
    //    }
    //if(moveAgain)
    //    {
    //    transform.position = Vector3.MoveTowards(transform.position, goal.position, Time.deltaTime * 4f);
    //    }
    //else
    //    {
    //    transform.position = Vector3.MoveTowards(transform.position, storedPoint, Time.deltaTime * 8f);
    //    if (transform.position == storedPoint)
    //        {
    //        Debug.Log("should go back to normal path");
    //        moveAgain = true;
    //        }
    //    }
    //}
    //transform.position = Vector3.MoveTowards(transform.position, randomPoint, Time.deltaTime * 6f);
    //}

    private void Update()
    {
        if (target == null)
            return;

        // Calculate the direction to the target
        Vector3 direction = ((target.position + randomPoint) - transform.position).normalized;

        // Apply force in the direction of the target
        rb.AddForce(direction * moveForce * Time.deltaTime * (moveForce - rb.velocity.magnitude));
    }
    public IEnumerator GetNewRandomPointAroundTarget()
    {
        Debug.Log("GetNewRandomPointAroundTarget");
        yield return new WaitForSeconds(1.0f);
        randomPoint = (Random.insideUnitSphere * buzzUnitSphere);
        randomPoint.y = Mathf.Abs(randomPoint.y);
        StartCoroutine(GetNewRandomPointAroundTarget());
    }
}
