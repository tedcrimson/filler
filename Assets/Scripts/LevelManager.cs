using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum State
{
    BAD = -1, GOOD = 0, PERFECT = 1
}

[System.Serializable]
public class HitPoint
{
    public float angle;
    public GameObject obj;
    public bool hit;
    public SpriteMask mask;
    public HitPoint(GameObject obj, float angle)
    {
        this.obj = obj;
        this.angle = angle;
        this.hit = false;
        mask = obj.GetComponent<SpriteMask>();
    }

    public void Hit()
    {
        mask.enabled = false;
        hit = true;
    }
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

    public List<HitPoint> pointAngles;
    public int currentIndex;
    // public delegate void WheelChange(float angle);
    // public static event WheelChange OnWheelChange;
    private float randomRotation;
    private float time;

    private int dir;
    private float speed;

    public static State state;
    public static State lastState;

    private float offset;
    private LevelData lvl;

    int score;
    int combo = 0;

    public int scoreMultiplier = 1;

    public delegate void UpdateData(int data);
    public static event UpdateData OnUpdateScore;
    public static event UpdateData OnCombo;
    public static event UpdateData OnGameOver;
    public static event UpdateData OnChangeLevel;
    public static event UpdateData OnStateCheck;

    public static LevelManager Instance;

    private void Awake()
    {

        Instance = this;
        offset = 0.03f;
        dir = -1;
        lvl = GameManager.Instance.Data.GetLevelData(PrefsManager.LastLevel);
        SkinData currentSkin = GameManager.Instance.CurrentSkin;
        targetPrefab = currentSkin.TargetObject;
        MainObject = Instantiate(currentSkin.MainObject);
        player.SetHitObject(currentSkin.HitObject, 4);
        spawner.transform.position = new Vector2(0, currentSkin.SpawnerPosY);
        hitObject.position = new Vector2(0, currentSkin.HitPosY);
        Camera.main.backgroundColor = currentSkin.BackgroundColor;
        if (currentSkin.BackgroundTexture != null)
            backGroundImage.sprite = currentSkin.BackgroundTexture;

        state = State.PERFECT;
        ShootController.OnHit += HitDetect;



    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        ShootController.OnHit -= HitDetect;
    }

    void Start()
    {
        // Debug.Log(PlayerPrefs.GetInt("Highscore"));
        dir = Random.value > .5f ? 1 : -1;
        if (lvl.canSlowDown)
        {
            StartCoroutine(ChangeSpeed());
            speed = 0;
        }
        else
            speed = lvl.maxSpeed;
        pointAngles = new List<HitPoint>();
        var startPos = spawner.position;
        for (int i = 0; i < 18; i++)
        {
            if (Random.value > levelCoefficient)
            {
                var g = Instantiate(targetPrefab, spawner.position, spawner.rotation, MainObject.transform);

                pointAngles.Add(new HitPoint(g, 360 - spawner.eulerAngles.z));
            }
            spawner.RotateAround(Vector3.zero, Vector3.forward, 20);
        }

        spawner.position = startPos;
        spawner.rotation = Quaternion.identity;
    }

    void Update()
    {
        MainObject.transform.Rotate(Vector3.forward * dir, speed);


        // MainObject.transform.Rotate(Vector3.forward, dir * speed);
        float zone = Mathf.Abs(MainObject.transform.eulerAngles.z - pointAngles[currentIndex].angle);
        // Debug.Log(pointAngles[currentIndex].angle + " "  + MainObject.transform.eulerAngles.z + "  " + zone);
        if (zone <= 4)
        {
            if (!pointAngles[currentIndex].hit)
                state = State.PERFECT;
        }
        else if (zone <= 11)
        {
            if (!pointAngles[currentIndex].hit)
                state = State.GOOD;
        }
        else
        {
            if (state != State.BAD && CheckAllHit())
            {
                Win();
            }
            state = State.BAD;
        }
        // Debug.Log(state);

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
        Debug.LogError("Current Was " + currentIndex);

        do
        {
            MoveOnNext();
        }
        while (!allhit && pointAngles[currentIndex].hit);
        Debug.LogError("Next is " + currentIndex);

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


    void HitDetect(ShootController s)
    {
        if (state != State.BAD && !pointAngles[currentIndex].hit)
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
            s.FixPosition(pointAngles[currentIndex].obj);
            pointAngles[currentIndex].Hit();
            // MoveOnNext();            
            // state = State.BAD;
        }
        else
        {
            state = State.BAD;
            s.GravityOff();
            GameOver();
        }

        score += ((int)state + 1) * scoreMultiplier;
        OnUpdateScore(score);
        Debug.LogError(state);
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
        OnGameOver(score);
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

        Audio.AS.PlayOneShot(Audio.WinSound);
    }

    public void UpdateHighscore(int score)
    {
        if (PlayerPrefs.GetInt("Highscore") < score)
            PlayerPrefs.SetInt("Highscore", score);
    }
}
