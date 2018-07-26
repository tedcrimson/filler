using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public HitPoint(GameObject obj, float angle)
	{
		this.obj = obj;
		this.angle = angle;
		this.hit = false;
	}

	public void Hit()
	{
		obj.SetActive(false);
		hit = true;
	}
}
public class LevelManager : MonoBehaviour
{
    [Range(20, 100)]
    public int levelCoefficient;
    public GameObject prefab;
    public Transform spawner;
    public GameObject MainObject;

    public List<HitPoint> pointAngles;
    public int currentIndex;
    // public delegate void WheelChange(float angle);
    // public static event WheelChange OnWheelChange;
    private float randomRotation;
    private float time;

    private int dir;
    private float speed;

    private State state;

    private void Awake()
    {
		state = State.PERFECT;
		// currentIndex = -1;
		// CheckAllHit();
        RandomGenerator();
        InvokeRepeating("RandomGenerator", 0, Random.Range(1f, 2f));
		PlayerController.OnClick += HitDetect;
    }

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		PlayerController.OnClick -= HitDetect;
		
	}

    void Start()
    {
        pointAngles = new List<HitPoint>();
        int randomAngleSum = 0;
        var startPos = spawner.position;
        while (randomAngleSum < 340)
        {
            var randomAngle = Random.Range(20, levelCoefficient);
            randomAngleSum += randomAngle;
            var g = Instantiate(prefab, spawner.position, spawner.rotation, MainObject.transform);

            pointAngles.Add(new HitPoint(g, 360 - spawner.eulerAngles.z));
            spawner.RotateAround(Vector3.zero, Vector3.forward, randomAngle);
			
            // Debug.Log(randomAngle);
        }
        spawner.position = startPos;
        spawner.rotation = Quaternion.identity;
    }

    void Update()
    {
        MainObject.transform.Rotate(Vector3.forward, -1f*speed);
        // MainObject.transform.Rotate(Vector3.forward, dir * speed);
        float zone = Mathf.Abs(MainObject.transform.eulerAngles.z - pointAngles[currentIndex].angle);
		// Debug.Log(pointAngles[currentIndex].angle + " "  + MainObject.transform.eulerAngles.z + "  " + zone);
        if (zone < 4)
        {
            state = State.PERFECT;
        }
        else if (zone < 11)
        {
            state = State.GOOD;
        }
        else
        {
            if (state != State.BAD && CheckAllHit())
            {
				UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
			state = State.BAD;
        }
        // Debug.Log(state);

    }

	public bool CheckAllHit()
	{
		bool allhit = pointAngles.TrueForAll(x=>x.hit);
		do{
			currentIndex++;
			if (currentIndex == pointAngles.Count)
				currentIndex = 0;
		}
		while(!allhit && pointAngles[currentIndex].hit);
		// Debug.LogError(currentIndex);
		return allhit;
	}


    void HitDetect()
    {
        if(state != State.BAD)
		{
			Debug.LogError(currentIndex);
			pointAngles[currentIndex].Hit();
			CheckAllHit();
		}
    }
    public void RandomGenerator()
    {
        dir = Random.value > .5f ? 1 : -1;
        speed = Random.Range(1f, 1.2f);
        return;
    }
}
