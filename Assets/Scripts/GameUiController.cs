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
    public CanvasGroup FadePanel;
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
    void Start()
    {

        LevelManager.OnUpdateScore += UpdateScore;
        // PlayerController.OnUpdateProgress += UpdateProgress;
        // PlayerController.OnClick += ClickRespond;
        LevelManager.OnGameOver += GameOver;
        LevelManager.OnChangeLevel += ChangeLevel;
        LevelManager.OnStateCheck += HitRespond;

        // PlayerController.OnAddScore += AddScore;
        LevelManager.OnCombo += AddCombo;

        LoseMainButton.onClick.AddListener(LoadMenu);

        LoseRestartButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        });
        StartCoroutine(FadeIn());

        // RateButton.onClick.AddListener(()=>Application.OpenURL("https://play.google.com/store/apps/details?id=com.nefster.meatbusters"));


        // var skins = GameManager.Instance.SkinsFile.skins;
        // int currentSkin = GameManager.Instance.CurrentSkinIndex;
        // for (int i = 0; i < skins.Count; i++)
        // {
        //     var skinClone = Instantiate(SkinPrefab, SkinContainer);
        //     skinClone.GetComponent<SkinUI>().UpdateData(skins[i], i, currentSkin == i);
        // }

    }


    IEnumerator Reload()//fade out
    {
        yield return StartCoroutine(FadeOut());
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    IEnumerator FadeOut()
    {
        while(FadePanel.alpha < 0.9)
        {
            FadePanel.alpha += Time.deltaTime;
            yield return null;
        }
        FadePanel.alpha = 1f;
    }
    IEnumerator FadeIn()
    {
        FadePanel.alpha = 1f;
        while(FadePanel.alpha > 0.1f)
        {
            FadePanel.alpha -= Time.deltaTime ;
            yield return null;
        }
        FadePanel.alpha = 0f;
    }


    public void LoadMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        LevelManager.OnChangeLevel -= ChangeLevel;
        LevelManager.OnUpdateScore -= UpdateScore;
        LevelManager.OnStateCheck -= HitRespond;

        // PlayerController.OnUpdateProgress -= UpdateProgress;
        // PlayerController.OnAddScore -= AddScore;
        LevelManager.OnCombo -= AddCombo;
        // PlayerController.OnClick -= ClickRespond;
        LevelManager.OnGameOver -= GameOver;

    }

    private void ChangeLevel(int level)
    {
        StartCoroutine(Reload());
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
        FadePanel.interactable=false;
        FadePanel.blocksRaycasts=false;
        GameOverPanel.SetActive(true);
        // LevelManager.OnUpdateScore -= UpdateScore;

    }

    // private void AddScore(int score)
    // {
    //     var s = Instantiate(ScorePrefab, Canvas);
    //     s.GetComponent<Text>().text = score + "";
    //     Destroy(s, 2);

    // }

    private void HitRespond(int state)
    {
        State i = (State)state;
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
