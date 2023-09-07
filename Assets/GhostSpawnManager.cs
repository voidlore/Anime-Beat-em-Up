using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawnManager : MonoBehaviour
{
    public int targetghostcount;
    public List<GamePhase> gamePhaseList;
    public GamePhase introPhase, breakPhase, currentPhase;
    public List<flyMovement> flies;
    public GameObject flyPrefab, playerHead;
    public SpawnPointManager spawnPointManager;

    private void Start()
    {
        flies = new();
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
        if (flies.Count < currentPhase.numberOfFlies)
        {
            //Debug.Log("Spawning a Fly");
            SpawnFly();
        }
        StartCoroutine(ManageSpawn());
    }

    public void SpawnFly()
    {
        flyMovement flyMovement = Instantiate(flyPrefab, spawnPointManager.GetSpawnPoint().transform.position, Quaternion.identity, this.transform).GetComponent<flyMovement>();
        flies.Add(flyMovement);
        flyMovement.target = playerHead.transform;
        flyMovement.moveForce = 15.0f;
    }
}
