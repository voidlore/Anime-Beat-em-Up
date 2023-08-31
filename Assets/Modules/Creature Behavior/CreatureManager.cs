using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuetilitySoftbody;

public class CreatureManager : MonoBehaviour
{
    public int targetEntityCount;
    public float creatureRespawnRate = 10.0f, creatureWanderRange = 5.0f;
    public List<CreatureController> activeCreatures;
    public List<GameObject> creaturePrefabs;
    public SpawnPointManager spawnPointManager, creatureWanderPoints;
    public Transform creatureTargetRotation;

    private void Start()
    {
        activeCreatures = new List<CreatureController>();
        StartCoroutine(ManageSpawn());
    }

    public IEnumerator ManageSpawn()
    {
        if (activeCreatures.Count < targetEntityCount)
        {
            Debug.Log("Spawning");
            Spawn();
        }
        yield return new WaitForSeconds(creatureRespawnRate);
        StartCoroutine(ManageSpawn());
    }

    public void Spawn()
    {
        CreatureController newCreature = Instantiate(creaturePrefabs[Random.Range(0, creaturePrefabs.Count)], spawnPointManager.GetSpawnPoint().transform.position, Quaternion.identity, this.transform).GetComponent<CreatureController>();
        activeCreatures.Add(newCreature);
        YueSoftbodyPhysics newSoftBody = newCreature.GetComponent<YueSoftbodyPhysics>();
        newSoftBody.tuning.PositionProportional = 300.0f;
        newCreature.transform.position += Vector3.up;
        newCreature.spawnPointManager = creatureWanderPoints;
        newCreature.moveForce = 30.0f;
        newCreature.tickRate = 20.0f;
        newCreature.GetComponent<RigidBodyMatchRotation>().target = creatureTargetRotation;
        newCreature.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000);
    }
}
