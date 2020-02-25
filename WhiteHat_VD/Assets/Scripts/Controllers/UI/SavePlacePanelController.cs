using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlacePanelController : PanelBase
{
    [SerializeField] private GameObject upgradeBasesParent;
    private UpgradeBase[] upgradeBases;
    private BlackCristalUI blackCristals;
    void Start()
    {
        blackCristals = GetComponentInChildren<BlackCristalUI>();
        upgradeBases = upgradeBasesParent.GetComponentsInChildren<UpgradeBase>();
        this.gameObject.SetActive(false);
    }

    public void SetUpgradeBasesInfo()
    {
        Debug.Log(upgradeBases.Length);
        foreach (var item in upgradeBases)
        {
            item.UpdateInfo();
        }
    }

    public void SetBlackCristals(int bcAmount)
    {
        blackCristals.BcText.text = bcAmount + "";
    }
}
