using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel1 : LevelBase
{
    private PlayerController player;
    private Boss1Behaviour[] smallBosses;
    public LevelPortalController levelPortal;
    private bool isDead = false;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerController>();
        smallBosses = GetComponentsInChildren<Boss1Behaviour>();
        //Invoke("ActivateBoss", 3.0f);
    }

    public void ActivateBoss()
    {
        HeadPanelController.Instance.uiPanel.hpBossUI.InitHpBossSlider(CheckHPOfChildren(), default, default);
        base.isAnimation = true;
        foreach (var item in smallBosses)
            item.isShooting = true;
    }

    public float CheckHPOfChildren()
    {
        float BossHP = 0;
        foreach (var item in smallBosses)
            BossHP += item.actualHP;
        if (BossHP <= 0 && player.GetAttribute(PlayerAttributes.HP) > 0 && !isDead)
            StartCoroutine(CoBossDefeated());
        return BossHP;
    }

    private IEnumerator CoBossDefeated()
    {
        isDead = true;
        yield return new WaitForSeconds(2.0f);
        HeadPanelController.Instance.uiPanel.HideObjects();
        HeadPanelController.Instance.uiPanel.ShowGameOver(true);
        //levelPortal.IsActive = true;
        player.AddItem(InventoryItems.BLACK_CRISTALS, 1000);
        ortographicSize = DEFAULT_ORTOGRAPHIC_SIZE;
        isAnimation = true;
    }
}
