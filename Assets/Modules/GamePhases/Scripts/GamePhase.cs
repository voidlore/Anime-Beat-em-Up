using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GamePhase))]
public class GamePhase : ScriptableObject
{
    public float duration, flySpeed, flySpeedVariance, flySpawnRate;
    public int numberOfFlies;
}
