using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<SkinData> skins;

    public SkinData CurrentSkin
    {
        get { return skins[Random.Range(0, skins.Count)]; }
    }
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
    }

    // Use this for initialization
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

}
