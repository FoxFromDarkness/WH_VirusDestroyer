using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerController : MonoBehaviour
{
    [Header("Player Options")]
    public bool GodMode;
    public GameObject _GameManager;

    public PlayerBase PlayerStats { get; set; }

    public bool CanOpenSavePlace { get; set; }

    //Portals
    public bool InPortal { get; set; }
    public bool InLevelPortal { get; set; }
    public Vector2 NewPortalPosition { get; set; }
    public LevelPortalController LevelPortalController { get; set; }
    //

    private BulletPlayerController bullet;
    private bool isShot;
    private bool isSlotChangeImageKey;

    private void Start()
    {
        PlayerStats = GetComponent<PlayerBase>();

        bullet = GetComponentInChildren<BulletPlayerController>();
        bullet.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!GameController.IsInputEnable) return;

        if (GetComponent<Platformer2DUserControl>().IsShotKey && !CanOpenSavePlace)
            isShot = IsAmmo();

        if (GetComponent<Platformer2DUserControl>().IsSlotChangeImageKey)
            isSlotChangeImageKey = true;

        if (InPortal)
            StartTeleportInPortal(NewPortalPosition);

        if (InLevelPortal)
            StartTeleportInLevelPortal(LevelPortalController);

        Shooting();
        ChangingSlotImage();
        SavePlaceEnter();
    }

    private bool IsAmmo()
    {
        bool isAmmo = true;

        switch (Platformer2DUserControl.NumberSlotKey)
        {

            case 0:
                if (PlayerStats.WeaponMods[0].AmmoAmount <= 0)
                    Platformer2DUserControl.NumberSlotKey = -1;
                break;

            case 1:
                if (PlayerStats.WeaponMods[1].AmmoAmount <= 0)
                    Platformer2DUserControl.NumberSlotKey = -1;
                break;

            case 2:
                if (PlayerStats.WeaponMods[2].AmmoAmount <= 0)
                    Platformer2DUserControl.NumberSlotKey = -1;
                break;

            case 3:
                if (PlayerStats.WeaponMods[3].AmmoAmount <= 0)
                    Platformer2DUserControl.NumberSlotKey = -1;
                break;

            default:
                Platformer2DUserControl.NumberSlotKey = -1;
                break;
        }
        ChangingSlotImage(true);
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

    public string GetFullWeaponInfo(int idx)
    {
        return PlayerStats.GetWeaponInfo(idx) + "|" + IndexOfWeaponMod(PlayerStats.WeaponMods[idx].TypeOfAmmo);
    }

    public void SetFullWeapon(int idx, string data)
    {
        string[] weaponsInfo = data.Split('|');
        if (weaponsInfo[0] == "False") return;

        InventoryItems typeOfAmmo = (InventoryItems)int.Parse(weaponsInfo[1]);
        SetBoughtItem(int.Parse(weaponsInfo[3]), typeOfAmmo, int.Parse(weaponsInfo[2]), ItemsDataBase.Instance.GetSpriteByItemType(typeOfAmmo));
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
        if (PlayerStats.HP <= 0 && !GodMode)
        {
            GameController.IsInputEnable = false;
            gameObject.SetActive(false);
            HeadPanelController.Instance.uiPanel.ShowHelperPanel("GAME OVER!", 2.0f);
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
                HeadPanelController.Instance.uiPanel.SetSlotImage(numberSlot, PlayerStats.WeaponMods[numberSlot].AmmoAmount);
            else
                HeadPanelController.Instance.uiPanel.SetSlotImage(numberSlot, -1);
            isSlotChangeImageKey = false;
        }
    }

    private void ChangingSlotImage(bool value)
    {
        if (value)
        {
            var numberSlot = Platformer2DUserControl.NumberSlotKey;
            if (numberSlot != -1 && PlayerStats.WeaponMods[numberSlot].IsUnlock)
                HeadPanelController.Instance.uiPanel.SetSlotImage(numberSlot, PlayerStats.WeaponMods[numberSlot].AmmoAmount);
            else
                HeadPanelController.Instance.uiPanel.SetSlotImage(numberSlot, -1);
            isSlotChangeImageKey = false;
        }
    }

    private void SavePlaceEnter()
    {
        if (CanOpenSavePlace)
        {
            if (Input.GetButtonDown("SavePlace") || Input.GetKeyDown(KeyCode.UpArrow)) //Really important. Remember about it
                HeadPanelController.Instance.savePlacePanel.ChangeVisibility();
        }
    }

    public void ChangeAttribute(PlayerAttributes attribute, float amount)
    {
        switch (attribute)
        {
            case PlayerAttributes.HP:
                PlayerStats.HP = amount;
                HeadPanelController.Instance.uiPanel.SetHPBar(PlayerStats.HP / PlayerStats.HP_Max);
                break;
            case PlayerAttributes.HP_MAX:
                PlayerStats.HP_Max = amount;
                HeadPanelController.Instance.uiPanel.SetHPBar(PlayerStats.HP / PlayerStats.HP_Max);
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
                if (PlayerStats.HP > PlayerStats.HP_Max)
                    PlayerStats.HP = PlayerStats.HP_Max;
                HeadPanelController.Instance.uiPanel.SetHPBar(PlayerStats.HP / PlayerStats.HP_Max);
                break;
            case PlayerAttributes.HP_MAX:
                PlayerStats.HP_Max += amount;
                HeadPanelController.Instance.uiPanel.SetHPBar(PlayerStats.HP / PlayerStats.HP_Max);
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
                HeadPanelController.Instance.uiPanel.SetBlackCristals(PlayerStats.BlackCristals);
                break;
            case InventoryItems.AMMO_TYPE_1:
                PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount = amount;
                HeadPanelController.Instance.uiPanel.SetAmmo(PlayerStats.WeaponMods[0].AmmoAmount);
                break;
            case InventoryItems.AMMO_TYPE_2:
                PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount = amount;
                HeadPanelController.Instance.uiPanel.SetAmmo(PlayerStats.WeaponMods[1].AmmoAmount);
                break;
            case InventoryItems.AMMO_TYPE_3:
                PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount = amount;
                HeadPanelController.Instance.uiPanel.SetAmmo(PlayerStats.WeaponMods[2].AmmoAmount);
                break;
            case InventoryItems.AMMO_TYPE_4:
                PlayerStats.WeaponMods[IndexOfWeaponMod(item)].AmmoAmount = amount;
                HeadPanelController.Instance.uiPanel.SetAmmo(PlayerStats.WeaponMods[3].AmmoAmount);
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
                HeadPanelController.Instance.uiPanel.SetBlackCristals(GetItemAmount(InventoryItems.BLACK_CRISTALS));
                break;
            case InventoryItems.AMMO_TYPE_1:
                idx = IndexOfWeaponMod(item);
                if (idx != -1)
                {
                    PlayerStats.WeaponMods[idx].AmmoAmount += amount;
                    if (Platformer2DUserControl.NumberSlotKey == idx)
                        HeadPanelController.Instance.uiPanel.SetAmmo(PlayerStats.WeaponMods[idx].AmmoAmount);
                }
                else PlayerStats.AmmoBoxSupply[0] += amount;
                break;
            case InventoryItems.AMMO_TYPE_2:
                idx = IndexOfWeaponMod(item);
                if (idx != -1)
                {
                    PlayerStats.WeaponMods[idx].AmmoAmount += amount;
                    if (Platformer2DUserControl.NumberSlotKey == idx)
                        HeadPanelController.Instance.uiPanel.SetAmmo(PlayerStats.WeaponMods[idx].AmmoAmount);
                }
                else PlayerStats.AmmoBoxSupply[1] += amount;
                break;
            case InventoryItems.AMMO_TYPE_3:
                idx = IndexOfWeaponMod(item);
                if (idx != -1)
                {
                    PlayerStats.WeaponMods[idx].AmmoAmount += amount;
                    if (Platformer2DUserControl.NumberSlotKey == idx)
                        HeadPanelController.Instance.uiPanel.SetAmmo(PlayerStats.WeaponMods[idx].AmmoAmount);
                }
                else PlayerStats.AmmoBoxSupply[2] += amount;
                break;
            case InventoryItems.AMMO_TYPE_4:
                idx = IndexOfWeaponMod(item);
                if (idx != -1)
                {
                    PlayerStats.WeaponMods[idx].AmmoAmount += amount;
                    if (Platformer2DUserControl.NumberSlotKey == idx)
                        HeadPanelController.Instance.uiPanel.SetAmmo(PlayerStats.WeaponMods[idx].AmmoAmount);
                }
                else PlayerStats.AmmoBoxSupply[3] += amount;
                break;
            case InventoryItems.HEALTH_POINTS:
                AddAttribute(PlayerAttributes.HP, amount);
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
        int idx = HeadPanelController.Instance.uiPanel.UnlockNextSlot(sprite) - 1;

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

    private void SetBoughtItem(int idx, InventoryItems item, int amount, Sprite sprite) //Loading operation
    {
        HeadPanelController.Instance.uiPanel.UnlockSlot(idx+1, sprite);

        PlayerStats.WeaponMods[idx].TypeOfAmmo = item;
        PlayerStats.WeaponMods[idx].AmmoAmount = amount;
        PlayerStats.WeaponMods[idx].IsUnlock = true;

        switch (item)
        {
            case InventoryItems.AMMO_TYPE_1:
                PlayerStats.WeaponMods[idx].AmmoAmount += PlayerStats.AmmoBoxSupply[0];
                PlayerStats.AmmoBoxSupply[0] = 0;
                break;
            case InventoryItems.AMMO_TYPE_2:
                PlayerStats.WeaponMods[idx].AmmoAmount += PlayerStats.AmmoBoxSupply[1];
                PlayerStats.AmmoBoxSupply[1] = 0;
                break;
            case InventoryItems.AMMO_TYPE_3:
                PlayerStats.WeaponMods[idx].AmmoAmount += PlayerStats.AmmoBoxSupply[2];
                PlayerStats.AmmoBoxSupply[2] = 0;
                break;
            case InventoryItems.AMMO_TYPE_4:
                PlayerStats.WeaponMods[idx].AmmoAmount += PlayerStats.AmmoBoxSupply[3];
                PlayerStats.AmmoBoxSupply[3] = 0;
                break;
        }
    }

    public void StartTeleportInPortal(Vector2 newPosition)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            this.transform.position = newPosition;
            InPortal = false;
        }
    }

    public void StartTeleportInLevelPortal(LevelPortalController levelPortalController)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            InLevelPortal = false;
            HeadPanelController.Instance.uiPanel.HideHelperPanel();
            _GameManager.GetComponent<SceneController>().UnloadScene(levelPortalController.thisSceneName);
            _GameManager.GetComponent<SceneController>().LoadScene(false, levelPortalController.nextSceneName, SetCharacterPositionAfterChangeLevel);
        }
    }

    private void SetCharacterPositionAfterChangeLevel()
    {
        this.transform.position = LevelPortalController.startLevelPosition;
    }
}