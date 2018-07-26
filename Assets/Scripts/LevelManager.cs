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
        MainObject.transform.Rotate(Vector3.forward, 1);
    }
}
