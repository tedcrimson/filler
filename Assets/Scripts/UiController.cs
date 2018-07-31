using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiController : MonoBehaviour {

	public Text HitInfo;

	
	// Update is called once per frame
	public void UpdateHitInfo (State state) {

		HitInfo.text = state.ToString();
		
	}
}
