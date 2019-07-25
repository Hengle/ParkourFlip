using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    //public List<BuildingScript> BuildingsList = new List<BuildingScript>();
    public int combo;
    public Transform _nextTarget;
    public float gravity;
    public float height;
    public bool gameEnd;
    public int coinCount;

   /* public enum STATE
    {
        Play,
        Pause,
        Dead
    }
    
    public STATE state = STATE.Pause;
    void FixedUpdate()
    {
        switch (state)
        {
            case STATE.Play:

                break;
            case STATE.Pause:
                
                break;
            case STATE.Dead:
             //   RestartGame();
                break;
        }
        
    }*/


   private bool deadAnim;
    private void Update()
    {

        if (!gameEnd)
        {
            BuidingsControl();
        }
        else
        {
            NextLevel();
        }

        
        if (Player.Instance.isDead)
        {
            PlayerDead();
        }
    }

    private void BuidingsControl()
    {
        for (int i = 0; i < BuildManager.Instance.buildingScripts.Count; i++)
        {
            if (BuildManager.Instance.buildingScripts[i].isPlayerOn)
            {
                if (!Player.Instance.isDead)
                {
                    _nextTarget = BuildManager.Instance.buildingScripts[i+1].target;
                }
                else 
                {
                    BuildManager.Instance.buildingScripts[i].isPlayerOn = false;
                }
            }
        }
    }

    public void StartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UIManager.Instance.HideGameStartPanel();
            BuildManager.Instance.ControlBuildings();
            _nextTarget = BuildManager.Instance.buildingScripts[0].target;
        }
    }

    private void NextLevel()
    {
        Debug.Log("NextLevel");
        Player.Instance._Anim.SetBool("IsMoving" , false);
        UIManager.Instance.ShowLevelCompletePanel();
        UIManager.Instance.ShowWinText();
        
        if (Input.GetMouseButtonDown(0))
        {
            Player.Instance.PlayerReset();
            StageManager.Instance.LevelUp();
            BuildManager.Instance.ControlBuildings();
            gameEnd = false;
            UIManager.Instance.HideGameOverPanel();
            
        }
    }
    public void PlayerDead()
    {
        if (!deadAnim)
        {
            StartCoroutine(DeadAnim());
        }
        else
        {
            RestartSameLevel();
        }
    }
    private void RestartSameLevel()
    {
        if (Input.GetMouseButtonUp(0))
        {
            deadAnim = false;
            UIManager.Instance.HideGameOverPanel();
            Player.Instance.PlayerReset();
        }
    }
    public IEnumerator DeadAnim()
    {   
        StartCoroutine(ParticleManager.Instance.DeathEffects());
        Player.Instance._rb.useGravity = true;
        yield return new WaitForSeconds(.5f);
        UIManager.Instance.ShowGameOverPanel();
        Player.Instance._rb.useGravity = false;
        Player.Instance.gameObject.SetActive(false);
        deadAnim = true;
    }


    public int GetCoinCount()
    {
        return coinCount;
    }
    public int GetFlipComboCount()
    {
        return combo + 1;
    }

}
