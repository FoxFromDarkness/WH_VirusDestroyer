using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour
{
    [SerializeField]
    private ShopItemBase shopItem;

    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI priceText;
    [SerializeField]
    private Button btn;

    void Start()
    {
        nameText.text = shopItem.itemName;
        priceText.text = shopItem.itemPrice + " BC";
        btn.onClick.AddListener(() => OnClick());
    }

    public void OnClick()
    {
        Debug.Log("Button  clicked!");
    }

}
