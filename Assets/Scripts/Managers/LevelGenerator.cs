using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoSingleton<LevelGenerator>
{

    BuildingPoolManager buildingPoolManager;
    BackgroundBuildsPool backgroundBuildsPool;
    private BuildManager buildManager;

    Vector3 startPosition = new Vector3(-21.478f, -13.85f, 0);
    Vector3 startPositionBackground = new Vector3(-21.478f, -13.85f, 100);
    Vector3 stepVector;
    Vector3 nextTargetPosition;
    Vector3 nextTargetPositionBackground;
    Vector3 stepBackgroundVector = new Vector3(50, 0, 0);

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
        nextTargetPositionBackground = startPositionBackground;
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

    int getBackGroundPrefabCount(string cityName)
    {
        int condition = backgroundBuildsPool.cityPool.Count;
        for (int i = 0; i < condition; i++)
        {
            if (backgroundBuildsPool.cityPool[i].city == cityName)
            {
                return backgroundBuildsPool.cityPool[i].buildPrefabs.Length;
            }
        }

        return 0;
    }

    public void createLevel(int countOfBuild, string cityName)
    {
        _randomX = Random.Range(45, 100);
        stepVector = new Vector3(_randomX, 0, 0);

        buildManager.buildingScripts.Clear();

        nextTargetPosition = startPosition;

        buildingPoolManager.spawnStartBuild(nextTargetPosition);

        nextTargetPosition += stepVector;

        backgroundBuildsPool.closeObjects();


        for (int i = 0; i < countOfBuild; i++)
        {

            int randomType = Random.Range(0, getPrefabCount(cityName));

            GameObject go = buildingPoolManager.SpawnFromPool(cityName, randomType, nextTargetPosition);

            randomType = Random.Range(0, getBackGroundPrefabCount(cityName));

            buildManager.buildingScripts.Add(go.GetComponent<BuildingScript>());
            nextTargetPosition += stepVector;

            //buildingPoolManager.SpawnFromPool(cityName, randomType, nextTargetPositionBackground);
            //nextTargetPositionBackground += stepVector;
        }

        for (int i = 0; i < countOfBuild * 2; i++)
        {
            int randomType = Random.Range(0, getBackGroundPrefabCount(cityName));
            backgroundBuildsPool.SpawnFromPool(cityName, randomType, nextTargetPositionBackground);

            nextTargetPositionBackground += stepBackgroundVector;
        }

        GameObject g = buildingPoolManager.spawnEndBuild(nextTargetPosition);
        buildManager.buildingScripts.Add(g.GetComponent<BuildingScript>());

    }


}
