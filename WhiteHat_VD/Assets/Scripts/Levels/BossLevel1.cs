using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel1 : MonoBehaviour
{
    private PlayerController player;
    private Boss1Behaviour[] smallBosses;

    private void Start()
    {
        Camera.main.orthographicSize *= 3.0f;
        player = FindObjectOfType<PlayerController>();

        Invoke("ActivateBoss", 2.0f);
    }

    public void ActivateBoss()
    {
        smallBosses = GetComponentsInChildren<Boss1Behaviour>();
        player.uiPanel.hpBossUI.InitHpBossSlider(CheckHPOfChildren(), default, default);
    }

    public float CheckHPOfChildren()
    {
        float BossHP = 0;
        foreach (var item in smallBosses)
            BossHP += item.actualHP;
        if (BossHP <= 0 && player.GetAttribute(PlayerAttributes.HP) > 0)
            BossDefeated();
        return BossHP;
    }

    private void BossDefeated()
    {
        player.uiPanel.HideObjects();
        player.uiPanel.ShowHelperPanel("You Win!", 2.0f);
    }
}
