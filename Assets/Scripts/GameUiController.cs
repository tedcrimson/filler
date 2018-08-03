using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameUiController : MonoBehaviour
{

    #region Public_Fields
    
    [Header("Panels")]
    public Transform ScoreParent;
    public Transform ComboParent;
    // public GameObject GameWinPanel;
    public GameObject GameOverPanel;
    public Text GameOverText;
    public Text ScoreText;

    // public Transform SkinContainer;
    // public GameObject SkinPrefab;
    // public Button SkinsButton;

    [Space]
    // public Button WinNextButton;
    // public Button WinMainButton;
    public Button LoseRestartButton;
    public Button LoseMainButton;

    public GameObject ComboPrefab;
    public GameObject ScorePrefab;

    
    #endregion




    // Use this for initialization
    void Awake()
    {

        LevelManager.OnUpdateScore += UpdateScore;
        // PlayerController.OnUpdateProgress += UpdateProgress;
        // PlayerController.OnClick += ClickRespond;
        LevelManager.OnGameOver += GameOver;
        LevelManager.OnChangeLevel += ChangeLevel;
        // ShootController.OnHit += HitDetect;

        // PlayerController.OnAddScore += AddScore;
        LevelManager.OnCombo += AddCombo;

        LoseMainButton.onClick.AddListener(LoadMenu);

        LoseRestartButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        });

        // RateButton.onClick.AddListener(()=>Application.OpenURL("https://play.google.com/store/apps/details?id=com.nefster.meatbusters"));


        // var skins = GameManager.Instance.SkinsFile.skins;
        // int currentSkin = GameManager.Instance.CurrentSkinIndex;
        // for (int i = 0; i < skins.Count; i++)
        // {
        //     var skinClone = Instantiate(SkinPrefab, SkinContainer);
        //     skinClone.GetComponent<SkinUI>().UpdateData(skins[i], i, currentSkin == i);
        // }

    }



    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
    }


    public void LoadMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }


    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        LevelManager.OnChangeLevel -= ChangeLevel;
        LevelManager.OnUpdateScore -= UpdateScore;
        // PlayerController.OnUpdateProgress -= UpdateProgress;
        // PlayerController.OnAddScore -= AddScore;
        LevelManager.OnCombo -= AddCombo;
        // PlayerController.OnClick -= ClickRespond;
        LevelManager.OnGameOver -= GameOver;

    }

    private void ChangeLevel(int level)
    {
        // int curLev = GameManager.Instance.CurrentLevelIndex + 1;
    }

    private void UpdateScore(int t)
    {
        // distanceText.text = Methods.AngularSpeed(6378,t)+"";
        ScoreText.text = t + "";

        // if(t >= dt.goalSpeed)
        // {
        //     GameWinPanel.SetActive(true);
        // }else 

        // if(t <= dt.minSpeed)
        //     GameOverPanel.SetActive(true);
    }

    private void GameOver(int score)
    {
        GameOverPanel.SetActive(true);
        LevelManager.OnUpdateScore -= UpdateScore;

    }

    // private void AddScore(int score)
    // {
    //     var s = Instantiate(ScorePrefab, Canvas);
    //     s.GetComponent<Text>().text = score + "";
    //     Destroy(s, 2);

    // }

    private void HitRespond(State i)
    {
        if (i == State.BAD)return;

        var s = Instantiate(ScorePrefab, ScoreParent);
        s.GetComponent<Text>().text = i + "";
        Destroy(s, 2);

    }

    private void AddCombo(int combo)
    {
        var c = Instantiate(ComboPrefab, ComboParent);
        c.GetComponent<Text>().text = "x" + combo;
        Destroy(c, 2);
    }



}
