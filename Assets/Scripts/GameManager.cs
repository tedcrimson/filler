using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int coins;
    public ScriptableLevels Data;
    public List<SkinData> skins;
    public List<BackgroundSkin> backgroundSkins;

    private int currentSkinIndex;
    private int currentBackgroundIndex;

    public SkinData CurrentSkin
    {
        get { return skins[currentSkinIndex]; }
        set
        {
            for (int i = 0; i < skins.Count; i++)
            {
                if (skins[i] == value)
                {
                    currentSkinIndex = i;
                    PrefsManager.MainSkinIndex = currentSkinIndex;
                    break;
                }
            }
        }
    }

    public BackgroundSkin CurrentBackgroundSkin
    {
        get { return backgroundSkins[currentBackgroundIndex]; }
        set
        {
            for (int i = 0; i < backgroundSkins.Count; i++)
            {
                if (backgroundSkins[i] == value)
                {
                    currentBackgroundIndex = i;
                    PrefsManager.BackgroundSkinIndex = currentBackgroundIndex;
                    break;
                }
            }
        }
    }

    public int GetCoins { get { return coins; } }

    public static GameManager Instance;



    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(this);

        coins = PrefsManager.Coins;
        currentSkinIndex = PrefsManager.MainSkinIndex;
        currentBackgroundIndex = PrefsManager.BackgroundSkinIndex;
        UpdateCoin(0);
    }

    // Use this for initialization
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public int UpdateCoin(int c)
    {
        coins += c;
        PrefsManager.Coins = coins;
        return coins;
    }

}
