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
        ItemBoxBehavior_Enter(collision);
        SavePlaceBehavior_Enter(collision);
        ParticleSystemBehavior_Enter(collision);
        PortalBehaviour_Enter(collision);
        LevelPortalBehaviour_Enter(collision);
        EnemyBullet_Enter(collision);
        CasualEnemyBullet_Enter(collision);
        ChestBehavior_Enter(collision);
        PlatformEffector_Enter(collision);
        EnemyIdleArea_Enter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SavePlaceBehavior_Exit(collision);
        PortalBehaviour_Exit(collision);
        LevelPortalBehaviour_Exit(collision);
        ChestBehavior_Exit(collision);
        PlatformEffector_Exit(collision);
        EnemyIdleArea_Exit(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<BulletEnemyController>())
        {
            if(collision.GetComponent<BulletEnemyController>().isLaser)
                CasualEnemyBullet_Enter(collision);
        }
    }

    private void QuestionDoorBehavior(Collider2D collision)
    {
        if (collision.GetComponent<QuestionDoorBase>())
        {
            if (!HeadPanelController.Instance.questionPanel.gameObject.activeSelf)
            {
                HeadPanelController.Instance.questionPanel.QuestionBehaviour();
                collision.gameObject.SetActive(false);
                GameController.IsInputEnable = false;
                //this.gameObject.SetActive(false);
            }
        }
    }

    private void ChestBehavior_Enter(Collider2D collision)
    {
        if (collision.GetComponent<ChestBase>())
        {
            ChestBase chest = collision.GetComponent<ChestBase>();

            if (chest.QuestionStatus == QuestionStatus.CORRECT || chest.QuestionStatus == QuestionStatus.INCORRECT) return;

            if (chest.QuestionStatus == QuestionStatus.DEFAULT)
            {
                HeadPanelController.Instance.uiPanel.ShowHelperPanel("Press 'Up arrow' to open the chest", 0f);
                PlayerController.Chest = chest;
                player.CanOpenChest = true;
            }
            //HeadPanelController.Instance.questionPanel.QuestionBehaviour();
        }
    }

    private void ChestBehavior_Exit(Collider2D collision)
    {
        if (collision.GetComponent<ChestBase>())
        {
            ChestBase chest = collision.GetComponent<ChestBase>();

            if (chest.QuestionStatus == QuestionStatus.CORRECT || chest.QuestionStatus == QuestionStatus.INCORRECT) return;

            if (chest.QuestionStatus == QuestionStatus.DEFAULT)
            {
                HeadPanelController.Instance.uiPanel.HideHelperPanel();
                //PlayerController.Chest = null;
                player.CanOpenChest = false;

                //if (!HeadPanelController.Instance.questionPanel.gameObject.activeSelf)
                //{
                //    HeadPanelController.Instance.questionPanel.QuestionBehaviour();
                //    //collision.gameObject.SetActive(false);
                //    //this.gameObject.SetActive(false);

                //}
                //HeadPanelController.Instance.questionPanel.QuestionBehaviour();
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

    private void ItemBoxBehavior_Enter(Collider2D collision)
    {
        if (collision.GetComponent<ItemBoxBase>())
        {
            collision.gameObject.SetActive(false);
            int amountItem = collision.GetComponent<ItemBoxBase>().ItemAmount;
            InventoryItems itemType = collision.GetComponent<ItemBoxBase>().ItemType;
            HeadPanelController.Instance.uiPanel.ShowHelperPanel(itemType.ToString() + ": " + amountItem, 2f);
            player.AddItem(itemType, amountItem);
        }
    }

    private void SavePlaceBehavior_Enter(Collider2D collision)
    {
        if (collision.GetComponent<SavePlaceBase>())
        {
            HeadPanelController.Instance.uiPanel.ShowHelperPanel("Press 'Up arrow' to enter", 0f);
            player.CanOpenSavePlace = true;
        }
    }

    private void SavePlaceBehavior_Exit(Collider2D collision)
    {
        if (collision.GetComponent<SavePlaceBase>())
        {
            HeadPanelController.Instance.uiPanel.HideHelperPanel();
            player.CanOpenSavePlace = false;
        }
    }

    private void ParticleSystemBehavior_Enter(Collider2D collision)
    {
        if (collision.GetComponent<ParticleSystem>() && player.GetItemAmount(InventoryItems.BLACK_CRISTALS) >= 3)
        {
            collision.gameObject.SetActive(false);
            HeadPanelController.Instance.uiPanel.ShowHelperPanel("Congratulations!!", 5f);
            player.AddItem(InventoryItems.BLACK_CRISTALS, 97);
        }
    }

    private void PortalBehaviour_Enter(Collider2D collision) {
        if (collision.GetComponent<PortalController>()) {
            HeadPanelController.Instance.uiPanel.ShowHelperPanel("Press 'Up arrow' to enter", 0f);
            player.NewPortalPosition = collision.GetComponent<PortalController>().GetPortalPosition(0);
            player.InPortal = true;
        }
    }

    private void PortalBehaviour_Exit(Collider2D collision) {
        if (collision.GetComponent<PortalController>()) {
            HeadPanelController.Instance.uiPanel.HideHelperPanel();
            player.InPortal = false;
        }
    }

    private void LevelPortalBehaviour_Enter(Collider2D collision)
    {
        if (collision.GetComponent<LevelPortalController>())
        {
            var portal = collision.GetComponent<LevelPortalController>();
           if (portal.IsActive)
                HeadPanelController.Instance.uiPanel.ShowHelperPanel(portal.descriptionOpen, 0f);
           else
                HeadPanelController.Instance.uiPanel.ShowHelperPanel(portal.descriptionClosed, 0f);

            PlayerController.LevelPortalController = portal;
            player.InLevelPortal = true;

        }
    }

    private void LevelPortalBehaviour_Exit(Collider2D collision)
    {
        if (collision.GetComponent<LevelPortalController>())
        {
            HeadPanelController.Instance.uiPanel.HideHelperPanel();
            player.InLevelPortal = false;
        }
    }

    private void EnemyBullet_Enter(Collider2D collision)
    {
        if (collision.GetComponent<BulletEnemyTowerController>())
        {
            player.AddAttribute(PlayerAttributes.HP, collision.GetComponent<BulletEnemyTowerController>().damage * -1);
            player.CheckHeroDeath();
            Destroy(collision.gameObject);
        }
    }

    private void CasualEnemyBullet_Enter(Collider2D collision)
    {
        if (collision.GetComponent<BulletEnemyController>())
        {
            player.AddAttribute(PlayerAttributes.HP, collision.GetComponent<BulletEnemyController>().damage * -1);
            player.CheckHeroDeath();
            //Destroy(collision.gameObject);
        }
    }

    private void PlatformEffector_Enter(Collider2D collision)
    {
        if (collision.GetComponent<PlatformEffector2DBase>())
        {
            player.Platform = collision.GetComponent<PlatformEffector2DBase>();
            player.CanMoveDownPlatform = true;
        }
    }

    private void PlatformEffector_Exit(Collider2D collision)
    {
        if (collision.GetComponent<PlatformEffector2DBase>())
        {
            player.CanMoveDownPlatform = false;
        }
    }

    private void EnemyIdleArea_Enter(Collider2D collision)
    {
        if (collision.GetComponent<EnemyIdleArea>())
        {
            collision.GetComponent<EnemyIdleArea>().EnemyAreaTrigger(true);
        }
    }

    private void EnemyIdleArea_Exit(Collider2D collision)
    {
        if (collision.GetComponent<EnemyIdleArea>())
        {
            collision.GetComponent<EnemyIdleArea>().EnemyAreaTrigger(false);
        }
    }
}
