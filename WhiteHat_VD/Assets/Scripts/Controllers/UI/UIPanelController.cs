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
    private GameOverPanelUI gameOverPanel;

    public HPBossUI hpBossUI { get; set; }

    private void Start()
    {
        uISlots = GetComponentsInChildren<UISlotBase>();
        ammunition = GetComponentInChildren<AmmoUI>();
        blackCristals = GetComponentInChildren<BlackCristalUI>();
        helperPanel = GetComponentInChildren<HelperPanelUI>();
        hpBossUI = GetComponentInChildren<HPBossUI>();
        gameOverPanel = GetComponentInChildren<GameOverPanelUI>();
        HideObjects();

        DeactiveSlots();
    }

    public void SetHPBar(float hpFraction) 
    {
        hpBar.sizeDelta = new Vector2(hpFraction * HP_BAR_WIDTH, hpBar.sizeDelta.y);
    }

    public void DeactiveSlots()
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

    public void UnlockSlot(int idx, Sprite img)
    {
        uISlots[idx].AmmoImage = img;
        uISlots[idx].gameObject.SetActive(true);
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

    private float helperShowTime;
    public void ShowHelperPanel(string info, float showTimeSek = 0, Sprite spr = null)
    {
        if (spr != null)
        {
            helperPanel.AmmoImageL.sprite = spr;
            helperPanel.AmmoImageR.sprite = spr;
        }

        helperPanel.AmmoImageL.gameObject.SetActive(spr == null ? false : true);
        helperPanel.AmmoImageR.gameObject.SetActive(spr == null ? false : true);
        helperPanel.HelpText.text = info;
        helperPanel.gameObject.SetActive(true);

        helperShowTime = showTimeSek;
        if (showTimeSek != 0 && !isCoroutineRun)
            StartCoroutine(CoHideHelperPanel());

        if (showTimeSek == 0)
        {
            StopCoroutine(CoHideHelperPanel());
            helperShowTime = 60;
        }
    }

    public void HideObjects()
    {
        helperPanel.gameObject.SetActive(false);
        hpBossUI.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
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

    private bool isCoroutineRun = false;
    private IEnumerator CoHideHelperPanel()
    {
        isCoroutineRun = true;
        while (helperShowTime > 0)
        {
            yield return new WaitForSeconds(1f);
            helperShowTime -= 1;
        }
        
        helperPanel.gameObject.SetActive(false);
        isCoroutineRun = false;
    }

    public void ShowGameOver(bool gameResult)
    {
        if (gameResult == true)
        {
            gameOverPanel.SetColor(0, 255, 0);
            gameOverPanel.result.text = "YOU WIN";
        }
        else
        {
            gameOverPanel.SetColor(255, 0, 0);
            gameOverPanel.result.text = "YOU LOST";
        }
        gameOverPanel.gameObject.SetActive(true);
    }

    public void HideGameOver()
    {
        gameOverPanel.gameObject.SetActive(false);
    }
}
