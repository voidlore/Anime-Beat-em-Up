using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerAndThumbDetector : MonoBehaviour
{
    public FingerTipExtractor fingerTipExtractorLeft, fingerTipExtractorRight;
    public bool triangleSign = false;
    public float epsilon = 0.03f;
    bool canPush = true;
    public GameObject airBlast;
    public GameObject kamehameha;

    private void FixedUpdate()
    {
        if (Vector3.Distance(fingerTipExtractorLeft.GetPointerPosition(), fingerTipExtractorRight.GetPointerPosition()) < epsilon
            && Vector3.Distance(fingerTipExtractorLeft.GetThumbPosition(), fingerTipExtractorRight.GetThumbPosition()) < epsilon
            /*&& !kamehameha.activeSelf*/)
        {
            triangleSign = true;
            canPush = false;
            airBlast.SetActive(true);
            airBlast.transform.position = fingerTipExtractorRight.GetPointerPosition() + (Vector3.forward * 2.0f);
            airBlast.transform.rotation = fingerTipExtractorLeft.GetPointerRotation();

        }
        else
        {
            triangleSign = false;
            airBlast.SetActive(false);
        }
    }
}
