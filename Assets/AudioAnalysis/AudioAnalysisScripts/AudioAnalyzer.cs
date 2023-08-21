using System.Collections;
using UnityEngine;

public class AudioAnalyzer : MonoBehaviour
{
    public float[] audioSpectrum;
    public float updateInterval = 0.1f; // Time between updates in seconds
    [Range(0, 8192)]
    public int audioSpectrumSize = 1024;
    AudioSource audioSource;
    public float clipFrequency = 400000;
    bool isAnalyzing = false;
    public float analysisOffset = 0.1f; // Offset in seconds

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSpectrum = new float[audioSpectrumSize];
        StartCoroutine(AnalyzeAudio());
    }

    IEnumerator AnalyzeAudio()
    {
        while (true)
        {
            // Check if the audio is playing and analysis is not in progress
            if (audioSource.isPlaying && !isAnalyzing)
            {
                // Set the flag to indicate that analysis is in progress
                isAnalyzing = true;

                // Perform frequency analysis
                audioSource.GetSpectrumData(audioSpectrum, 0, FFTWindow.BlackmanHarris);

                // Reset the flag to indicate that analysis is complete
                isAnalyzing = false;
            }

            // Wait for the specified update interval
            yield return new WaitForSeconds(updateInterval);
        }
    }
}
