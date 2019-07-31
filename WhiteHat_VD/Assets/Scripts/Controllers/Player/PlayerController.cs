using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerController : MonoBehaviour
{
    public PlayerBase playerStats { get; set; }

    public UIPanelController uiPanel;
    public SavePlacePanelController savePlacePanel;
    public QuestionPanelController questionPanel;

    public bool canOpenSavePlace { get; set; }

    private void Start()
    {
        playerStats = GetComponent<PlayerBase>();
    }

    private void Update()
    {
        if(canOpenSavePlace)
        {
            if (Input.GetButtonDown("SavePlace"))
                savePlacePanel.ChangeVisibility();

            if(!savePlacePanel.gameObject.activeSelf)
                GetComponent<Platformer2DUserControl>().isPlayerActive = true;
        }
    }

    public void AddBlackCristals(int value)
    {
        playerStats.BlackCristals += value;
        uiPanel.SetBlackCristals(playerStats.BlackCristals);
    }  
}
