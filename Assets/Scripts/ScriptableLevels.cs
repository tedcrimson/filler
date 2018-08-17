using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Create")]
public class ScriptableLevels : ScriptableObject
{
    public List<Difficulty> difficulties;
	public LevelData GetLevelData(int lvl, ref float coef)
	{
		Difficulty current = difficulties[0];
		foreach(Difficulty d in difficulties)
			if (lvl >= d.lvlValue){
				current = d;
			}
		coef = current.sliceCoeficient;
		Debug.Log(current.name);
		return current.levelData[Random.Range(0, current.levelData.Count)];
	}
}

[System.Serializable]
public class Difficulty
{
	public string name;
	public int lvlValue;
	[Range(0f, 1f)]
	public float sliceCoeficient;
    public List<LevelData> levelData;
}

[System.Serializable]
public class LevelData
{
	public float minSpeed;
    public float maxSpeed;
	public float waitTime;
	public float timeScale = 1f;
    public bool canSlowDown;
    public bool canChangeDirection;
}
