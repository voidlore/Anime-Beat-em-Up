using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetWithOffset : MonoBehaviour
{
    public Transform target, offset;
    public bool matchPosition, matchRotation, offsetPosition, offsetRotation;

    void Update()
    {
        if(matchPosition)
        {
            transform.position = target.position;
            if(offsetPosition)
            {
                transform.position += offset.position;
            }
        }
        if(matchRotation)
        {
            transform.rotation = target.rotation;
            if(offsetRotation)
            {
                transform.rotation *= offset.rotation;
            }
        }
    }
}
