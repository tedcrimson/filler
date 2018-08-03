using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class MainUiController : MonoBehaviour {

    public Button StartButton;


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
	void Awake()
	{



        StartButton.onClick.AddListener(()=>{
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
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
}
