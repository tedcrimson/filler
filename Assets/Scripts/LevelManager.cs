using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject prefab;
	public Transform spawner;
	public GameObject MainObject;

	// Use this for initialization
	void Start () {
		var startPos = spawner.position;

		for (int i = 0; i < 10; i++)
		{
			spawner.RotateAround(Vector3.zero, Vector3.forward, 30);
			Instantiate(prefab, spawner.position, spawner.rotation);
		}
		spawner.position = startPos;
		spawner.rotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
		MainObject.transform.Rotate(Vector3.forward, 1);
	}
}
