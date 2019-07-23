using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPanelController : PanelBase
{
    private BlackCristalUI blackCristals;
    private HelperPanelUI helperPanel;

    private void Start()
    {
        blackCristals = GetComponentInChildren<BlackCristalUI>();
        helperPanel = GetComponentInChildren<HelperPanelUI>();
        hideHelperPanel();
    }

    public void setBlackCristals(int bcAmount)
    {
        blackCristals.bcText.text = bcAmount + "";
    }

    public void setHelperPanelText(string info)
    {
        helperPanel.helpText.text = info;
    }

    public void showHelperPanel()
    {
        helperPanel.gameObject.SetActive(true);
    }

    public void hideHelperPanel()
    {
        helperPanel.gameObject.SetActive(false);
    }

}
