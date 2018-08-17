using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum State
{
    BAD = -1, GOOD = 0, PERFECT = 1, WIN = 2
}

public class LevelManager : MonoBehaviour
{
    public PlayerController player;


    public AudioController Audio;
    public SpriteRenderer backGroundImage;
    [Range(0f, 1f)]
    public float levelCoefficient;
    public Transform spawner;
    public Transform hitObject;
    private GameObject MainObject;
    private GameObject targetPrefab;
    public GameObject coinPrefab;

    public List<HitPoint> pointAngles;
    public int currentIndex;
    // public delegate void WheelChange(float angle);
    // public static event WheelChange OnWheelChange;
    private float randomRotation;
    private float time;

    private int dir;
    private float speed;

    // public static State state;
    public static State lastState;

    private float offset;
    private LevelData lvl;

    public Color currentColor;

    private SkinData currentSkin;
    private BackgroundSkin backgroundSkin;

    int score;
    int combo = 0;

    public int scoreMultiplier = 1;

    public delegate void UpdateData(int data);
    public static event UpdateData OnUpdateScore;
    public static event UpdateData OnCombo;
    public static event UpdateData OnGameOver;
    public static event UpdateData OnChangeLevel;
    public static event UpdateData OnStateCheck;
    public static event UpdateData OnGetCoin;
    public static event UpdateData OnAddCoin;
    public static LevelManager Instance;

    private void Awake()
    {

        Instance = this;
        // levelCoefficient = Mathf.Min(0.02285714f * PrefsManager.LastLevel, .8f);
        offset = 0.03f;
        dir = -1;
        lvl = GameManager.Instance.Data.GetLevelData(PrefsManager.LastLevel, ref levelCoefficient);
        currentSkin = GameManager.Instance.CurrentSkin;
        backgroundSkin = GameManager.Instance.CurrentBackgroundSkin;
        

        spawner.transform.position = new Vector2(0, currentSkin.SpawnerPosY);
        hitObject.position = new Vector2(0, currentSkin.HitPosY);
        Camera.main.backgroundColor = currentSkin.BackgroundColor;

        if (backgroundSkin.BackgroundTexture != null)
            backGroundImage.sprite = backgroundSkin.BackgroundTexture;



        // state = State.PERFECT;
        ShootController.OnHit += HitDetect;




    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        GetCoin(0);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        ShootController.OnHit -= HitDetect;

    }

    public void Enable()
    {
        player.enabled = true;
        MainObject = currentSkin.InitSkin();
        // MainObject.transform.localScale = Vector2.zero;
        targetPrefab = currentSkin.TargetObject;
        player.SetHitObject(currentSkin.HitObject, 4);

        currentColor = currentSkin.GetColor();

        MainObject.transform.Find("Main").GetComponent<SpriteRenderer>().color = currentColor;

        dir = Random.value > .5f ? 1 : -1;
        if (lvl.canSlowDown)
        {
            StartCoroutine(ChangeSpeed());
            speed = 0;
        }
        else
            speed = lvl.maxSpeed;
        pointAngles = new List<HitPoint>();

        var coinCount = 2f;
        var startPos = spawner.position;
        for (int i = 0; i < 18; i++)
        {
            if (Random.value < levelCoefficient)
            {
                var g = Instantiate(targetPrefab, spawner.position, spawner.rotation, MainObject.transform);
                var hito = new HitPoint(g, 360 - spawner.eulerAngles.z);

                GameObject coin = null;
                if(coinCount > 0 && Random.value > .7f)
                {
                    coinCount --;
                    var coinObj = Instantiate(coinPrefab, spawner.position - spawner.transform.up/2f, spawner.rotation, MainObject.transform);
                    // coinObj.transform.localScale = Vector3.one * 2;
                    hito.SetCoin(coinObj);
                }

                pointAngles.Add(hito);
            }
            spawner.RotateAround(Vector3.zero, Vector3.forward, 20);
        }

        spawner.position = startPos;
        spawner.rotation = Quaternion.identity;

        score = PrefsManager.CurrentScore;
        OnUpdateScore(score);



    }

    void Update()
    {
        if(MainObject)
            MainObject.transform.Rotate(Vector3.forward * dir, speed);
    }


    IEnumerator ChangeSpeed()
    {
        while (true)
        {
            Debug.Log("Reset");
            while (speed > offset + lvl.minSpeed)
            {
                speed = Mathf.Lerp(speed, lvl.minSpeed, Time.deltaTime * lvl.timeScale);
                yield return 0;
            }
            if (lvl.canChangeDirection)
            {
                Debug.Log("SlowDowned");
                dir = Random.value > .5f ? 1 : -1;
            }
            while (speed < lvl.maxSpeed - offset)
            {
                speed = Mathf.Lerp(speed, lvl.maxSpeed, Time.deltaTime * lvl.timeScale);
                yield return 0;
            }
            Debug.Log("SpeedUped");
            speed = lvl.maxSpeed;
            yield return new WaitForSeconds(lvl.waitTime);
        }
    }

    public bool CheckAllHit()
    {
        bool allhit = pointAngles.TrueForAll(x => x.hit);
        // Debug.LogError("Current Was " + currentIndex);

        do
        {
            MoveOnNext();
        }
        while (!allhit && pointAngles[currentIndex].hit);
        // Debug.LogError("Next is " + currentIndex);

        return allhit;
    }

    void MoveOnNext()
    {
        currentIndex = currentIndex - dir;
        if (currentIndex == pointAngles.Count)
            currentIndex = 0;
        if (currentIndex < 0)
            currentIndex = pointAngles.Count - 1;
    }


    HitPoint GetState(out State s)
    {
        var angle = MainObject.transform.eulerAngles.z;
        angle = angle % 360;
        if (angle < 0)
            angle += 360;
        
        foreach (var item in pointAngles)
        {
            // float zone = Mathf.Abs(MainObject.transform.eulerAngles.z - item.angle);

            float zone = Mathf.Abs(angle - item.angle);
            if(zone + currentSkin.GoodZone > 360){
                zone = (zone + currentSkin.GoodZone) % 360;
                Debug.LogWarning("Opa");
            }

            if (zone <= currentSkin.PerfectZone)
            {
                if (!item.hit)
                {
                    s = State.PERFECT;
                    return item;
                }
            }
            else if (zone <= currentSkin.GoodZone)
            {
                if (!item.hit)
                {
                    s = State.GOOD;
                    return item;
                }
            }

        }

        s = State.BAD;
        return null;
    }


    void HitDetect(ShootController s)
    {
        State state;
        var hitPoint = GetState(out state);


        score += ((int)state + 1) * scoreMultiplier;
        OnUpdateScore(score);

        if (hitPoint != null)
        {
            if (lastState == State.PERFECT && state == State.PERFECT)
            {
                combo++;
                OnCombo(combo);
            }
            else
                combo = 0;

            lastState = state;
            // Debug.Log("Hit" + state);
            s.FixPosition(hitPoint.obj);
            hitPoint.Hit();

            if(hitPoint.coin)
            {
                GetCoin(lastState == State.PERFECT? 2 : 1);
            }

            if (pointAngles.TrueForAll(x => x.hit))
            {
                Win();
                // return;

            }

            // MoveOnNext();            
            // state = State.BAD;
        }
        else
        {
            Debug.LogError("NINGGG");
            state = State.BAD;
            s.GravityOff();
            GameOver();
        }

        
        OnStateCheck((int)state);

        UpdateHighscore(score);
    }
    public void RandomGenerator()
    {
        dir = Random.value > .5f ? 1 : -1;
        speed = Random.Range(1f, 1.2f);
        return;
    }

    public void GameOver()
    {
        // UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        PrefsManager.CurrentScore = 0;
        dir = 0;
        MainObject.AddComponent<Shaker>().Shake();
        // StartCoroutine(Shake());
        OnGameOver(score);
    }


    IEnumerator Shake()
    {
        float dur = 0;
        float d = 1f;
        while(dur < 1f)
        {
            MainObject.transform.Rotate(Vector3.forward * d, speed * 2);
            dur += 0.1f;
            d = -d;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public AudioClip GetLevelAudio()
    {
        return null;
    }

    public void Win()
    {
        MainObject.GetComponent<Animator>().SetTrigger("Hide");

        // UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        // PrefsManager.LastLevel ++;
        OnChangeLevel(++PrefsManager.LastLevel);
        PrefsManager.CurrentScore = score;

        Audio.AS.PlayOneShot(Audio.WinSound);
    }

    public void UpdateHighscore(int score)
    {
        if (PrefsManager.HighScore < score)
            PrefsManager.HighScore =  score;
    }

    public void GetCoin(int coin)
    {
        OnGetCoin(GameManager.Instance.UpdateCoin(coin));
        if(coin > 0)
            OnAddCoin(coin);
    }
}
