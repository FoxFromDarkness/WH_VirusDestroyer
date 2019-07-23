using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerController : MonoBehaviour
{
    private PlayerBase playerStats;

    public UIPanelController uiPanel;
    public SavePlacePanelController savePlacePanel;
    public QuestionPanelController questionPanel;
    
    private bool canOpenSavePlace;

    private void Start()
    {
        playerStats = GetComponent<PlayerBase>();
    }

    private void Update()
    {
        if(canOpenSavePlace)
        {
            if (Input.GetButtonDown("SavePlace"))
            {
                savePlacePanel.ChangeVisibility();
                GetComponent<Platformer2DUserControl>().isPlayerActive = !GetComponent<Platformer2DUserControl>().isPlayerActive;
            }
        }

        if(!canOpenSavePlace)
        {
            if (Input.GetButtonDown("SavePlace"))
            {
                savePlacePanel.ChangeVisibility(false);
                GetComponent<Platformer2DUserControl>().isPlayerActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        QuestionDoorBehavior(collision);
        DeadZoneBehavior(collision);
        BlackCristalBehavior(collision);
        SavePlaceBehavior_Enter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SavePlaceBehavior_Exit(collision);
    }

    private void QuestionDoorBehavior(Collider2D collision)
    {
        if (collision.GetComponent<QuestionDoorBase>())
        {
            if (!questionPanel.gameObject.activeSelf)
            {
                questionPanel.QuestionBehaviour();
                collision.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void DeadZoneBehavior(Collider2D collision)
    {
        if (collision.GetComponent<DeadZoneBase>())
        {
            this.gameObject.transform.position = new Vector3(-1240.0f, 255.0f);
        }
    }

    private void BlackCristalBehavior(Collider2D collision)
    {
        if (collision.GetComponent<BlackCristalBase>())
        {
            playerStats.BlackCristals += 1;
            uiPanel.setBlackCristals(playerStats.BlackCristals);
            collision.gameObject.SetActive(false);
        }
    }

    private void SavePlaceBehavior_Enter(Collider2D collision)
    {
        if (collision.GetComponent<SavePlaceBase>())
        {
            uiPanel.showHelperPanel();
            canOpenSavePlace = true;
        }
    }

    private void SavePlaceBehavior_Exit(Collider2D collision)
    {
        if (collision.GetComponent<SavePlaceBase>())
        {
            uiPanel.hideHelperPanel();
            canOpenSavePlace = false;
        }
    }
}
