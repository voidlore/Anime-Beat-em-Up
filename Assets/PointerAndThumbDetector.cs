using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerAndThumbDetector : MonoBehaviour
{
    public FingerTipExtractor fingerTipExtractorLeft, fingerTipExtractorRight;
    public bool triangleSign = false;
    public float epsilon = 0.01f;

    private void FixedUpdate()
    {
        if(Vector3.Distance(fingerTipExtractorLeft.GetPointerPosition(), fingerTipExtractorRight.GetPointerPosition()) < epsilon
            && Vector3.Distance(fingerTipExtractorLeft.GetThumbPosition(), fingerTipExtractorRight.GetThumbPosition()) < epsilon)
        {
            triangleSign = true;
        }
        else
        {
            triangleSign = false;
        }
    }
}
