using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BuildManager))]
public class BuildEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BuildManager buildManager = BuildManager.Instance;

        if (GUILayout.Button("Control Buildings"))
        {
            buildManager.ControlBuildings();
        }


    }
}
