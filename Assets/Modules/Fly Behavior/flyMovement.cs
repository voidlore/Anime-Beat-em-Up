using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class flyMovement : MonoBehaviour
{
    //Useful
    public float        health = 100;
    public Transform    target;
    public float        timer, moveForce = 5.0f, buzzUnitSphere = 5.0f;

    //Useless
    private Rigidbody   rb;
    private Vector3     randomPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(GetNewRandomPointAroundTarget());
    }

    private void Update()
    {
        if (target == null)
            return;
        Vector3 direction = ((target.position + randomPoint) - transform.position).normalized;
        rb.AddForce(direction * moveForce * Time.deltaTime * (moveForce - rb.velocity.magnitude));
    }
    public IEnumerator GetNewRandomPointAroundTarget()
    {
        yield return new WaitForSeconds(1.0f);
        randomPoint = (Random.insideUnitSphere * buzzUnitSphere);
        randomPoint.y = Mathf.Abs(randomPoint.y);
        StartCoroutine(GetNewRandomPointAroundTarget());
    }
}


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
