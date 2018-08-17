using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public abstract class ShopItem : ScriptableObject {

	public Sprite ShopIcon;
    public bool Unlocked;
    public int UnlockCoin;

    public bool CanUnlock(int coins)
    {
        return coins >= UnlockCoin;
    }

	// public void SetUI(Image iconImage, Text coinText){

	// }

}
