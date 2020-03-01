using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour
{
    private PlayerController player;

    [SerializeField]
    private ShopItemBase shopItem;

    [SerializeField]
    private InventoryItems InventoryItems;

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField]
    private Button btn;

    private bool isBought;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        nameText.text = shopItem.itemStringName.ToString();
        priceText.text = shopItem.itemPrice + " BitCoin";
        btn.onClick.AddListener(() => OnClick());
    }

    public void OnClick()
    {
        if(!isBought && player.GetItemAmount(InventoryItems.BLACK_CRISTALS) >= shopItem.itemPrice)
        {
            player.AddItem(InventoryItems.BLACK_CRISTALS, -shopItem.itemPrice);
            player.BuyItem(shopItem.itemName, shopItem.sprite);
            isBought = true;
            btn.GetComponent<Image>().color = Color.grey;
            HeadPanelController.Instance.PlayUISFX(true);
        }
        else
            HeadPanelController.Instance.PlayUISFX(false);
    }

    public void SetBoughtItem()
    {
        isBought = true;
        btn.GetComponent<Image>().color = Color.grey;
    }

    public void ClearBoughtItem()
    {
        isBought = false;
        btn.GetComponent<Image>().color = Color.white;
    }
}
