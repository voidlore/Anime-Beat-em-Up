using UnityEngine;

public class AudioHistogramVisualizer : MonoBehaviour
{
    public AudioAnalyzer audioAnalyzer;
    public GameObject cubePrefab;
    public Transform cubesParent;
    public float intervalStartHz = 200f;
    public float intervalEndHz = 800f;
    public int numberOfCubes = 10;
    public float cubeSpacing = 1f;
    public float maxHeight = 100f;
    public float analysisOffset = 0.1f; // Offset in seconds
    public float cubeHeightMultiplier = 10000;
    public GameObject hand;

    private GameObject[] cubes;
    private float binSize;
    private float offsetTimer;

    private void Start()
    {
        cubes = new GameObject[numberOfCubes];
        binSize = (intervalEndHz - intervalStartHz) / numberOfCubes;
        CreateCubes();
        offsetTimer = 0f;
    }

    private void CreateCubes()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            float startFreqHz = intervalStartHz + (i * binSize);
            float endFreqHz = startFreqHz + binSize;

            float[] spectrumData = GetSpectrumDataInRange(startFreqHz, endFreqHz);
            float averageAmplitude = CalculateAverageAmplitude(spectrumData);

            Vector3 cubePosition = transform.position + new Vector3(averageAmplitude, i * cubeSpacing, 0f);
            GameObject cube = Instantiate(cubePrefab, cubePosition, Quaternion.identity, cubesParent);
            cube.GetComponent<MoveAwayOnApproach>().targetObject = hand.transform;
            cubes[i] = cube; // Assign the instantiated cube to the array
        }
    }

    private float[] GetSpectrumDataInRange(float startFreqHz, float endFreqHz)
    {
        int startIndex = FrequencyToSpectrumIndex(startFreqHz);
        int endIndex = FrequencyToSpectrumIndex(endFreqHz);
        int numBins = endIndex - startIndex + 1;
        float[] spectrumData = new float[numBins];

        for (int i = 0; startIndex + i < audioAnalyzer.audioSpectrum.Length && i < spectrumData.Length; i++)
        {
            spectrumData[i] = audioAnalyzer.audioSpectrum[startIndex + i];
        }

        return spectrumData;
    }

    private int FrequencyToSpectrumIndex(float frequencyHz)
    {
        float fraction = frequencyHz / audioAnalyzer.clipFrequency;
        return Mathf.FloorToInt(fraction * audioAnalyzer.audioSpectrumSize);
    }

    private float CalculateAverageAmplitude(float[] spectrumData)
    {
        float sum = 0f;

        for (int i = 0; i < spectrumData.Length; i++)
        {
            sum += spectrumData[i];
        }

        return sum / spectrumData.Length;
    }

    private void Update()
    {
        if (cubes == null)
            return;

        offsetTimer += Time.deltaTime;

        if (offsetTimer >= analysisOffset)
        {
            for (int i = 0; i < numberOfCubes; i++)
            {
                if (cubes[i] != null) // Check if cube is null before updating
                {
                    float startFreqHz = intervalStartHz + (i * binSize);
                    float endFreqHz = startFreqHz + binSize;

                    float[] spectrumData = GetSpectrumDataInRange(startFreqHz, endFreqHz);
                    float averageAmplitude = CalculateAverageAmplitude(spectrumData);

                    Vector3 newScale = new Vector3(
                        (4 * cubeSpacing) + Mathf.Min(averageAmplitude * cubeHeightMultiplier * Mathf.Max((i + 1), 100), maxHeight),
                        cubeSpacing,
                        cubeSpacing);
                    cubes[i].transform.localScale = newScale;
                }
            }
            offsetTimer = 0f;
        }
    }
}
