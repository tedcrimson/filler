using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Range(22, 100)]
    public int levelCoefficient;
    public GameObject prefab;
    public Transform spawner;
    public GameObject MainObject;

    private float randomRotation;
	private float time;

	private int dir;
	private float speed;

    private void Awake()
    {
        RandomGenerator();
		InvokeRepeating("RandomGenerator", 0, Random.Range(1f, 2f));
    }

    void Start()
    {
        int randomAngleSum = 0;
        var startPos = spawner.position;
        while (randomAngleSum < 340)
        {
            var randomAngle = Random.Range(22, levelCoefficient);
            randomAngleSum += randomAngle;
            Instantiate(prefab, spawner.position, spawner.rotation, MainObject.transform);

            spawner.RotateAround(Vector3.zero, Vector3.forward, randomAngle);
        }
        spawner.position = startPos;
        spawner.rotation = Quaternion.identity;
    }

    void Update()
    {
		MainObject.transform.Rotate(Vector3.forward, dir * speed);

    }

    // IEnumerator RandomGeneratorOne(int dir)
    // {
    //     while (time < 140)
    //     {
	// 		time++;
    //         MainObject.transform.Rotate(Vector3.forward, dir);
    //         yield return new WaitForEndOfFrame();
    //     }
    //     time = 0;
    //     RandomGenerator();
    // }


    public void RandomGenerator()
    {
		dir = Random.value > .5f ? 1 : -1;
		speed = Random.Range(0f, 2f);
		return;
	}
    //     randomRotation = Random.Range(0f, 2f);
	// 	Debug.Log("Random num - " + randomRotation);
    //     if (randomRotation < 1)
    //     {
    //         StartCoroutine(RandomGeneratorOne(1));
			
    //         // StartCoroutine(RandomGenerator());
    //     }
    //     else
    //     {
    //         StartCoroutine(RandomGeneratorOne(-1));
    //         // StartCoroutine(RandomGeneratorOne());
    //     }
    // }
}
