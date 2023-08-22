using UnityEngine;

public class AudioSpectrumAnalyzer : MonoBehaviour
{
    public AudioSource audioSource;
    public int spectrumSize = 1024; // The number of samples used to calculate the spectrum data.
    public float maxFrequency = 2000f; // The maximum frequency to consider in the analysis.
    public float loudnessMultiplier = 100f; // A multiplier to make the loudness values more meaningful.

    private float[] spectrumData;
    public NoteData[] notes;

    // Array to hold the frequencies of all semitones on a grand piano.
    private readonly float[] pianoSemitoneFrequencies = new float[88];

    private void Start()
    {
        spectrumData = new float[spectrumSize];
        notes = new NoteData[spectrumSize / 2]; // We divide by 2 since the spectrum data is mirrored.

        // Calculate frequencies of all semitones on a grand piano (A0 to C8).
        for (int i = 0; i < 88; i++)
        {
            pianoSemitoneFrequencies[i] = 27.5f * Mathf.Pow(2f, i / 12f);
        }
    }

    private void Update()
    {
        // Ensure the audio source is playing and the spectrum data is available.
        if (audioSource.isPlaying && AudioSettings.dspTime > audioSource.time)
        {
            // Get the spectrum data.
            audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

            // Populate the notes array with frequency and loudness values.
            float frequencyStep = AudioSettings.outputSampleRate / 2f / (float)notes.Length;
            for (int i = 0; i < notes.Length; i++)
            {
                float frequency = i * frequencyStep;
                if (frequency > maxFrequency)
                    break;

                float closestPianoFrequency = FindClosestPianoSemitoneFrequency(frequency);

                int midiNote = GetMidiNoteFromFrequency(closestPianoFrequency);

                notes[i] = new NoteData
                {
                    frequency = frequency,
                    loudness = spectrumData[i] * loudnessMultiplier,
                    note = midiNote
                };
            }
        }
    }

    // Find the closest piano semitone frequency from the pre-calculated table.
    private float FindClosestPianoSemitoneFrequency(float frequency)
    {
        float closestFrequency = pianoSemitoneFrequencies[0];
        float minDistance = Mathf.Abs(frequency - closestFrequency);

        for (int i = 1; i < pianoSemitoneFrequencies.Length; i++)
        {
            float currentFrequency = pianoSemitoneFrequencies[i];
            float distance = Mathf.Abs(frequency - currentFrequency);

            if (distance < minDistance)
            {
                closestFrequency = currentFrequency;
                minDistance = distance;
            }
        }

        return closestFrequency;
    }

    // Get the MIDI note value from the piano semitone frequency.
    private int GetMidiNoteFromFrequency(float frequency)
    {
        float noteValue = 12f * Mathf.Log(frequency / 440f) / Mathf.Log(2f) + 69f;
        return Mathf.RoundToInt(noteValue);
    }
}

[System.Serializable]
public struct NoteData
{
    public float frequency;
    public float loudness;
    public int note; // MIDI note value representing the closest musical note.
}
