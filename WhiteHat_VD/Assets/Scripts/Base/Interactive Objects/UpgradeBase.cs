using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeBase : MonoBehaviour
{
    private int actualPercentUpgrade = 0;
    private int hpPercentUpgrade = 0;
    private int luckPercentUpgrade = 0;
    private int magazineCapacityPercentUpgrade = 0;
    private int additionalDamagePercentUpgrade = 0;

    private InventoryItems InventoryItems;

    private PlayerController player;
    [SerializeField]
    private TextMeshProUGUI percentUpgrade;
    [SerializeField]
    private PlayerAttributes PlayerAttributes;
    [SerializeField]
    private int itemPrice; // Upgrade price
    [SerializeField]
    private int percentImprovement; // Upgrade % per buy

    public void Start()
    {
        player = FindObjectOfType<PlayerController>();
        percentUpgrade.text = actualPercentUpgrade.ToString() + "%";
    }

    public void OnClick()
    {
        Debug.Log("Try to buy upgrade: " + PlayerAttributes);
        if (player.GetItemAmount(InventoryItems.BLACK_CRISTALS) >= itemPrice)
        {
            Debug.Log("You bought upgrade: " + PlayerAttributes);
            player.AddItem(InventoryItems.BLACK_CRISTALS, -itemPrice);  // test
            player.AddAttribute(PlayerAttributes, percentImprovement);
            actualPercentUpgrade = actualPercentUpgrade + percentImprovement;
            percentUpgrade.text = actualPercentUpgrade.ToString() + "%";
        }
        else
        {
            Debug.Log("Not enought money: " + PlayerAttributes);
            Debug.Log("You have: " + player.GetItemAmount(InventoryItems.BLACK_CRISTALS) + " BC");
        }
    }
}
