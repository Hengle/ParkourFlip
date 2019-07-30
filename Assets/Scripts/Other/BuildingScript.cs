using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildingScript : MonoBehaviour
{
    public bool isPlayerOn;
    public Transform target;

    public GameObject trejectorCoins;
    public GameObject stableCoins;
    private int trejectorCoinChange;
    private int stableCoinChange;

    private void Start()
    {
        coinChange();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerOn = true;
          
        }
    }

    private void coinChange()
    {
        trejectorCoinChange = Random.Range(1, 4);

        if (trejectorCoinChange == 1)
        {
            trejectorCoins.SetActive(true);
        }
        else
        {
            trejectorCoins.SetActive(false);
        }
        
        stableCoinChange = Random.Range(1, 3);

        if (trejectorCoinChange == 1)
        {
            stableCoins.SetActive(true);
        }
        else
        {
            stableCoins.SetActive(false);
        }
    }
}
