using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerController : MonoBehaviour
{
    [Header("Player Options")]
    public bool GodMode;

    private PlayerBase PlayerStats { get; set; }

    [Header("Panels")]
    public UIPanelController uiPanel;
    public SavePlacePanelController savePlacePanel;
    public QuestionPanelController questionPanel;

    public bool CanOpenSavePlace { get; set; }
    //Portals
    public bool InPortal { get; set; }
    public Vector2 NewPortalPosition { get; set; }

    private BulletPlayerController bullet;
    private bool isShot;
    private bool isSlotChangeImageKey;

    private void Start()
    {
        PlayerStats = GetComponent<PlayerBase>();

        bullet = GetComponentInChildren<BulletPlayerController>();
        bullet.gameObject.SetActive(false);

        InitPlayerWeaponMods();
    }

    private void Update()
    {
        if (GetComponent<Platformer2DUserControl>().IsShotKey && !CanOpenSavePlace)
            isShot = IsAmmo();

        if (GetComponent<Platformer2DUserControl>().IsSlotChangeImageKey)
            isSlotChangeImageKey = true;

        if (InPortal)
            StartTeleportInPortal(NewPortalPosition);

        Shooting();
        ChangingSlotImage();
        SavePlaceEnter();
    }

    private void InitPlayerWeaponMods()
    {
        PlayerStats.WeaponMods[0] = new WeaponBase();
        PlayerStats.WeaponMods[1] = new WeaponBase();
        PlayerStats.WeaponMods[2] = new WeaponBase();
        PlayerStats.WeaponMods[3] = new WeaponBase();
    }

    private bool IsAmmo()
    {
        bool isAmmo = false;

        switch (Platformer2DUserControl.NumberSlotKey)
        {
            case 0:
                if (PlayerStats.WeaponMods[0].AmmoAmount > 0)
                    isAmmo = true;
                break;

            case 1:
                if (PlayerStats.WeaponMods[1].AmmoAmount > 0)
                    isAmmo = true;
                break;

            case 2:
                if (PlayerStats.WeaponMods[2].AmmoAmount > 0)
                    isAmmo = true;
                break;

            case 3:
                if (PlayerStats.WeaponMods[3].AmmoAmount > 0)
                    isAmmo = true;
                break;

            default:
                isAmmo = true;
                break;
        }

        return isAmmo;
    }

    private int IndexOfWeaponMod(InventoryItems weaponModAmmo)
    {
        for (int i = 0; i < PlayerStats.WeaponMods.Length; i++)
        {
            if (PlayerStats.WeaponMods[i].TypeOfAmmo == weaponModAmmo)
                return i;
        }
        return -1;
    }

    private void Shooting()
    {
        if (isShot)
        {
            var copy_bullet = Instantiate(bullet, bullet.transform.parent);
            copy_bullet.GetComponent<BulletPlayerController>().InitBullet(GetActiveWeapon());
            AddItem(GetActiveWeapon(), -1);
            isShot = false;
        }
    }

    public void CheckHeroDeath()
    {
        if(PlayerStats.HP <= 0 && !GodMode)
        {
            GetComponent<Platformer2DUserControl>().IsPlayerActive = false;
            gameObject.SetActive(false);
            uiPanel.ShowHelperPanel("GAME OVER!", 2.0f);
        }
    }

    private InventoryItems GetActiveWeapon()
    {
        switch (Platformer2DUserControl.NumberSlotKey)
        {
            case 0:
                return PlayerStats.WeaponMods[0].TypeOfAmmo;
            case 1:
                return PlayerStats.WeaponMods[1].TypeOfAmmo;
            case 2:
                return PlayerStats.WeaponMods[2].TypeOfAmmo;
            case 3:
                return PlayerStats.WeaponMods[3].TypeOfAmmo;
            default:
                return InventoryItems.NULL;
        }
    }

    private void ChangingSlotImage()
    {
        if (isSlotChangeImageKey)
        {
            var numberSlot = Platformer2DUserControl.NumberSlotKey;
            if (numberSlot != -1 && PlayerStats.WeaponMods[numberSlot].IsUnlock)
                uiPanel.SetSlotImage(numberSlot, PlayerStats.WeaponMods[numberSlot].AmmoAmount);
            else
                uiPanel.SetSlotImage(numberSlot, -1);
            isSlotChangeImageKey = false;
        }
    }

    private void SavePlaceEnter()
    {
        if (CanOpenSavePlace)
        {
            if (Input.GetButtonDown("SavePlace") || Input.GetKeyDown(KeyCode.UpArrow)) //Really important. Remember about it
                savePlacePanel.ChangeVisibility();

            if (!savePlacePanel.gameObject.activeSelf)
                GetComponent<Platformer2DUserControl>().IsPlayerActive = true;
        }
    }

    public void ChangeAttribute(PlayerAttributes attribute, float amount)
    {
        switch (attribute)
        {
            case PlayerAttributes.HP:
                PlayerStats.HP = amount;
                uiPanel.SetHPBar(PlayerStats.HP / PlayerStats.HP_Max);
                break;
            case PlayerAttributes.HP_MAX:
                PlayerStats.HP_Max = amount;
                uiPanel.SetHPBar(PlayerStats.HP / PlayerStats.HP_Max);
                break;
            case PlayerAttributes.LEVEL:
                PlayerStats.Level = amount;
                break;
            case PlayerAttributes.EXP:
                PlayerStats.Exp = amount;
                break;
            case PlayerAttributes.EXP_TO_NEXT_LVL:
                PlayerStats.ExpToNextLvl = amount;
                break;
        }
    }

    public void AddAttribute(PlayerAttributes attribute, float amount)
    {
        switch (attribute)
        {
            case PlayerAttributes.HP:
                PlayerStats.HP += amount;
                uiPanel.SetHPBar(PlayerStats.HP / PlayerStats.HP_Max);
                break;
            case PlayerAttributes.HP_MAX:
                PlayerStats.HP_Max += amount;
                uiPanel.SetHPBar(PlayerStats.HP / PlayerStats.HP_Max);
                break;
            case PlayerAttributes.LEVEL:
                PlayerStats.Level += amount;
                break;
            case PlayerAttributes.EXP:
                PlayerStats.Exp += amount;
                break;
            case PlayerAttributes.EXP_TO_NEXT_LVL:
                PlayerStats.ExpToNextLvl += amount;
                break;
        }
    }

    public void ChangeItemAmount(InventoryItems item, int amount)
    {
        switch (item)
        {
            case InventoryItems.BLACK_CRISTALS:
                PlayerStats.BlackCristals = amount;
                uiPanel.SetBlackCristals(PlayerStats.BlackCristals);
                break;
            case InventoryItems.AMMO_TYPE_1:
                PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount = amount;
                uiPanel.SetAmmo(PlayerStats.WeaponMods[0].AmmoAmount);
                break;
            case InventoryItems.AMMO_TYPE_2:
                PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount = amount;
                uiPanel.SetAmmo(PlayerStats.WeaponMods[1].AmmoAmount);
                break;
            case InventoryItems.AMMO_TYPE_3:
                PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount = amount;
                uiPanel.SetAmmo(PlayerStats.WeaponMods[2].AmmoAmount);
                break;
            case InventoryItems.AMMO_TYPE_4:
                PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount = amount;
                uiPanel.SetAmmo(PlayerStats.WeaponMods[3].AmmoAmount);
                break;
            case InventoryItems.NULL:
                break;
        }
    }

    public void AddItem(InventoryItems item, int amount)
    {
        int idx;
        switch (item)
        {
            case InventoryItems.BLACK_CRISTALS:
                PlayerStats.BlackCristals += amount;
                uiPanel.SetBlackCristals(GetItemAmount(InventoryItems.BLACK_CRISTALS));
                break;
            case InventoryItems.AMMO_TYPE_1:
                idx = IndexOfWeaponMod(item);
                if (idx != -1) {
                    PlayerStats.WeaponMods[idx].AmmoAmount += amount;
                    if (Platformer2DUserControl.NumberSlotKey == idx)
                        uiPanel.SetAmmo(PlayerStats.WeaponMods[idx].AmmoAmount);
                }
                else PlayerStats.AmmoBoxSupply[0] += amount;
                break;
            case InventoryItems.AMMO_TYPE_2:
                idx = IndexOfWeaponMod(item);
                if (idx != -1) {
                    PlayerStats.WeaponMods[idx].AmmoAmount += amount;
                    if (Platformer2DUserControl.NumberSlotKey == idx)
                        uiPanel.SetAmmo(PlayerStats.WeaponMods[idx].AmmoAmount);
                }
                else PlayerStats.AmmoBoxSupply[1] += amount;
                break;
            case InventoryItems.AMMO_TYPE_3:
                idx = IndexOfWeaponMod(item);
                if (idx != -1) {
                    PlayerStats.WeaponMods[idx].AmmoAmount += amount;
                    if (Platformer2DUserControl.NumberSlotKey == idx)
                        uiPanel.SetAmmo(PlayerStats.WeaponMods[idx].AmmoAmount);
                }
                else PlayerStats.AmmoBoxSupply[2] += amount;
                break;
            case InventoryItems.AMMO_TYPE_4:
                idx = IndexOfWeaponMod(item);
                if (idx != -1) {
                    PlayerStats.WeaponMods[idx].AmmoAmount += amount;
                    if (Platformer2DUserControl.NumberSlotKey == idx)
                        uiPanel.SetAmmo(PlayerStats.WeaponMods[idx].AmmoAmount);
                }
                else PlayerStats.AmmoBoxSupply[3] += amount;
                break;
            case InventoryItems.NULL:
                break;
        }
    }

    public float GetAttribute(PlayerAttributes attribute)
    {
        switch (attribute)
        {
            case PlayerAttributes.HP:
                return PlayerStats.HP;
            case PlayerAttributes.HP_MAX:
                return PlayerStats.HP_Max;
            case PlayerAttributes.LEVEL:
                return PlayerStats.Level;
            case PlayerAttributes.EXP:
                return PlayerStats.Exp;
            case PlayerAttributes.EXP_TO_NEXT_LVL:
                return PlayerStats.ExpToNextLvl;
            default:
                return -1;
        }
    }

    public int GetItemAmount(InventoryItems item)
    {
        switch (item)
        {
            case InventoryItems.BLACK_CRISTALS:
                return PlayerStats.BlackCristals;
            case InventoryItems.AMMO_TYPE_1:
                return PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount;
            case InventoryItems.AMMO_TYPE_2:
                return PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount;
            case InventoryItems.AMMO_TYPE_3:
                return PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount;
            case InventoryItems.AMMO_TYPE_4:
                return PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount;
            default:
                return -1;
        }
    }

    public void BuyItem(InventoryItems item, Sprite sprite)
    {
        int idx = uiPanel.UnlockNextSlot(sprite) - 1;

        switch (item)
        {
            case InventoryItems.AMMO_TYPE_1:
                PlayerStats.WeaponMods[idx].TypeOfAmmo = item;
                PlayerStats.WeaponMods[idx].AmmoAmount += PlayerStats.AmmoBoxSupply[0];
                PlayerStats.AmmoBoxSupply[0] = 0;
                PlayerStats.WeaponMods[idx].IsUnlock = true;
                break;
            case InventoryItems.AMMO_TYPE_2:
                PlayerStats.WeaponMods[idx].TypeOfAmmo = item;
                PlayerStats.WeaponMods[idx].AmmoAmount += PlayerStats.AmmoBoxSupply[1];
                PlayerStats.AmmoBoxSupply[1] = 0;
                PlayerStats.WeaponMods[idx].IsUnlock = true;
                break;
            case InventoryItems.AMMO_TYPE_3:
                PlayerStats.WeaponMods[idx].TypeOfAmmo = item;
                PlayerStats.WeaponMods[idx].AmmoAmount += PlayerStats.AmmoBoxSupply[2];
                PlayerStats.AmmoBoxSupply[2] = 0;
                PlayerStats.WeaponMods[idx].IsUnlock = true;
                break;
            case InventoryItems.AMMO_TYPE_4:
                PlayerStats.WeaponMods[idx].TypeOfAmmo = item;
                PlayerStats.WeaponMods[idx].AmmoAmount += PlayerStats.AmmoBoxSupply[3];
                PlayerStats.AmmoBoxSupply[3] = 0;
                PlayerStats.WeaponMods[idx].IsUnlock = true;
                break;
        }
    }
    

    public void StartTeleportInPortal(Vector2 newPosition) {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            this.transform.position = newPosition;
            InPortal = false;
        }
    }
}