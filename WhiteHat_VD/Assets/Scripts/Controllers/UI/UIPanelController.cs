using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelController : PanelBase
{
    public Sprite emptySlot;
    public Sprite activeSlot;
    private UISlotBase[] uISlots;
    private AmmoUI ammonution;
    private BlackCristalUI blackCristals;
    private HelperPanelUI helperPanel; 

    private void Start()
    {
        uISlots = GetComponentsInChildren<UISlotBase>();
        ammonution = GetComponentInChildren<AmmoUI>();
        blackCristals = GetComponentInChildren<BlackCristalUI>();
        helperPanel = GetComponentInChildren<HelperPanelUI>();
        HideHelperPanel();

        ShowHelperPanel("Find 3 Black Cristals and shut down a virus machine!", 4f);
    }

    private void FixedUpdate()
    {
        
    }

    public void SetAmmo(int ammoAmount)
    {
        ammonution.ammoText.text = ammoAmount + "";
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

    private void ClearSlotImage()
    {
        foreach (var item in uISlots)
        {
            item.GetComponent<Image>().sprite = emptySlot;
        }
    }
    
    public void SetSlotImage(int number, int ammoAmount)
    {
        ClearSlotImage();
        uISlots[number].GetComponent<Image>().sprite = activeSlot;
        SetAmmo(ammoAmount);
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
