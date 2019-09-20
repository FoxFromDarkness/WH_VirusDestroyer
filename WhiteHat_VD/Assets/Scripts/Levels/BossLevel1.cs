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

    private void Start()
    { 
        player = FindObjectOfType<PlayerController>();

        Invoke("ActivateBoss", 2.0f);
    }

    public void ActivateBoss()
    {
        smallBosses = GetComponentsInChildren<Boss1Behaviour>();
        player.uiPanel.hpBossUI.InitHpBossSlider(CheckHPOfChildren(), default, default);
        base.isAnimation = true;
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
        player.uiPanel.HideObjects();
        player.uiPanel.ShowHelperPanel("You Win!", 2.0f);
        Debug.Log("WIN!");
        levelPortal.IsActive = true;
        player.AddItem(InventoryItems.BLACK_CRISTALS, 1000);
        ortographicSize = DEFAULT_ORTOGRAPHIC_SIZE;
        isAnimation = true;
    }
}
