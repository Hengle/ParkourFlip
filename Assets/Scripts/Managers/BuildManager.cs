﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    #region Variables
    public List<BuildingScript> buildingScripts;

    #endregion

    #region Functions
    void Start()
    {
        buildingScripts = new List<BuildingScript>();

        ControlBuildings();
    }

    public void ControlBuildings()
    {
        GameObject[] tempBuilds = GameObject.FindGameObjectsWithTag("Ground");

        buildingScripts.Clear();

        for (int i = 0; i < tempBuilds.Length; i++)
        {
            if (tempBuilds[i].GetComponent<BuildingScript>() != null && tempBuilds[i].activeInHierarchy)
            {
                buildingScripts.Add(tempBuilds[i].GetComponent<BuildingScript>());
            }
        }
    }

    public BuildingScript[] GetActiveBuilds()
    {
        ControlBuildings();
        return buildingScripts.ToArray();
    }
    #endregion
}
