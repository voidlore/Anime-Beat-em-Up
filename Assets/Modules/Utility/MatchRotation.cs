using System.Collections;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    public Transform targetTransLeft, targetTransformRight;        // The target transform to compare angles with
    public GameObject kamehamehaBeam;
    //public float angleThreshold = 100f;     // The angle threshold in degrees
    public bool isWithinAngle = false;       // Will be set to true if the angle condition is met
    public float updateFrequency = 0.1f;
    public float charge = 0f;
    public AudioClip[] audioClips;
    private AudioSource audioPlayer;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        StartCoroutine(CoroutineUpdate());
    }

    public IEnumerator CoroutineUpdate()
    {
        yield return new WaitForSeconds(updateFrequency);

        // Calculate the angle between the two transforms
        Vector3 directionToTarget = targetTransLeft.position - targetTransformRight.position;
        float angleToTarget = Vector3.Angle(targetTransformRight.forward, directionToTarget);

        // Check if the angle is within the specified threshold
        isWithinAngle = Mathf.Abs(Quaternion.Dot(targetTransformRight.rotation, targetTransLeft.rotation)) >= .85f;
        if (isWithinAngle)
        {
            isWithinAngle = Vector3.Distance(targetTransLeft.position, targetTransformRight.position) < 3f;
            if(isWithinAngle)
            {
                audioPlayer.clip = audioClips[0];
                audioPlayer.loop = true;
                if(!audioPlayer.isPlaying)
                {
                    audioPlayer.Play();
                }
                //play charging sound effect
                charge += 1;
            }
        }
        //Debug.Log(isWithinAngle);

        if(!isWithinAngle && charge > 0f)
        {
            if(Vector3.Distance(targetTransLeft.position, targetTransformRight.position) < 1f)
            {
                StartCoroutine(kamehameha());
            }
        }

        StartCoroutine(CoroutineUpdate());
    }


    public IEnumerator kamehameha()
    {
        audioPlayer.clip = audioClips[1];
        if (!audioPlayer.isPlaying)
        {
            audioPlayer.loop = true;
            audioPlayer.Play();
        }
        //instantiate a big beam attached to right wrist
        GameObject tempBeam = Instantiate(kamehamehaBeam, targetTransformRight, worldPositionStays: false);
        yield return new WaitForSeconds(charge / 10f);
        Destroy(tempBeam);
        audioPlayer.Stop();
        charge = 0f;
        //destroy beam

    }
}
