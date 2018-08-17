using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class MainUiController : MonoBehaviour {

    public GameManager gm;

    public Button StartButton;

    public GameObject PlayPanel;

    public Text CoinText;
    public Text LevelText;
    public Image SkinContainer;

    public Color SelectedColor;
    public Color NotSelectedColor;

    [Header("Shop Menu")]
    public Button ShopButton;
    public Button CloseShopButton;
    public GameObject ShopPanel;
    public Button MainSkinButton;
    public GameObject MainSkinPanel;
    public Transform MainSkinContainer;
    public Button BackgroundSkinButton;
    public GameObject BackgroundSkinPanel;
    public Transform BackgroundSkinContainer;
    public GameObject ShopItemPrefab; 


    [Header("Bottom Panel")]
	public Button audioOnButton;
	public Button audioOffButton;
    public Button InstaButton;
    public Button FbButton;
    public Button RateButton;

	#region delegates_and_events
    public delegate void AudioToggle(bool toggle);
    public static event AudioToggle  OnAudioChange;

    #endregion


	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Start()
	{

        LevelText.text = "Level " + PrefsManager.LastLevel;
        StartButton.onClick.AddListener(()=>{
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
        });

        var mainSkins = GameManager.Instance.skins;
        var backgroundSkins = GameManager.Instance.backgroundSkins;

        for (int i = 0; i < mainSkins.Count; i++)
        {

            var clone = Instantiate(ShopItemPrefab, MainSkinContainer).GetComponent<ShopItemUI>();
            clone.SetUI(mainSkins[i]);
        }

        SkinContainer.sprite = mainSkins[PrefsManager.MainSkinIndex].SkinSprite;


        for (int i = 0; i < backgroundSkins.Count; i++)
        {

            var clone = Instantiate(ShopItemPrefab, BackgroundSkinContainer).GetComponent<ShopItemUI>();
            clone.SetUI(backgroundSkins[i]);
        }

        PerformMainSkinButton();
        MainSkinButton.onClick.AddListener(PerformMainSkinButton);

        BackgroundSkinButton.onClick.AddListener(()=>{
            BackgroundSkinPanel.SetActive(true);
            MainSkinPanel.SetActive(false);
            BackgroundSkinButton.image.color = SelectedColor;
            MainSkinButton.image.color = NotSelectedColor;
            BackgroundSkinContainer.GetChild(PrefsManager.BackgroundSkinIndex).GetComponent<ShopItemUI>().SetCurrent();
        });

        CloseShopButton.onClick.AddListener(()=>{
            ShopPanel.SetActive(false);
            PlayPanel.SetActive(true);
            SkinContainer.sprite = mainSkins[PrefsManager.MainSkinIndex].SkinSprite;

        });



        ShopButton.onClick.AddListener(()=>{
            ShopPanel.SetActive(true);
            PlayPanel.SetActive(false);
        });



        return;
		OnAudioChange(PrefsManager.Audio);
		

        audioOnButton.onClick.AddListener(() =>
        {
            OnAudioChange(false);
        });

        audioOffButton.onClick.AddListener(() =>
        {
            OnAudioChange(true);
        });

        InstaButton.onClick.AddListener(()=>Application.OpenURL("https://www.instagram.com/_u/nefstergames"));
        FbButton.onClick.AddListener(()=>Application.OpenURL("fb://page/NefsterEntertainment"));

		OnAudioChange += ToggleAudioButtons;

	}

    public void PerformMainSkinButton()
    {
        MainSkinPanel.SetActive(true);
        BackgroundSkinPanel.SetActive(false);
        MainSkinButton.image.color = SelectedColor;
        BackgroundSkinButton.image.color = NotSelectedColor;
        MainSkinContainer.GetChild(PrefsManager.MainSkinIndex).GetComponent<ShopItemUI>().SetCurrent();
    }

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		OnAudioChange -= ToggleAudioButtons;

	}


    void ToggleAudioButtons(bool b)
    {
        audioOnButton.gameObject.SetActive(b);
        audioOffButton.gameObject.SetActive(!b);
    }

    void Update()
    {
        CoinText.text = gm.GetCoins + "";
    }
}
