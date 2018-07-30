using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Skin", menuName="Skin",order = 0)]
public class SkinData : ScriptableObject {
	public GameObject MainObject;
	public GameObject TargetObject;
	public ShootController HitObject;
	public Color BackgroundColor;
	public Sprite BackgroundTexture;
	public float SpawnerPosY;
	public float HitPosY;

}
