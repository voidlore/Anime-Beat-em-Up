using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnPointManager))]
public class SpawnPointManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpawnPointManager spawnPointManager = (SpawnPointManager)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Populate Spawn Points"))
        {
            Undo.RecordObject(spawnPointManager, "Populate Spawn Points");

            spawnPointManager.spawnPoints.Clear();

            foreach (Transform child in spawnPointManager.transform)
            {
                if (child.gameObject != spawnPointManager.gameObject)
                {
                    spawnPointManager.spawnPoints.Add(child.gameObject);
                }
            }
        }
    }
}
