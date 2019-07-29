using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoSingleton<LevelGenerator>
{

    BuildingPoolManager buildingPoolManager;
    BackgroundBuildsPool backgroundBuildsPool;
    private BuildManager buildManager;

    Vector3 startPosition = new Vector3(-21.478f, -13.85f, 0);
    Vector3 startPositionBackground = new Vector3(-21.478f, -13.85f, 0);
    Vector3 stepVector;
    Vector3 nextTargetPosition;
    Vector3 nextTargetPositionBackground;

    private int _randomX; 

    public void Awake()
    {
        buildingPoolManager = BuildingPoolManager.Instance;
        backgroundBuildsPool = BackgroundBuildsPool.Instance;
        buildManager = BuildManager.Instance;
    }

    public void Start()
    {
        nextTargetPosition = startPosition;
        createLevel(2, "Istanbul");
    }

    private void Update()
    {
        
        Debug.Log(stepVector);
    }

    int getPrefabCount(string cityName)
    {
        int condition = buildingPoolManager.cityPool.Count;
        for (int i = 0; i < condition; i++)
        {
            if (buildingPoolManager.cityPool[i].city == cityName)
            {
                return buildingPoolManager.cityPool[i].buildPrefabs.Length;
            }
        }

        return 0;
    }

    public void createLevel(int countOfBuild, string cityName)
    {
        _randomX = Random.Range(45, 100);
        stepVector = new Vector3(_randomX,0,0);
        
        buildManager.buildingScripts.Clear();

        nextTargetPosition = startPosition;
        
        buildingPoolManager.spawnStartBuild(nextTargetPosition);
        

        nextTargetPosition += stepVector;


        for (int i = 0; i < countOfBuild; i++)
        {
            
            int randomType = Random.Range(0, getPrefabCount(cityName));

            GameObject go =buildingPoolManager.SpawnFromPool(cityName, randomType  , nextTargetPosition);
            
            
            
            buildManager.buildingScripts.Add(go.GetComponent<BuildingScript>());
            nextTargetPosition += stepVector;

           //buildingPoolManager.SpawnFromPool(cityName, randomType, nextTargetPositionBackground);
           //nextTargetPositionBackground += stepVector;
        }

        GameObject g = buildingPoolManager.spawnEndBuild(nextTargetPosition);
        buildManager.buildingScripts.Add(g.GetComponent<BuildingScript>());
        
    }


}
