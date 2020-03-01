using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlacePanelController : PanelBase
{
    [SerializeField] private GameObject upgradeBasesParent;
    [SerializeField] private GameObject contentGuns;
    private UpgradeBase[] upgradeBases;
    private ShopItemDisplay[] shopItemDisplays;
    private BlackCristalUI blackCristals;
    void Start()
    {
        blackCristals = GetComponentInChildren<BlackCristalUI>();
        upgradeBases = upgradeBasesParent.GetComponentsInChildren<UpgradeBase>();
        shopItemDisplays = contentGuns.GetComponentsInChildren<ShopItemDisplay>();
        this.gameObject.SetActive(false);
    }

    public void SetUpgradeBasesInfo()
    {
        foreach (var item in upgradeBases)
        {
            item.UpdateInfo();
        }
    }

    public void SetBlackCristals(int bcAmount)
    {
        blackCristals.BcText.text = bcAmount + "";
    }

    public void SetItemBought(InventoryItems item)
    {
        switch (item)
        {
            //Diffrent idx -> because diffrent position in hierarchy
            case InventoryItems.AMMO_TYPE_1:
                shopItemDisplays[2].SetBoughtItem();
                break;
            case InventoryItems.AMMO_TYPE_2:
                shopItemDisplays[0].SetBoughtItem();
                break;
            case InventoryItems.AMMO_TYPE_3:
                shopItemDisplays[1].SetBoughtItem();
                break;
            case InventoryItems.AMMO_TYPE_4:
                shopItemDisplays[3].SetBoughtItem();
                break;
        }
    }

    public void ClearBoughtItems()
    {
        shopItemDisplays[0].ClearBoughtItem();
        shopItemDisplays[1].ClearBoughtItem();
        shopItemDisplays[2].ClearBoughtItem();
        shopItemDisplays[3].ClearBoughtItem();
    }
}
