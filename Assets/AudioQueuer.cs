using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioQueuer : MonoBehaviour
{
    public List<AudioClip> clips;
    public ResponsiveAudioDemo responsiveAudio;
    public GameObject controlObject;

    public float minHeight = 0f;   // Minimum height for clip queue
    public float maxHeight = 10f;  // Maximum height for clip queue

    private void Start()
    {
        LoadClip();
    }

    public void LoadClip()
    {
        float normalizedHeight = Mathf.InverseLerp(minHeight, maxHeight, controlObject.transform.position.y);
        int clipIndex = Mathf.FloorToInt(normalizedHeight * clips.Count);
        responsiveAudio.QueueClip(clips[clipIndex]);
    }
}
