using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    public int targetEntityCount;
    public float creatureRespawnRate = 10.0f;
    public List<CreatureController> activeCreatures;
    public List<GameObject> creaturePrefabs;
    public SpawnPointManager spawnPointManager;

    private void Start()
    {
        activeCreatures = new List<CreatureController>();
        StartCoroutine(ManageSpawn());
    }

    public IEnumerator ManageSpawn()
    {
        yield return new WaitForSeconds(creatureRespawnRate);
        if (activeCreatures.Count < targetEntityCount)
        {
            Debug.Log("Spawning");
            Spawn();
        }
        StartCoroutine(ManageSpawn());
    }

    public void Spawn()
    {
        CreatureController newCreature = Instantiate(creaturePrefabs[Random.Range(0, creaturePrefabs.Count)], spawnPointManager.GetSpawnPoint().transform.position, Quaternion.identity, this.transform).GetComponent<CreatureController>();
        activeCreatures.Add(newCreature);
    }
}
