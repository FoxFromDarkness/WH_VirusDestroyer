using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAmountTypeBase : MonoBehaviour
{
    private PlayerController player;

    [SerializeField]
    private ShopItemBase shopItem;

    [SerializeField]
    private InventoryItems InventoryItems;

    [SerializeField]
    private int itemPrice;
    [SerializeField]
    private int itemAmount;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void OnClick()
    {
        if (player.GetItemAmount(InventoryItems.BLACK_CRISTALS) > shopItem.itemPrice)
        {
            player.AddItem(InventoryItems.BLACK_CRISTALS, -itemPrice);
            switch (InventoryItems)
            {
                case InventoryItems.AMMO_TYPE_1:
                    player.AddItem(InventoryItems.AMMO_TYPE_1, +itemAmount);
                    break;
                case InventoryItems.AMMO_TYPE_2:
                    player.AddItem(InventoryItems.AMMO_TYPE_2, +itemAmount);
                    break;
                case InventoryItems.AMMO_TYPE_3:
                    player.AddItem(InventoryItems.AMMO_TYPE_3, +itemAmount);
                    break;
                case InventoryItems.AMMO_TYPE_4:
                    player.AddItem(InventoryItems.AMMO_TYPE_4, +itemAmount);
                    break;
            }
        }
    }
}
