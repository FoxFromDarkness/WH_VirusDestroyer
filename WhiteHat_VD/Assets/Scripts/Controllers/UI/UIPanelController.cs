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
        HideHelperPanel();

        ShowHelperPanel("Find 3 Black Cristals and shut down a virus machine!", 4f);
    }

    public void SetBlackCristals(int bcAmount)
    {
        blackCristals.bcText.text = bcAmount + "";
    }

    public void ShowHelperPanel(string info, float showTimeSek)
    {
        StartCoroutine(CoShowHelperPanel(info, showTimeSek));
    }

    public void HideHelperPanel()
    {
        helperPanel.gameObject.SetActive(false);
    }

    private IEnumerator CoShowHelperPanel(string info, float showTimeSek)
    {
        helperPanel.helpText.text = info;
        helperPanel.gameObject.SetActive(true);

        if (showTimeSek != 0)
        {
            yield return new WaitForSeconds(showTimeSek);
            helperPanel.gameObject.SetActive(false);
        }
    }
}
