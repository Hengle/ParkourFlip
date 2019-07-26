using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoSingleton<CollectionManager>
{
    #region Variables
    [Tooltip("Collected coins in current level")]
    [SerializeField] int collectedCoins = 0;
    [SerializeField] int flipCount = 0;
    [SerializeField] int perfectCount = 0;
    [SerializeField] int allCoins = 0;
    [SerializeField] int score = 0;

    #endregion
    #region Functions


    void Awake()
    {
        allCoins = PlayerPrefs.GetInt("Coins");
    }

    public void collectCoin()
    {
        collectedCoins++;
    }

    public void flip()
    {
        flipCount++;
    }

    public void perfect()
    {
        perfectCount++;
    }

    public void levelOver()
    {
        allCoins += collectedCoins;
        collectedCoins=0;
        PlayerPrefs.SetInt("Coins", allCoins);
    }

    public void resetCollectedCoins()
    {
        collectedCoins = 0;
    }

    public void resetFlip()
    {
        flipCount = 0;
    }

    public void resetPerfect()
    {
        perfectCount = 0;
    }

    public void resetAllCoins()
    {
        PlayerPrefs.SetInt("Coins", 0);
        allCoins= PlayerPrefs.GetInt("Coins");
    }

    public void scoreFind()
    {
        score = collectedCoins + flipCount + perfectCount;
    }

    public int getScore()
    {
        return score;
    }
    public int getCollectedCoins()
    {
        return collectedCoins;
    }
    public int getFlipCount()
    {
        return flipCount;
    }
    public int getPerfectCount()
    {
        return perfectCount;
    }

    public int getAllCoins()
    {
        return allCoins;
    }

    #endregion
}
