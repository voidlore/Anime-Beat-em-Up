using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ResponsiveAudioDemo : MonoBehaviour
{
    public AudioSource[] sources;
    public List<AudioClip> clipQueue = new List<AudioClip>();
    public float currentPlayTime = 0;
    public bool playingOnSourceZero = true, nextClipLoaded = false;
    public UnityEvent clipFinished;

    public void QueueClip(AudioClip nextClip)
    {
        clipQueue.Add(nextClip);
    }

    private void LoadNextClip()
    {
        if (clipQueue.Count <= 0)
            return;

        AudioSource currentSource = playingOnSourceZero ? sources[1] : sources[0];
        currentSource.clip = clipQueue[0];
        clipQueue.RemoveAt(0);
        clipFinished.Invoke();
        nextClipLoaded = true;
    }

    private void PlayOnNextSource()
    {
        if (playingOnSourceZero)
        {
            sources[1].Play();
            sources[0].Pause();
        }
        else
        {
            sources[0].Play();
            sources[1].Pause();
        }

        playingOnSourceZero = !playingOnSourceZero;
        currentPlayTime = 0;
        nextClipLoaded = false;
    }

    private void Update()
    {
        if (clipQueue.Count <= 0)
            return;

        currentPlayTime += Time.deltaTime;

        AudioSource currentSource = playingOnSourceZero ? sources[0] : sources[1];
        float clipLength = currentSource.clip.length;

        if (!nextClipLoaded && currentPlayTime >= clipLength / 2)
        {
            LoadNextClip();
        }

        if (currentPlayTime >= clipLength)
        {
            PlayOnNextSource();
        }
    }
}
