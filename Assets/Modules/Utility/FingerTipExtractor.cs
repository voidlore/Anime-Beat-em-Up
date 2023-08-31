using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTipExtractor : MonoBehaviour
{
    public HandVisual handVisual; // Reference to the HandVisual script

    public Vector3 GetPointerPosition()
    {
        return handVisual.GetTransformByHandJointId(Oculus.Interaction.Input.HandJointId.HandIndexTip).transform.position;
    }

    public Vector3 GetThumbPosition()
    {
        return handVisual.GetTransformByHandJointId(Oculus.Interaction.Input.HandJointId.HandThumbTip).transform.position;
    }

    public Quaternion GetPointerRotation()
    {
        return handVisual.GetTransformByHandJointId(Oculus.Interaction.Input.HandJointId.HandIndexTip).transform.rotation;
    }
}
