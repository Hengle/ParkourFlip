﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
   
    [Header("GameOver Panel")]
    public GameObject gameOverPanel;
   
    [Header("GameStart Panel")]
    public GameObject gameStartPanel;
    
    
    [Header("LevelComplete")]
    public GameObject levelCompletePanel;

    [Header("Perfect Text")]
    public GameObject perfectText;
    
    [Header("Normal Text")]
    public GameObject normalText;
    
    [Header("NearMiss Text ")]
    public GameObject nearMissText;
    
    [Header("Combo Text ")]
    public GameObject comboText;
    public TextMeshProUGUI flip;
    
    [Header("Coin Text ")]
    public TextMeshProUGUI coinText;

    [Header("Win Text ")]
    public GameObject winText;
    

    private void Update()
    {
        ShowCoinText();
    }

    public void ShowLevelCompletePanel()
    {
        levelCompletePanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
   
    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
    public void HideGameStartPanel()
    {
        gameStartPanel.SetActive(false);
    }
    
    public void ShowGameStartPanel()
    {
        gameStartPanel.SetActive(true);
    }

    public void ShowWinText()
    {
        winText.SetActive(true);
    }
    public void HideWinText()
    {
        winText.SetActive(false);
    }

    public void ShowCoinText()
    {
        coinText.text = "Coin: " + GameManager.Instance.GetCoinCount().ToString();
    }
    public IEnumerator ShowComboText()
    {
        comboText.gameObject.SetActive(true);
        flip.text = "Flip " + GameManager.Instance.GetFlipComboCount().ToString();
        iTween.MoveFrom(comboText, iTween.Hash("y", comboText.transform.position.y - 20f,  "time", .5f, "easetype", "easeInOutElastic"));
        yield return new WaitForSeconds(.5f);
        comboText.SetActive(false);
    }
    
    public IEnumerator ShowPerfectText()
    {
        perfectText.gameObject.SetActive(true);
        iTween.MoveFrom(perfectText, iTween.Hash("y", perfectText.transform.position.y + 100f,  "time", 1f, "easetype", "easeOutBounce"));
        yield return new WaitForSeconds(1f);
        perfectText.SetActive(false);
    }
    public IEnumerator ShowNormalText()
    {
        normalText.SetActive(true);
        iTween.MoveFrom(normalText, iTween.Hash("y", normalText.transform.position.y + 100f,  "time", 1f, "easetype", "easeOutBounce"));
        yield return new WaitForSeconds(1f);
        normalText.SetActive(false);
    }
    public IEnumerator ShowNearMissText()
    {
        nearMissText.SetActive(true);
        iTween.MoveFrom(nearMissText, iTween.Hash("y", nearMissText.transform.position.y + 100f,  "time", 1f, "easetype", "easeOutBounce"));
        yield return new WaitForSeconds(1f);
        nearMissText.SetActive(false);
    }
    
}