using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public AudioSource soundEffectSource;

    public List<AudioClip> acceleratorSounds;
    public AudioClip WinSound;
    public AudioClip LoseSound;

    // Use this for initialization
    void Awake()
    {
        LevelManager.OnStateCheck += HitSound;
        // PlayerController.OnUpdateScore += WinOrLoseSound;
        // PlayerController.OnGameOver += GameOverSound;
        MainUiController.OnAudioChange += ToggleAudio;
    }

    private void ToggleAudio(bool i)
    {
        BackgroundMusic.enabled = i;
        soundEffectSource.enabled = i;
    }



    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        LevelManager.OnStateCheck -= HitSound;
        // PlayerController.OnUpdateScore -= WinOrLoseSound;
        // PlayerController.OnGameOver -= GameOverSound;
        MainUiController.OnAudioChange -= ToggleAudio;

    }

    public void HitSound(int i)
    {
        soundEffectSource.PlayOneShot(LevelManager.Instance.GetLevelAudio());
    }

    // public void WinOrLoseSound(int t)
    // {
    //     // Debug.Log("WATAATATA");

    //     // if (t >= dt.goalSpeed)
    //     // {
    //     //     soundEffectSource.Stop();
    //     //     soundEffectSource.PlayOneShot(WinSound);
    //     //     PlayerController.OnUpdateScore -= WinOrLoseSound;
    //     // }
    //     // else if (t <= dt.minSpeed)
    //     // {
    //     //     soundEffectSource.Stop();
    //     //     soundEffectSource.PlayOneShot(LoseSound);
    //     //     PlayerController.OnUpdateScore -= WinOrLoseSound;
    //     // }


    // }

    
    // private void GameOverSound(float score)
    // {
    //     soundEffectSource.PlayOneShot(LoseSound);
    //     // throw new NotImplementedException();
    // }


}
