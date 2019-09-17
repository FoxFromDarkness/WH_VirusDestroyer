using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class UIPanelController : PanelBase
{
    private const float HP_BAR_WIDTH = 500.0f;

    [SerializeField] private Sprite emptySlot;
    [SerializeField] private Sprite activeSlot;
    [SerializeField] private RectTransform hpBar;
    private UISlotBase[] uISlots;
    private AmmoUI ammunition;
    private BlackCristalUI blackCristals;
    private HelperPanelUI helperPanel;
    private HPBossUI hpBossUI;

    private void Start()
    {
        uISlots = GetComponentsInChildren<UISlotBase>();
        ammunition = GetComponentInChildren<AmmoUI>();
        blackCristals = GetComponentInChildren<BlackCristalUI>();
        helperPanel = GetComponentInChildren<HelperPanelUI>();
        hpBossUI = GetComponentInChildren<HPBossUI>();
        HideObjects();

        DeactiveSlots();
    }

    public void SetHPBar(float hpFraction) 
    {
        hpBar.sizeDelta = new Vector2(hpFraction * HP_BAR_WIDTH, hpBar.sizeDelta.y);
    }

    private void DeactiveSlots()
    {
        foreach (var item in uISlots)
        {
            if (!item.IsSlot0)
                item.gameObject.SetActive(false);
        }
    }

    public int UnlockNextSlot(Sprite img)
    {
        foreach (var item in uISlots)
        {
            if (!item.gameObject.activeSelf)
            {
                item.AmmoImage = img;
                item.gameObject.SetActive(true);
                return item.transform.GetSiblingIndex();
            }
        }
        throw new System.Exception("UnlockNextSlot Error");
    }

    public void SetAmmo(int ammoAmount)
    {
        if (ammoAmount != -1)
            ammunition.AmmoText.text = ammoAmount + "";
        else
            ammunition.AmmoText.text = "∞";
    }

    public void SetBlackCristals(int bcAmount)
    {
        blackCristals.BcText.text = bcAmount + "";
    }

    public void ShowHelperPanel(string info, float showTimeSek)
    {
        StartCoroutine(CoShowHelperPanel(info, showTimeSek));
    }

    public void HideObjects()
    {
        helperPanel.gameObject.SetActive(false);
        hpBossUI.gameObject.SetActive(false);
    }

    public void HideHelperPanel()
    {
        helperPanel.gameObject.SetActive(false);
    }

    private void ClearSlotImage()
    {
        foreach (var item in uISlots)
        {
            if(item.gameObject.activeSelf)
                item.GetComponent<Image>().sprite = emptySlot;
        }
    }
    
    public void SetSlotImage(int number, int ammoAmount)
    {
        ClearSlotImage();
        if (uISlots[number + 1].gameObject.activeSelf)
        {
            uISlots[number + 1].GetComponent<Image>().sprite = activeSlot;
            SetAmmo(ammoAmount);
        }
        else
        {
            Platformer2DUserControl.NumberSlotKey = -1;
            SetAmmo(-1);
        }
    }

    private IEnumerator CoShowHelperPanel(string info, float showTimeSek)
    {
        helperPanel.HelpText.text = info;
        helperPanel.gameObject.SetActive(true);

        if (showTimeSek != 0)
        {
            yield return new WaitForSeconds(showTimeSek);
            helperPanel.gameObject.SetActive(false);
        }
    }
}
