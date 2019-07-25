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

    public List<BuildingScript> BuildingsList = new List<BuildingScript>();
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

   private void Start()
   {
       _nextTarget = BuildingsList[0].target;
   }

   private void Update()
    {
        
        if (!gameEnd)
        {
            BuidingsControl();
        }
        else
        {
            StartCoroutine("NextLevel");
        }

        if (Player.Instance.isDead)
        {
            PlayerDead();
        }
        
    }

    private void BuidingsControl()
    {
        for (int i = 0; i < BuildingsList.Count; i++)
        {
            if (BuildingsList[i].isPlayerOn)
            {
                if (!Player.Instance.isDead)
                {
                    _nextTarget = BuildingsList[i+1].target;
                }
            }
        }
    }

    public void StartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UIManager.Instance.HideGameStartPanel();
        }
    }
    private void RestartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
    public void PlayerDead()
    {
        StartCoroutine(DeadAnim());
    }

    public IEnumerator NextLevel()
    {
        Player.Instance._Anim.SetBool("IsMoving" , false);
        UIManager.Instance.ShowLevelCompletePanel();
        UIManager.Instance.ShowWinText();
        yield return new WaitForSeconds(1f);
        if (Input.GetMouseButtonDown(0))
        {
            UIManager.Instance.HideWinText();
            Scene scene = SceneManager.GetActiveScene();
            if (scene.buildIndex == 5)
            {
                SceneManager.LoadScene(scene.buildIndex - 4);
            }
            else
            {
                SceneManager.LoadScene(scene.buildIndex + 1);
            }
        }
    }
    
    public IEnumerator SameLevel()
    {
        UIManager.Instance.ShowLoseText();
        yield return new WaitForSeconds(1f);
        if (Input.GetMouseButtonDown(0))
        {
            UIManager.Instance.HideLoseText();
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex );
        }
    }
    public IEnumerator DeadAnim()
    {
        StartCoroutine(ParticleManager.Instance.DeathEffects());
        Player.Instance._rb.useGravity = true;
        Player.Instance._collider.enabled = !Player.Instance._collider.enabled;
        Player.Instance._rb.constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(1f);
        Player.Instance.gameObject.SetActive(false);
        UIManager.Instance.ShowGameOverPanel();
        RestartGame();
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
