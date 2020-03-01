using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeBase : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private TextMeshProUGUI percentUpgrade;
    public PlayerAttributes playerAttributes;
    [SerializeField] private int itemPrice; // Upgrade price
    [SerializeField] private int percentImprovement; // Upgrade % per buy

    public void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void UpdateInfo()
    {
        if (player == null) player = FindObjectOfType<PlayerController>();

        if (playerAttributes == PlayerAttributes.HP_MAX)
        {
            percentUpgrade.text = ((player.GetAttribute(playerAttributes)-100)/10) + "";
        }
        else
            percentUpgrade.text = player.GetAttribute(playerAttributes)+"";
    }

    public void OnClick()
    {
        Debug.Log("Try to buy upgrade: " + playerAttributes);
        if (player.GetItemAmount(InventoryItems.BLACK_CRISTALS) >= itemPrice)
        {
            Debug.Log("You bought upgrade: " + playerAttributes);
            player.AddItem(InventoryItems.BLACK_CRISTALS, -itemPrice);  // test
            player.AddAttribute(playerAttributes, percentImprovement);
            HeadPanelController.Instance.PlayUISFX(true);
        }
        else
        {
            Debug.Log("Not enought money: " + playerAttributes);
            Debug.Log("You have: " + player.GetItemAmount(InventoryItems.BLACK_CRISTALS) + " BC");
            HeadPanelController.Instance.PlayUISFX(false);
        }

        UpdateInfo();
    }
}
