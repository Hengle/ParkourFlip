using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(StageManager))]
public class StageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        StageManager stageManager = StageManager.Instance;

        if(GUILayout.Button("Level Up"))
        {
            stageManager.LevelUp();
        }
        
        if(GUILayout.Button("Level Reset"))
        {
            stageManager.ResetLevel();
        }
    }
}
