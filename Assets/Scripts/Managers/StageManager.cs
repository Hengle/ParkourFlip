using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{

    #region Variables

    [Tooltip("Just to see current level, do not change at runtime")]
    [SerializeField] int CurrentLevel;

    BuildingPool buildingPool;

    #endregion
    #region  Functions
    public void Awake()
    {
        buildingPool = BuildingPool.Instance;

        ControlLevel();
        BuildBuildings();
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
        buildingPool.closeObject(CurrentLevel);
        CurrentLevel = PlayerPrefs.GetInt("Level");

        PlayerPrefs.SetInt("Level", ++CurrentLevel);

        CurrentLevel = PlayerPrefs.GetInt("Level");

        if (CurrentLevel >= buildingPool.getLevelCount())
        {
            CurrentLevel=1;
            PlayerPrefs.SetInt("Level", CurrentLevel);
        }

        BuildBuildings();
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

    #endregion


}
