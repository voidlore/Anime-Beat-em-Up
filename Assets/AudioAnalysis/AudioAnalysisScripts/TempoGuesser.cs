using UnityEngine;

/*
 * The idea is to establish four thigns
 * 1. BPM
 * 2. Comparative speedup
 * 3. Comparative slowdown
 * 4. Silence
 * 
 * BPM will be an int
 * 
 * Comparative speedup and slowdown will be bools
 *  representing the last four beats as quarternotes would be no slowdown and no speedup
 *  representing the last four beats as eighth notes would be a speedup
 *  representing the last four beats as half notes would be a slowdown
 *  
 * Silence is true if it is silent for a beat
 */

public class TempoGuesser : MonoBehaviour
{
    public AudioIntervalAnalyzer audioIntervalAnalyzer;
    public float threshold = 0.5f; // Adjust this threshold to fit your needs
    public int windowSize = 10; // Number of measurements to consider for tempo calculation

    private float[] amplitudeBuffer;
    private float[] timeBuffer;
    private int bufferIndex;
    private bool isBufferFilled;
    private float tempo;

    private void Start()
    {
        amplitudeBuffer = new float[windowSize];
        timeBuffer = new float[windowSize];
        bufferIndex = 0;
        isBufferFilled = false;
        tempo = 0f;
    }

    private void Update()
    {
        float currentAmplitude = audioIntervalAnalyzer.outputPercentage.percentage;

        // Check if the current amplitude has exceeded the threshold
        if (currentAmplitude > threshold)
        {
            float currentTime = Time.time;

            // Store the amplitude and time in the buffer
            amplitudeBuffer[bufferIndex] = currentAmplitude;
            timeBuffer[bufferIndex] = currentTime;

            // Update buffer index
            bufferIndex++;
            if (bufferIndex >= windowSize)
            {
                bufferIndex = 0;
                isBufferFilled = true;
            }

            if (isBufferFilled)
            {
                // Calculate the time differences between loud peaks
                float[] timeDifferences = new float[windowSize];
                for (int i = 0; i < windowSize - 1; i++)
                {
                    timeDifferences[i] = timeBuffer[(bufferIndex + i) % windowSize] - timeBuffer[(bufferIndex + i + 1) % windowSize];
                }

                // Calculate the average time difference
                float averageTimeDifference = 0f;
                for (int i = 0; i < windowSize - 1; i++)
                {
                    averageTimeDifference += timeDifferences[i];
                }
                averageTimeDifference /= windowSize - 1;

                // Calculate the tempo (beats per minute)
                if (averageTimeDifference > 0f)
                {
                    tempo = 60f / averageTimeDifference;
                }
            }
        }
    }
}
