using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HitPoint
{
    public float angle;
    public GameObject obj;
    public bool hit;
    public SpriteMask mask;
	public GameObject coin;

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
		if(coin != null)
			coin.SetActive(false);
    }
	public void SetCoin(GameObject coin)
	{
		this.coin = coin;
	}
}