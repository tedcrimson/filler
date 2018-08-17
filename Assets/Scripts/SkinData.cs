using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "Skin", order = 0)]
public class SkinData : ShopItem
{
    public Sprite SkinSprite;
    public GameObject MainObject;
    [Range(-0.1f,1f)]
    public float RareValue = -0.1f;
    public GameObject RareObject;
    public GameObject TargetObject;
    public ShootController HitObject;
    public Color BackgroundColor;
    public float SpawnerPosY;
    public float HitPosY;

	public float PerfectZone;
	public float GoodZone;
    public Gradient RandomGradient;

    public Color GetColor()
    {
        return RandomGradient.Evaluate(Random.value);
    }

    public GameObject InitSkin()
    {
        var r = Random.value;
        if(r >= RareValue &&  RareValue > 0)
        {
            return Instantiate(RareObject);
        }
        return Instantiate(MainObject);
    }

}
