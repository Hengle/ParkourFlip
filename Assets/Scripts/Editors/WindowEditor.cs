using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WindowEditor : MonoBehaviour
{
#if UNITY_EDITOR

    [MenuItem("Parkour Flip/Clear All PlayerPrefs")]
    public static void ClearAllPrefabs()
    {
        PlayerPrefs.DeleteAll();
    }

#endif
}
