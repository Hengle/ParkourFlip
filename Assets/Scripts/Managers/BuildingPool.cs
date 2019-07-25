using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPool : MonoSingleton<BuildingPool>
{

    #region Level Pool
    [System.Serializable]
    public struct LevelPool
    {
        public int levelNumber;
        public GameObject levelPrefab;
    }
    #endregion

    #region Lists
    [Header("Level Pool")]
    [Tooltip("Put here your level prefabs with level numbers")]
    [SerializeField] List<LevelPool> levelPool;
    [SerializeField] Dictionary<int, Queue<GameObject>> poolDictionary;
    #endregion

    #region Functions
    void Awake()
    {
        poolDictionary = new Dictionary<int, Queue<GameObject>>();

        foreach (LevelPool pool in levelPool)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            GameObject obj = Instantiate(pool.levelPrefab);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            objectPool.Enqueue(obj);

            poolDictionary.Add(pool.levelNumber, objectPool);
        }
    }

    public int getLevelCount()
    {
        return poolDictionary.Count + 1;
    }

    public void closeObject(int level)
    {
        GameObject obj = poolDictionary[level].Dequeue();
        obj.SetActive(false);
        poolDictionary[level].Enqueue(obj);
    }

    public GameObject SpawnFromPool(int level)
    {

        if (!poolDictionary.ContainsKey(level))
        {
            Debug.Log("Level " + level + " doesn't exist.");
            return null;
        }

        if (level != 1)
        {
            GameObject obj = poolDictionary[level - 1].Dequeue();
            obj.SetActive(false);
            poolDictionary[level - 1].Enqueue(obj);
        }

        GameObject objectToSpawn = poolDictionary[level].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = Vector3.zero;
        poolDictionary[level].Enqueue(objectToSpawn);


        return objectToSpawn;
    }
    #endregion
}
