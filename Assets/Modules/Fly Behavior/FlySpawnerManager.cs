using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawnerManager : MonoBehaviour
{
    public int targetFlycount;
    public List<GamePhase> gamePhaseList;
    public GamePhase introPhase, breakPhase, currentPhase;
    public List<flyMovement> flies;
    public GameObject flyPrefab;
    public SpawnPointManager spawnPointManager;
    public List<CreatureController> creatures;

    private void Start()
    {
        flies = new List<flyMovement>();
        ChangePhase(introPhase);
        StartCoroutine(ManageSpawn());
    }

    public void ChangePhase(GamePhase newPhase)
    {
        currentPhase = newPhase;
    }

    public IEnumerator ManageSpawn()
    {
        yield return new WaitForSeconds(currentPhase.flySpawnRate);
        if(flies.Count < currentPhase.numberOfFlies)
        {
            Debug.Log("Spawning a Fly");
            SpawnFly();
        }
        StartCoroutine(ManageSpawn());
    }

    public void SpawnFly()
    {
        flyMovement flyMovement = Instantiate(flyPrefab, spawnPointManager.GetSpawnPoint().transform.position, Quaternion.identity, this.transform).GetComponent<flyMovement>();
        flies.Add(flyMovement);
        flyMovement.target = GetStrongestCreature().gameObject.transform;
    }

    public CreatureController GetStrongestCreature()
    {
        CreatureController result = creatures[0];
        for(int i = 0; i < creatures.Count; i++)
        {
            if (creatures[i].health > result.health)
            {
                result = creatures[i];
            }
        }
        return result;
    }
}
