using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerController : MonoBehaviour
{
    private const float LOADING_TIME_FOR_RIFLES = 0.25f;
    private const float LOADING_TIME_FOR_HEAVY_GUNS = 0.5f;

    [Header("Player Options")]
    private bool godMode;
    private Sprite startHeroSprite;
    public GameObject _GameManager;

    public PlayerBase PlayerStats { get; private set; }

    //
    public bool CanOpenSavePlace { get; set; }
    public bool CanOpenChest { get; set; }
    public bool CanMoveDownPlatform { get; set; }

    public static ChestBase Chest { get; set; }
    public PlatformEffector2DBase Platform { get; set; }

    //Portals
    public bool InPortal { get; set; }
    public bool InLevelPortal { get; set; }
    public Vector2 NewPortalPosition { get; set; }
    public static LevelPortalController LevelPortalController { get; set; }
    //
    [SerializeField]
    private GameObject bulletsParent;
    private BulletPlayerController[] bullets;
    private bool isShot;
    private float bulletLoadingTime = LOADING_TIME_FOR_RIFLES;
    private float currentLoadingTime;

    [Space]
    [Header("Sounds")]
    private AudioSource[] audioSources;
    [SerializeField] private AudioClip[] bulletsSFX;
    [SerializeField] private AudioClip bonusSFX;
    [SerializeField] private AudioClip changeWeaponSFX;
    [SerializeField] private AudioClip teleportSFX;

    public static bool IsSlotChangeImageKey;

    private void Start()
    {
        PlayerStats = GetComponent<PlayerBase>();
        startHeroSprite = GetComponent<SpriteRenderer>().sprite;
        bullets = bulletsParent.GetComponentsInChildren<BulletPlayerController>();
        audioSources = GetComponents<AudioSource>();
        foreach (var item in bullets)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        Cheats();

        if (currentLoadingTime < 2) currentLoadingTime += Time.deltaTime;

        if (!GameController.IsInputEnable) return;

        if (GetComponent<Platformer2DUserControl>().IsShotKey && !CanOpenSavePlace)
            isShot = IsAmmo();

        StartTeleportInPortal(NewPortalPosition);
        StartTeleportInLevelPortal();
        Shooting();
        ChangingSlotImage();
        SavePlaceEnter();
        OpenChestOperation();
        PlatformJumpDown();
    }

    #region WEAPONS_SYSTEM

    private bool IsAmmo()
    {
        bool isAmmo = true;
        int tmpNumberSlotKey = Platformer2DUserControl.NumberSlotKey;

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

        if(tmpNumberSlotKey != Platformer2DUserControl.NumberSlotKey)
            ChangingSlotOperation();

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
        if (data == "null") return;

        string[] weaponsInfo = data.Split('|');
        if (weaponsInfo[0] == "False") return;

        InventoryItems typeOfAmmo = (InventoryItems)int.Parse(weaponsInfo[1]);
        SetBoughtItem(int.Parse(weaponsInfo[3]), typeOfAmmo, int.Parse(weaponsInfo[2]), ItemsDataBase.Instance.GetAmmoSpriteByItemType(typeOfAmmo));
    }

    private void Shooting()
    {
        if (isShot && currentLoadingTime >= bulletLoadingTime)
        {
            GetComponent<PlatformerCharacter2D>().ShotAnim();
            isShot = false;
            var copy_bullet = Instantiate(bullets[GetActiveWeapon() == InventoryItems.NULL ? 0 : (int)GetActiveWeapon()], bulletsParent.transform.parent);
            copy_bullet.GetComponent<BulletPlayerController>().InitBullet(GetActiveWeapon(), GetAttribute(PlayerAttributes.ADDITIONAL_DAMAGE));
            PlayBulletSFX();
            AddItem(GetActiveWeapon(), -1);
            isShot = false;
            currentLoadingTime = 0;
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
        if (IsSlotChangeImageKey)
            ChangingSlotOperation();
    }

    public void ChangingSlotOperation(bool setDefault = false)
    {
        var numberSlot = setDefault == false ? Platformer2DUserControl.NumberSlotKey : -1;

        if (numberSlot != -1 && PlayerStats.WeaponMods[numberSlot].IsUnlock && !setDefault)
        {
            HeadPanelController.Instance.uiPanel.SetSlotImage(numberSlot, PlayerStats.WeaponMods[numberSlot].AmmoAmount);
            PlayChangeWeaponSFX();
        }
        else
        {
            HeadPanelController.Instance.uiPanel.SetSlotImage(numberSlot, -1);
            Platformer2DUserControl.NumberSlotKey = -1;
        }

        ChangeHeroAnim();

        InventoryItems tmp = GetActiveWeapon();
        switch (tmp)
        {
            case InventoryItems.NULL:
            case InventoryItems.AMMO_TYPE_3:
                bulletLoadingTime = LOADING_TIME_FOR_RIFLES;
                break;
            case InventoryItems.AMMO_TYPE_1:
            case InventoryItems.AMMO_TYPE_2:
            case InventoryItems.AMMO_TYPE_4:
                bulletLoadingTime = LOADING_TIME_FOR_HEAVY_GUNS;
                break;
            default:
                break;
        }
        HeadPanelController.Instance.uiPanel.UnlockSlot(0, ItemsDataBase.Instance.GetWeaponSpriteByItemType(tmp));
        IsSlotChangeImageKey = false;
    }

    private void ChangeHeroAnim(bool forceChange = false)
    {
        switch (GetActiveWeapon())
        {
            case InventoryItems.AMMO_TYPE_1:
                this.GetComponent<PlatformerCharacter2D>().SetAnimController(1, forceChange);
                break;
            case InventoryItems.AMMO_TYPE_2:
                this.GetComponent<PlatformerCharacter2D>().SetAnimController(2, forceChange);
                break;
            case InventoryItems.AMMO_TYPE_3:
                this.GetComponent<PlatformerCharacter2D>().SetAnimController(3, forceChange);
                break;
            case InventoryItems.AMMO_TYPE_4:
                this.GetComponent<PlatformerCharacter2D>().SetAnimController(4, forceChange);
                break;
            case InventoryItems.NULL:
                this.GetComponent<PlatformerCharacter2D>().SetAnimController(0, forceChange);
                break;
        }
    }

    #endregion

    #region HERO_OPERATIONS

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
            case PlayerAttributes.LUCK:
                PlayerBase.Luck = (int)amount;
                break;
            case PlayerAttributes.ADDITIONAL_DAMAGE:
                PlayerStats.AdditionalDamage = amount;
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
            case PlayerAttributes.LUCK:
                PlayerBase.Luck += (int)amount;
                break;
            case PlayerAttributes.ADDITIONAL_DAMAGE:
                PlayerStats.AdditionalDamage += amount;
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
            case PlayerAttributes.LUCK:
                return PlayerBase.Luck;
            case PlayerAttributes.ADDITIONAL_DAMAGE:
                return PlayerStats.AdditionalDamage;
            default:
                return -1;
        }
    }

    public void ChangeItemAmount(InventoryItems item, int amount)
    {
        switch (item)
        {
            case InventoryItems.BLACK_CRISTALS:
                PlayerStats.BlackCristals = amount;
                HeadPanelController.Instance.uiPanel.SetBlackCristals(PlayerStats.BlackCristals);
                HeadPanelController.Instance.savePlacePanel.SetBlackCristals(GetItemAmount(InventoryItems.BLACK_CRISTALS));
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
                HeadPanelController.Instance.savePlacePanel.SetBlackCristals(GetItemAmount(InventoryItems.BLACK_CRISTALS));
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

    public void CheckHeroDeath()
    {
        if (godMode || !GameController.IsInputEnable) return;

        if (PlayerStats.HP <= 0)
        {
            GameController.IsInputEnable = false;
            //gameObject.SetActive(false);
            GetComponent<PlatformerCharacter2D>().DeadAnim();
            HeadPanelController.Instance.uiPanel.ShowGameOver(false);
            GameController.Instance.wasStart = false;
        }
    }

    public void SetDefaultSprite()
    {
        ChangeHeroAnim(true);
        GetComponent<SpriteRenderer>().sprite = startHeroSprite;
    }

    #endregion

    #region SAVE_PLACE_SYSTEM

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
        HeadPanelController.Instance.savePlacePanel.SetItemBought(item);

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

    #endregion

    #region SFX_METHODS

    private void PlayBulletSFX()
    {
        if (GetActiveWeapon() == InventoryItems.AMMO_TYPE_4 || GetActiveWeapon() == InventoryItems.AMMO_TYPE_1)
            audioSources[0].clip = bulletsSFX[1];
        else
            audioSources[0].clip = bulletsSFX[0];
        audioSources[0].Play();
    }

    public void PlayBonusSFX()
    {
        audioSources[1].clip = bonusSFX;
        audioSources[1].Play();
    }

    public void PlayChangeWeaponSFX()
    {
        audioSources[2].clip = changeWeaponSFX;
        audioSources[2].Play();
    }

    public void PlayTeleportSFX()
    {
        audioSources[1].clip = teleportSFX;
        audioSources[1].Play();
    }

    #endregion

    #region TRIGGER_OPERATIONS

    private void SavePlaceEnter()
    {
        if (CanOpenSavePlace)
        {
            if (Input.GetButtonDown("SavePlace") || Input.GetKeyDown(KeyCode.UpArrow)) //Really important. Remember about it
            {
                HeadPanelController.Instance.savePlacePanel.ChangeVisibility();
                HeadPanelController.Instance.savePlacePanel.SetBlackCristals(GetItemAmount(InventoryItems.BLACK_CRISTALS));
            }
        }
    }

    private void OpenChestOperation()
    {
        if (CanOpenChest)
        {
            if (Input.GetButtonDown("SavePlace") || Input.GetKeyDown(KeyCode.UpArrow))
            {
                CanOpenChest = false;
                HeadPanelController.Instance.uiPanel.HideHelperPanel();
                HeadPanelController.Instance.questionPanel.QuestionBehaviour();
            }
        }
    }

    public void StartTeleportInPortal(Vector2 newPosition)
    {
        if (InPortal)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                this.transform.position = newPosition;
                PlayTeleportSFX();
                InPortal = false;
            }
        }
    }

    public void StartTeleportInLevelPortal()
    {
        if (InLevelPortal)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (LevelPortalController.IsActive)
                {
                    InLevelPortal = false;
                    PlayTeleportSFX();
                    HeadPanelController.Instance.uiPanel.HideHelperPanel();
                    GameController.CurrentWorld = LevelPortalController.nextSceneName;
                    SaveController.Instance.SavePrefs();
                    _GameManager.GetComponent<SceneController>().UnloadScene(LevelPortalController.thisSceneName.ToString());
                    _GameManager.GetComponent<SceneController>().LoadScene(false, LevelPortalController.nextSceneName.ToString(), SetCharacterPositionAfterChangeLevel, true);
                }
                else if(LevelPortalController.isOpenGame)
                {
                    HeadPanelController.Instance.binaryGame.StartGame();
                }
            }
        }
    }

    private void SetCharacterPositionAfterChangeLevel()
    {
        this.transform.position = LevelPortalController.startLevelPosition;
    }

    private void PlatformJumpDown()
    {
        if (CanMoveDownPlatform)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                CanMoveDownPlatform = false;
                Platform.RunPlatformOperation();
            }
        }
    }

    #endregion

    private void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            AddItem(InventoryItems.BLACK_CRISTALS, 100);
            Debug.Log("MoreMoney");
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            godMode = !godMode;
            Debug.Log("godMode" + godMode);
        }
    }

}