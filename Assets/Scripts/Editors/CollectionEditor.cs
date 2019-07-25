using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CollectionManager))]
public class CollectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CollectionManager collectionManager = CollectionManager.Instance;

        if (GUILayout.Button("Collect Coin"))
        {
            collectionManager.collectCoin();
        }

        if (GUILayout.Button("Flip Up"))
        {
            collectionManager.flip();
        }

        if (GUILayout.Button("Perfect"))
        {
            collectionManager.perfect();
        }

        if (GUILayout.Button("Merge  Coins"))
        {
            collectionManager.levelOver();
        }

        if (GUILayout.Button("Reset Collected Coins"))
        {
            collectionManager.resetCollectedCoins();
        }

        if (GUILayout.Button("Reset Flip Count"))
        {
            collectionManager.resetFlip();
        }

        if (GUILayout.Button("Reset Perfect"))
        {
            collectionManager.resetPerfect();
        }

        if (GUILayout.Button("Reset All Coins"))
        {
            collectionManager.resetAllCoins();
        }
    }
}
