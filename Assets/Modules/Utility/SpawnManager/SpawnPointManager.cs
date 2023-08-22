using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public List<GameObject> spawnPoints = new();
    public GameObject GetSpawnPoint(int index) { return spawnPoints[index]; }
    public GameObject GetSpawnPoint() { return spawnPoints[Random.Range(0, spawnPoints.Count)]; }
}
