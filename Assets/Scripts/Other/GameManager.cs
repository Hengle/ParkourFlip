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
    private int _resetCount;

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


   private bool _deadAnim;
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
            _nextTarget = BuildManager.Instance.buildingScripts[0].target;
        }
    }

    private void NextLevel()
    {
        UIManager.Instance.ShowLevelCompletePanel();
        UIManager.Instance.ShowWinText();
        
        if (Input.GetMouseButtonDown(0))
        {
            ResetsCollection();
            CollectionManager.Instance.levelOver();
            StageManager.Instance.LevelUp();
            Player.Instance.PlayerReset();
            UIManager.Instance.HideLevelCompletePanel();
            gameEnd = false;
        }
    }
    public void PlayerDead()
    {
        if (!_deadAnim)
        {
            StartCoroutine(DeadAnim());
        }
        else 
        {
            RestartSameLevel();
        }
    }
    public void RestartSameLevel()
    {
        if (Input.GetMouseButtonDown(0) && UIManager.Instance.canClick)
        {
            _deadAnim = false;
            UIManager.Instance.canClick = false;
            UIManager.Instance.HideGameOverPanel();
            Player.Instance.PlayerReset();
        }
    }

    private void ResetsCollection()
    {
        CollectionManager.Instance.resetFlip();
        CollectionManager.Instance.resetPerfect();
        CollectionManager.Instance.resetCollectedCoins();
    }
    public IEnumerator DeadAnim()
    {
        ResetsCollection();
        GetCoinCountZero();
        UIManager.Instance.HideGameStartPanel();
        StartCoroutine(ParticleManager.Instance.DeathEffects());
        Player.Instance._rb.useGravity = true;
        yield return new WaitForSeconds(1f);
        UIManager.Instance.ShowGameOverPanel();
        StartCoroutine(UIManager.Instance.ShowRestartButton());
        Player.Instance._rb.useGravity = false;
        Player.Instance.gameObject.SetActive(false);
        _deadAnim = true;
    }
    public int GetCoinCount()
    {
        return coinCount;
    }

    public int GetCoinCountZero()
    {
        return coinCount = 0;
    }
    public int GetFlipComboCount()
    {
        return combo + 1;
    }

}
