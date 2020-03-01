using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDataBase : MonoBehaviour
{
    public static ItemsDataBase Instance;
    [SerializeField] private Sprite[] weaponSprites;
    [SerializeField] private Sprite[] ammoSprites;
    [SerializeField] private Sprite defaultSprite;


    private void Start()
    {
        if (Instance == null)
            Instance = FindObjectOfType<ItemsDataBase>();
    }

    public Sprite GetWeaponSpriteByItemType(InventoryItems inventoryItems)
    {
        switch (inventoryItems)
        {
            case InventoryItems.AMMO_TYPE_1:
                return weaponSprites[0];
            case InventoryItems.AMMO_TYPE_2:
                return weaponSprites[1];
            case InventoryItems.AMMO_TYPE_3:
                return weaponSprites[2];
            case InventoryItems.AMMO_TYPE_4:
                return weaponSprites[3];
            default:
                return defaultSprite;
        }
    }

    public Sprite GetAmmoSpriteByItemType(InventoryItems inventoryItems)
    {
        switch (inventoryItems)
        {
            case InventoryItems.AMMO_TYPE_1:
                return ammoSprites[0];
            case InventoryItems.AMMO_TYPE_2:
                return ammoSprites[1];
            case InventoryItems.AMMO_TYPE_3:
                return ammoSprites[2];
            case InventoryItems.AMMO_TYPE_4:
                return ammoSprites[3];
            default:
                return defaultSprite;
        }
    }
}
