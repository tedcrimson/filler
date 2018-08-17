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
    public Transform CoinParent;
    // public GameObject GameWinPanel;
    public CanvasGroup GameOverPanel;
    public CanvasGroup FadePanel;
    public CanvasGroup LevelPanel;
    public Text GameOverText;
    public Text ScoreText;
    public Text CoinText;

    [Header("New Skin Suggestion")] // ns = New Skin
    public GameObject ns_Panel;
    public Image ns_Background;
    public Text ns_Name;
    public GameObject ns_PricePanel;
    public Text ns_Price;
    public Button ns_BuyButton;
    public Button ns_UseButton;
    public GameObject ns_EnjoyText;
    [Header("Unlocked Skin Suggestion")] // us = Unlocked Skin
    public GameObject us_Panel;
    public Image us_Background;
    public Text us_Name;
    public Button us_UseButton;
    public GameObject us_EnjoyText;
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
    public GameObject CoinPrefab;

    
    #endregion

    private bool levelUpdated = false;




    // Use this for initialization
    IEnumerator Start()
    {

        LevelManager.OnGetCoin += UpdateCoin;
        LevelManager.OnAddCoin += AddCoin;
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
        LevelPanel.GetComponentInChildren<Text>().text = "Level " + (PrefsManager.LastLevel+1);

        yield return StartCoroutine(FadeIn(FadePanel, 2));
        LevelPanel.GetComponentInChildren<Animation>().Play();
        // yield return StartCoroutine(FadeOut(LevelPanel, 2));
        // yield return StartCoroutine(FadeIn(LevelPanel, 2));
        yield return new WaitForSeconds(1f);
        LevelManager.Instance.Enable();// = true;



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
        yield return StartCoroutine(FadeOut(FadePanel));
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    IEnumerator FadeOut(CanvasGroup panel, float speed = 1, float preWait = 0, float postWait = 0)
    {
        panel.alpha = 0f;
        panel.interactable = false;

        if(preWait > 0)
            yield return new WaitForSeconds(preWait);
        while(panel.alpha < 0.9)
        {
            panel.alpha += Time.deltaTime * speed;
            yield return null;
        }
        panel.alpha = 1f;
        if(postWait > 0)
            yield return new WaitForSeconds(postWait);
        panel.interactable = true;
    }
    IEnumerator FadeIn(CanvasGroup panel, float speed = 1)
    {
        panel.alpha = 1f;
        while(panel.alpha > 0.1f)
        {
            panel.alpha -= Time.deltaTime * speed;
            yield return null;
        }
        panel.alpha = 0f;
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
        LevelManager.OnGetCoin -= UpdateCoin;
        LevelManager.OnAddCoin -= AddCoin;
        LevelManager.OnChangeLevel -= ChangeLevel;
        LevelManager.OnUpdateScore -= UpdateScore;
        LevelManager.OnStateCheck -= HitRespond;

        // PlayerController.OnUpdateProgress -= UpdateProgress;
        // PlayerController.OnAddScore -= AddScore;
        LevelManager.OnCombo -= AddCombo;
        // PlayerController.OnClick -= ClickRespond;
        LevelManager.OnGameOver -= GameOver;

    }

    private void AddCoin(int addCoin)
    {
        var coinClone =Instantiate(CoinPrefab, CoinParent);
        coinClone.GetComponent<Text>().text = "+" + addCoin;
        Destroy(coinClone, 1f);
        
    }

    private void UpdateCoin(int TotalCoin)
    {
        CoinText.text = TotalCoin + "";
    }

    private void ChangeLevel(int level)
    {
        StartCoroutine(Reload());
        levelUpdated = true;
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
        ScoreText.gameObject.SetActive(false);
        GameOverPanel.gameObject.SetActive(true);
        StartCoroutine(FadeOut(GameOverPanel, 3, .5f));
        GameOverText.text = "Level " + (PrefsManager.LastLevel+1);

        if(levelUpdated && (PrefsManager.LastLevel + 1)%5 >= 0)
        {
            UnlockSkinSuggestion();
        }else
            NewSkinSuggestion();
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

    private void NewSkinSuggestion()
    {
        ShopItem shopItem = GameManager.Instance.skins.Find(x=>!x.Unlocked && x.CanUnlock(GameManager.Instance.GetCoins));
        if(shopItem == null) return;
        ns_Panel.SetActive(true);

        ns_Name.text = shopItem.name;
        ns_Background.sprite = shopItem.ShopIcon;
        ns_Price.text = shopItem.UnlockCoin +"";
        ns_BuyButton.onClick.AddListener(()=>{
            GameManager.Instance.UpdateCoin(-shopItem.UnlockCoin);
            ns_PricePanel.SetActive(false);
            ns_UseButton.gameObject.SetActive(true);
            shopItem.Unlocked = true;
        });

        ns_UseButton.onClick.AddListener(()=>{
            ns_UseButton.gameObject.SetActive(false);
            ns_EnjoyText.SetActive(true);

            if(shopItem is SkinData)
				GameManager.Instance.CurrentSkin = (SkinData)shopItem;
			else if(shopItem is BackgroundSkin)
				GameManager.Instance.CurrentBackgroundSkin = (BackgroundSkin)shopItem;
			
        });

    }


    private void UnlockSkinSuggestion()
    {
        ShopItem shopItem = GameManager.Instance.skins.Find(x=>!x.Unlocked);
        if(shopItem == null) return;
        us_Panel.SetActive(true);
        us_Name.text = shopItem.name;
        us_Background.sprite = shopItem.ShopIcon;

        us_UseButton.onClick.AddListener(()=>{
            us_UseButton.gameObject.SetActive(false);
            us_EnjoyText.SetActive(true);

            if(shopItem is SkinData)
				GameManager.Instance.CurrentSkin = (SkinData)shopItem;
			else if(shopItem is BackgroundSkin)
				GameManager.Instance.CurrentBackgroundSkin = (BackgroundSkin)shopItem;
			
        });

    }



}
