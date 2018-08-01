using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiController : MonoBehaviour {

	public Text HitInfo;
	public Text ScoreText;

	
	// Update is called once per frame
	public void UpdateHitInfo (State state) {

		HitInfo.text = state.ToString();
		
	}

	public void UpdateScore(int score)
	{
		ScoreText.text = score+"";
	}
}
