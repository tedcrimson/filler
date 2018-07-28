using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	public int bulletCount;

	private ShootController current;

	private ShootController bulletPrefab;


	// Use this for initialization
	void Start () {
		ShootController.OnHit += Pick;
		Pick(null);
	}

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		ShootController.OnHit -= Pick;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && bulletCount > 0)
		{
			current.Throw();
		}
	}

	void Pick(ShootController s)
	{
		current = Instantiate(bulletPrefab, this.transform);
		// bullets.RemoveAt(0);
	}

	public void SetHitObject(ShootController hito, int count)
	{
		bulletPrefab = hito;
		bulletCount = count;
	}


}
