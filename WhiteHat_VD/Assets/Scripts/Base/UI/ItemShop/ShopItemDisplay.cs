using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private ShopItemBase shopItem;

    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI priceText;
    [SerializeField]
    private Button btn;
    private bool isBought;

    void Start()
    {
        nameText.text = shopItem.itemName.ToString();
        priceText.text = shopItem.itemPrice + " BC";
        btn.GetComponent<Image>().sprite = shopItem.sprite;
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
        }
    }

}
