using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField]
    private List<Coin> _coins;
    public void FindCoinsInLevel()
    {
        GameObject[] tempCoins = GameObject.FindGameObjectsWithTag("Coin");

        _coins.Clear();
        
        for (int i = 0; i < tempCoins.Length; i++)
        {
            if (tempCoins[i].GetComponent<Coin>() != null && tempCoins[i].activeInHierarchy)
            {
                _coins.Add(tempCoins[i].GetComponent<Coin>());
            }
        }
    }

    public void Activate()
    {
        foreach (var coin in _coins)
        {
            
        }
    }
    
    
}
