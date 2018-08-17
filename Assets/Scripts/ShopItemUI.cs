using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{

    public Image Icon;

    [Header("Select")]
    public Button SelectButton;
    public GameObject SelectedObject;

    [Header("Buy")]
    public Text Title;
    public Text Price;
    public Button BuyButton;

    [Header("Confirm")]
    public GameObject ConfirmPanel;
    public Button YesButton;
    public Button NoButton;

	private static ShopItemUI lastSelected;

    public static event System.Action OnItemPurchase;

    public void SetUI(ShopItem item)
    {
        Icon.sprite = item.ShopIcon;
        Title.text = item.name;
        Price.text = item.UnlockCoin + "";
		
		if(!item.Unlocked){
			BuyButton.gameObject.SetActive(true);
			SelectButton.gameObject.SetActive(false);
		}else
			SelectButton.gameObject.SetActive(true);

		SelectButton.onClick.AddListener(()=>{
			if(item is SkinData)
				GameManager.Instance.CurrentSkin = (SkinData)item;
			else if(item is BackgroundSkin)
				GameManager.Instance.CurrentBackgroundSkin = (BackgroundSkin)item;
			
			SetCurrent();
			
		});

        BuyButton.onClick.AddListener(() =>
        {
            ConfirmPanel.SetActive(true);
            BuyButton.gameObject.SetActive(false);
        });

        NoButton.onClick.AddListener(() =>
        {
            ConfirmPanel.SetActive(false);
            BuyButton.gameObject.SetActive(true);
        });

        YesButton.onClick.AddListener(() =>
        {
            if (item.CanUnlock(GameManager.Instance.GetCoins))
            {
                GameManager.Instance.UpdateCoin(-item.UnlockCoin);
                ConfirmPanel.SetActive(false);
                BuyButton.gameObject.SetActive(false);
				SelectButton.gameObject.SetActive(true);
				item.Unlocked = true;
            }
        });
    }

	public void SetCurrent()
	{
		SelectedObject.SetActive(true);
		if(lastSelected != null && lastSelected != this)
			lastSelected.SelectedObject.SetActive(false);
		lastSelected = this;

	}
}
