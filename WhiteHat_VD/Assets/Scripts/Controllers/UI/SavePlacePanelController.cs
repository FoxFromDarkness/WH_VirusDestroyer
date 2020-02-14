using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlacePanelController : PanelBase
{
    private BlackCristalUI blackCristals;
    void Start()
    {
        this.gameObject.SetActive(false);
        blackCristals = GetComponentInChildren<BlackCristalUI>();
    }

    public void SetBlackCristals(int bcAmount)
    {
        blackCristals.BcText.text = bcAmount + "";
    }
}
