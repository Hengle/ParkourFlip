using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{

    #region Variables

    [Tooltip("Just to see current level, do not change at runtime")]
    [SerializeField] int CurrentLevel;

    private int randomLevel;

    BuildingPool buildingPool;

    private BuildingPoolManager buildingPoolManager;

    private BuildManager buildManager;

    private LevelGenerator levelGenerator;
    #endregion
    #region  Functions
    public void Awake()
    {
        levelGenerator = LevelGenerator.Instance;
        buildManager = BuildManager.Instance;
        buildingPoolManager = BuildingPoolManager.Instance;
    }

    private void Start()
    {
        //levelGenerator.createLevel(5,"Istanbul");
        //buildManager.ControlBuildings();
        ControlLevel();
    }

    private void ControlLevel() 
    {
        if (PlayerPrefs.GetInt("Level") == 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        CurrentLevel = PlayerPrefs.GetInt("Level");
    }

    public void LevelUp() // Bir sonraki stage;
    {
        buildingPoolManager.closeObjects();
        
        randomLevel = Random.Range(-1, 3);
        levelGenerator.createLevel(randomLevel + CurrentLevel,"Istanbul");
        CurrentLevel = PlayerPrefs.GetInt("Level");

        PlayerPrefs.SetInt("Level", ++CurrentLevel);

        CurrentLevel = PlayerPrefs.GetInt("Level");
    }
    public void ResetLevel()//Reset all the game
    {
        PlayerPrefs.SetInt("Level", 1);
        buildingPool.closeObject(CurrentLevel);
        BuildBuildings();

        CurrentLevel = PlayerPrefs.GetInt("Level");
    }

    private void BuildBuildings()
    {
        buildingPool.SpawnFromPool(CurrentLevel);
    }

    public int getCurrentLevel()
    {
        return CurrentLevel;
    }

    #endregion


}
