using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoSingleton<LevelGenerator>
{

    BuildingPoolManager buildingPoolManager;
    BackgroundBuildsPool backgroundBuildsPool;
    private BuildManager buildManager;

    Vector3 startPosition = new Vector3(-21.478f, -13.85f, 0);
    Vector3 startPositionBackground = new Vector3(15f, -13.85f, 60);
    Vector3 stepVector;
    Vector3 nextTargetPosition;
    Vector3 nextTargetPositionBackground;
    Vector3 stepBackgroundVector = new Vector3(48, 0, 0);


    private int _countOfBuild;
    private Vector3 _difference = new Vector3(27,0,0);
    public Vector3 _trajectory;
    private float _endBuildPosition;
    private float _backGroundCount;
    private float _endStartDif;
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
        //createLevel(_countOfBuild, "Istanbul");
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
        _randomX = 80;
        stepVector = new Vector3(_randomX, 0, 0);

        nextTargetPositionBackground = startPositionBackground;

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
            
            _randomX = Random.Range(55, 100);
            stepVector = new Vector3(_randomX, 0, 0);
            //buildManager.buildingScripts.Add(go.GetComponent<BuildingScript>());
            nextTargetPosition += stepVector;

            //buildingPoolManager.SpawnFromPool(cityName, randomType, nextTargetPositionBackground);
            //nextTargetPositionBackground += stepVector;
        }
       
        GameObject g = buildingPoolManager.spawnEndBuild(nextTargetPosition);
        
        _endBuildPosition = g.transform.position.x;
        
        _endStartDif = _endBuildPosition + startPosition.x;

        _backGroundCount = _endStartDif / 45 + 1;
        
        
        for (int i = 0; i < _backGroundCount; i++)
        {
            int randomType = Random.Range(0, getBackGroundPrefabCount(cityName));
            backgroundBuildsPool.SpawnFromPool(cityName, randomType, nextTargetPositionBackground);
            nextTargetPositionBackground += stepBackgroundVector;
        }

        // buildManager.buildingScripts.Add(g.GetComponent<BuildingScript>());
    }


    public float getEndBuildPos()
    {
        return _endBuildPosition;
    }
    
}
