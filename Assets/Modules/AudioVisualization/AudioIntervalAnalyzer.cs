using UnityEngine;

public class AudioIntervalAnalyzer : MonoBehaviour
{
    public Percentage outputPercentage;
    public AudioAnalyzer audioAnalyzer;
    public float intervalStartHz = 200f;
    public float intervalEndHz = 800f;
    public float scalingFactor = 100f;

    private int startIndex;
    private int endIndex;

    private void Start()
    {
        startIndex = FrequencyToSpectrumIndex(intervalStartHz);
        endIndex = FrequencyToSpectrumIndex(intervalEndHz);
    }

    private void Update()
    {
        // Calculate the average amplitude within the interval
        float temp = CalculateAverageAmplitude(audioAnalyzer.audioSpectrum, startIndex, endIndex);
        outputPercentage.percentage =  temp * temp * temp * scalingFactor;
    }

    private int FrequencyToSpectrumIndex(float frequencyHz)
    {
        float fraction = frequencyHz / audioAnalyzer.clipFrequency;
        return Mathf.FloorToInt(fraction * audioAnalyzer.audioSpectrumSize);
    }

    private float CalculateAverageAmplitude(float[] spectrumData, int startIndex, int endIndex)
    {
        float sum = 0f;
        int numBins = endIndex - startIndex + 1;

        for (int i = startIndex; i <= endIndex; i++)
        {
            sum += spectrumData[i];
        }

        return sum / numBins;
    }
}
