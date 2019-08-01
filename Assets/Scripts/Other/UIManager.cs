using System.Collections;
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
    
    [Header("Loading Panel")]
    public GameObject loadPanel;
    
    [Header("GameOver Panel")]
    public GameObject gameOverPanel;
   
    [Header("GameStart Panel")]
    public GameObject gameStartPanel;
    
    
    [Header("LevelComplete")]
    public GameObject levelCompletePanel;

    [Header("RiskyText Text")]
    public GameObject riskyText;
    
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
    
    [Header("Restart Button ")]
    public GameObject restartButton;

    [Header("NextLevel Button ")]
    public GameObject NextLevel;
    
    [Header("LevelCompletePanel ")]
    [Header("Level Text ")]
    public TextMeshProUGUI levelText;
    
    [Header("Flip Text ")]
    public TextMeshProUGUI flipText;
    
    [Header("PerfectLevelEnd Text ")]
    public TextMeshProUGUI perfectTextCounter;
    
    [Header("Coin Text ")]
    public TextMeshProUGUI coinTextCounter;
    
    [Header("Score Text ")]
    public TextMeshProUGUI scoreText;
        
    [Header("CurrentLevelText Text ")]
    public TextMeshProUGUI CurrentLevelText;
    
    [Header("NextLevelText Text ")]
    public TextMeshProUGUI NextLevelText;

    public Image Bar;
    
    private float _currentPos;
    private float _endPosition;
    private float startPosition = -21.478f;
    private float _progress;
    
    public bool canClick;
    public bool canClickNextLevel;


    private void Start()
    {
    }

    private void Update()
    {
        CollectionUpdate();
        ShowCoinText();
    }


    public void CollectionUpdate()
    {
        flipText.text = CollectionManager.Instance.getFlipCount().ToString();
        perfectTextCounter.text = CollectionManager.Instance.getPerfectCount().ToString();
        coinTextCounter.text = CollectionManager.Instance.getCollectedCoins().ToString();
        scoreText.text = CollectionManager.Instance.getScore().ToString();
        levelText.text = StageManager.Instance.getCurrentLevel().ToString();
    }
    public void ShowLevelCompletePanel()
    {
        levelCompletePanel.SetActive(true);
    }
    
    public void HideLevelCompletePanel()
    {
        levelCompletePanel.SetActive(false);
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
        Invoke("CloseGameStart",0.8f);
    }
    
    public void CloseGameStart()
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
        coinText.text = GameManager.Instance.GetCoinCount().ToString();
    }
    
    public IEnumerator SetProgressBar()
    {
        _currentPos = Player.Instance.transform.position.x;
        Vector3 pos = Vector3.one;
        CurrentLevelText.text = StageManager.Instance.getCurrentLevel().ToString();
        NextLevelText.text = (StageManager.Instance.getCurrentLevel()+ 1).ToString();
        while (true)
        {
            pos.x = Remapper.Remap(_currentPos, startPosition, _endPosition, 0, 1);
            Bar.transform.localScale = pos;
            yield return new WaitForFixedUpdate();
        }
    }
    
    public IEnumerator ShowNextLevelButton()
    {
        if (!canClickNextLevel)
        {
            NextLevel.SetActive(false);
            canClickNextLevel = false;
            yield return new WaitForSeconds(3f);
            NextLevel.SetActive(true);
            canClickNextLevel = true;
        }
    }

    public IEnumerator ShowRestartButton()
    {
        canClick = false;
        restartButton.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
        canClick = true;
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
    public IEnumerator ShowRiskyText()
    {
        riskyText.SetActive(true);
        iTween.ShakeScale(riskyText,iTween.Hash("x", 0.2f, "time", 1f, "easetype", "easeInOutSine"));
        yield return new WaitForSeconds(1f);
        riskyText.SetActive(false);
    }
}

