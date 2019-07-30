using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBuildsPool : MonoSingleton<BackgroundBuildsPool>
{
    [System.Serializable]
    public struct BuildingPool
    {
        public string city;
        public GameObject[] buildPrefabs;
        public int poolSize;
    }


    
    [Header("Prefabs")]
    public List<BuildingPool> cityPool;
    
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        int counter = 0;
        foreach (BuildingPool pool in cityPool)
        {
            for (int j = 0; j < pool.buildPrefabs.Length; j++)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.poolSize; i++)
                {

                    GameObject obj = Instantiate(pool.buildPrefabs[j]);
                    obj.SetActive(false);
                    obj.transform.SetParent(transform);
                    objectPool.Enqueue(obj);
                }
                string key = pool.city + j;
                //Debug.Log(key);
                poolDictionary.Add(key, objectPool);
            }

            counter++;
        }


    }
    public void closeObjects()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Background");

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }

    public GameObject SpawnFromPool(string cityName, int buildType, Vector3 position)
    {

        string tag = cityName + buildType;
       // Debug.Log(tag);
        if (!poolDictionary.ContainsKey(tag))
        {
            //Debug.Log(tag + "doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;

        poolDictionary[tag].Enqueue(objectToSpawn);


        return objectToSpawn;
    }

}
