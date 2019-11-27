using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDataBase : MonoBehaviour
{
    public static ItemsDataBase Instance;
    [SerializeField] private Sprite[] itemsSprites;
    [SerializeField] private Sprite defaultSprite;


    private void Start()
    {
        if (Instance == null)
            Instance = FindObjectOfType<ItemsDataBase>();
    }

    public Sprite GetSpriteByItemType(InventoryItems inventoryItems)
    {
        switch (inventoryItems)
        {
            case InventoryItems.AMMO_TYPE_1:
                return itemsSprites[0];
            case InventoryItems.AMMO_TYPE_2:
                return itemsSprites[1];
            case InventoryItems.AMMO_TYPE_3:
                return itemsSprites[2];
            case InventoryItems.AMMO_TYPE_4:
                return itemsSprites[3];
            default:
                return defaultSprite;
        }
    }
}
