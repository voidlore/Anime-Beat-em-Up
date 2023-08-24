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
    public CreatureManager creatureManager;

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
        if(flies.Count < currentPhase.numberOfFlies && creatureManager.activeCreatures != null && creatureManager.activeCreatures.Count > 0)
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
        CreatureController result = creatureManager.activeCreatures[0];
        for(int i = 0; i < creatureManager.activeCreatures.Count; i++)
        {
            if (creatureManager.activeCreatures[i].health > result.health)
            {
                result = creatureManager.activeCreatures[i];
            }
        }
        return result;
    }
}
