using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerController : MonoBehaviour
{
    public PlayerBase playerStats { get; set; }

    [Header("Panels")]
    public UIPanelController uiPanel;
    public SavePlacePanelController savePlacePanel;
    public QuestionPanelController questionPanel;

    public bool canOpenSavePlace { get; set; }

    private BulletBase bullet;
    public bool isShot;

    private void Start()
    {
        playerStats = GetComponent<PlayerBase>();

        bullet = GetComponentInChildren<BulletBase>();
        bullet.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GetComponent<Platformer2DUserControl>().isShotKey && playerStats.Ammo > 0 && !canOpenSavePlace)
            isShot = true;

        Shooting();
        SavePlaceEnter(); 
    }

    public void AddAmmo(int value)
    {
        playerStats.Ammo += value;
        uiPanel.SetAmmo(playerStats.Ammo);
    }

    public void AddBlackCristals(int value)
    {
        playerStats.BlackCristals += value;
        uiPanel.SetBlackCristals(playerStats.BlackCristals);
    }
    
    private void Shooting()
    {
        if (isShot)
        {
            var copy_bullet = Instantiate(bullet, bullet.transform.parent);
            copy_bullet.gameObject.SetActive(true);
            copy_bullet.GetComponent<BulletBase>().Moving = true;
            AddAmmo(-1);
            isShot = false;
        }    
    }

    private void SavePlaceEnter()
    {
        if (canOpenSavePlace)
        {
            if (Input.GetButtonDown("SavePlace"))
                savePlacePanel.ChangeVisibility();

            if (!savePlacePanel.gameObject.activeSelf)
                GetComponent<Platformer2DUserControl>().isPlayerActive = true;
        }
    }
}
