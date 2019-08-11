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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SavePlaceBehavior_Exit(collision);
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
            player.playerStats.startPosition = new Vector3(-1240.0f, 255.0f);
            //
            this.gameObject.transform.position = player.playerStats.startPosition;
        }
    }

    private void BlackCristalBehavior(Collider2D collision)
    {
        if (collision.GetComponent<BlackCristalBase>())
        {
            player.AddBlackCristals(1);
            collision.gameObject.SetActive(false);
        }
    }

    private void AmmoBoxBehavior_Enter(Collider2D collision)
    {
        if (collision.GetComponent<AmmoBoxBase>())
        {
            collision.gameObject.SetActive(false);
            int tmpAmmo = collision.GetComponent<AmmoBoxBase>().AmmoAmount;
            player.uiPanel.ShowHelperPanel("Ammo: +" + tmpAmmo, 2f);
            player.AddAmmo(tmpAmmo);
        }
    }

    private void SavePlaceBehavior_Enter(Collider2D collision)
    {
        if (collision.GetComponent<SavePlaceBase>())
        {
            player.uiPanel.ShowHelperPanel("Press 'P' to enter", 0f);
            player.canOpenSavePlace = true;
        }
    }

    private void SavePlaceBehavior_Exit(Collider2D collision)
    {
        if (collision.GetComponent<SavePlaceBase>())
        {
            player.uiPanel.HideHelperPanel();
            player.canOpenSavePlace = false;
        }
    }

    private void ParticleSystemBehavior_Enter(Collider2D collision)
    {
        if (collision.GetComponent<ParticleSystem>() && player.playerStats.BlackCristals >= 3)
        {
            collision.gameObject.SetActive(false);
            player.uiPanel.ShowHelperPanel("Congratulations!!", 5f);
            player.AddBlackCristals(97);
        }
    }
}
