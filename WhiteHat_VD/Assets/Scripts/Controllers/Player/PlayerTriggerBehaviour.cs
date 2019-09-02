using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerBehaviour : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        QuestionDoorBehavior(collision);
        DeadZoneBehavior(collision);
        BlackCristalBehavior(collision);
        AmmoBoxBehavior_Enter(collision);
        SavePlaceBehavior_Enter(collision);
        ParticleSystemBehavior_Enter(collision);
        PortalBehaviour_Enter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SavePlaceBehavior_Exit(collision);
        PortalBehaviour_Exit(collision);
    }

    private void QuestionDoorBehavior(Collider2D collision)
    {
        if (collision.GetComponent<QuestionDoorBase>())
        {
            if (!player.questionPanel.gameObject.activeSelf)
            {
                player.questionPanel.QuestionBehaviour();
                collision.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void DeadZoneBehavior(Collider2D collision)
    {
        if (collision.GetComponent<DeadZoneBase>())
        {
            //TO DELETE
            //player.PlayerStats.startPosition = new Vector3(-1240.0f, 255.0f);
            //
            this.gameObject.transform.position = new Vector3(-1240.0f, 255.0f); //player.PlayerStats.startPosition;
        }
    }

    private void BlackCristalBehavior(Collider2D collision)
    {
        if (collision.GetComponent<BlackCristalBase>())
        {
            player.AddItem(InventoryItems.BLACK_CRISTALS, 1);
            collision.gameObject.SetActive(false);
        }
    }

    private void AmmoBoxBehavior_Enter(Collider2D collision)
    {
        if (collision.GetComponent<AmmoBoxBase>())
        {
            collision.gameObject.SetActive(false);
            int amountAmmo = collision.GetComponent<AmmoBoxBase>().AmmoAmount;
            InventoryItems ammoType = collision.GetComponent<AmmoBoxBase>().AmmoType;
            player.uiPanel.ShowHelperPanel(ammoType.ToString() + ": " + amountAmmo, 2f);
            player.AddItem(ammoType, amountAmmo);
        }
    }

    private void SavePlaceBehavior_Enter(Collider2D collision)
    {
        if (collision.GetComponent<SavePlaceBase>())
        {
            player.uiPanel.ShowHelperPanel("Press 'Up arrow' to enter", 0f);
            player.CanOpenSavePlace = true;
        }
    }

    private void SavePlaceBehavior_Exit(Collider2D collision)
    {
        if (collision.GetComponent<SavePlaceBase>())
        {
            player.uiPanel.HideHelperPanel();
            player.CanOpenSavePlace = false;
        }
    }

    private void ParticleSystemBehavior_Enter(Collider2D collision)
    {
        if (collision.GetComponent<ParticleSystem>() && player.GetItemAmount(InventoryItems.BLACK_CRISTALS) >= 3)
        {
            collision.gameObject.SetActive(false);
            player.uiPanel.ShowHelperPanel("Congratulations!!", 5f);
            player.AddItem(InventoryItems.BLACK_CRISTALS, 97);
        }
    }

    private void PortalBehaviour_Enter(Collider2D collision) {
        if (collision.GetComponent<PortalController>()) {
            player.uiPanel.ShowHelperPanel("Press 'Up arrow' to enter", 0f);
            player.NewPortalPosition = collision.GetComponent<PortalController>().GetPortalPosition(0);
            player.InPortal = true;
        }
    }

    private void PortalBehaviour_Exit(Collider2D collision) {
        if (collision.GetComponent<PortalController>()) {
            player.uiPanel.HideHelperPanel();
            player.InPortal = false;
        }
    }

}
