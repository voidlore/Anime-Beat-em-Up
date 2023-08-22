using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GamePhase))]
public class GamePhaseEditor : Editor
{
    [MenuItem("Assets/Create/Game Phase")]
    public static void CreateGamePhase()
    {
        GamePhase newGamePhase = ScriptableObject.CreateInstance<GamePhase>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (System.IO.Path.GetExtension(path) != "")
        {
            path = path.Replace(System.IO.Path.GetFileName(path), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/NewGamePhase.asset");

        AssetDatabase.CreateAsset(newGamePhase, assetPathAndName);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newGamePhase;
    }
}
