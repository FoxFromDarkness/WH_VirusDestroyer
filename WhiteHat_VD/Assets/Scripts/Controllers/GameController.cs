using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    public enum PlayableWorld
    {
        Level_Tutorial,
        Level_1,
        BossLevel_1
    }

    private static bool isInputEnable;
    public static bool IsInputEnable {
        get { return isInputEnable; }
        set { isInputEnable = value; Cursor.visible = !isInputEnable; }
    }
    public static GameController Instance { get; private set; }
    public static PlayableWorld CurrentWorld { get; set; } = 0;
    public bool wasStart = false;
    private SceneController sceneController;

    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer currentBackgroundImage;

    [Space]
    [Header("Audio Options")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float lastMusicVolume;
    [SerializeField] private float lastSFXVolume;
    [SerializeField] private bool isMute = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        sceneController = GetComponent<SceneController>();

        if (!sceneController.IsSceneLoaded("_UI"))
        {
#if !UNITY_EDITOR
            player.GetComponent<SpriteRenderer>().enabled = false;
#endif
            sceneController.LoadUIScene();
        }
    }
    void Update()
    {
#if !UNITY_EDITOR
        if(!wasStart) return;
#endif

        if (Input.GetButtonDown("Cancel"))
        {
            ShowHideMainMenu();
        }
    }

    public void StartNewGame(bool isLoading)
    {
        player.SetActive(true);
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<PlayerController>().SetDefaultSprite();
        player.GetComponent<PlayerBase>().StartPosition = new Vector3(-1240.0f, 255.0f);
        player.GetComponent<PlayerBase>().SetStartPlayerOptions();

        if (isLoading)
            LoadPlayerPrefs();
        else
            SavePlayerPrefs();

        sceneController.UnloadAllScenes(false);
        sceneController.LoadScene(false, CurrentWorld.ToString(), SetCharacterPosition);
        var playerC = player.GetComponent<PlayerController>();
        playerC.ChangeAttribute(PlayerAttributes.HP, playerC.GetAttribute(PlayerAttributes.HP_MAX));
        ShowHideMainMenu();
        wasStart = true;
    }

    public void ShowHideMainMenu()
    {
        if (HeadPanelController.Instance.savePlacePanel.gameObject.activeSelf 
            || HeadPanelController.Instance.questionPanel.gameObject.activeSelf)
        {
            HeadPanelController.Instance.savePlacePanel.ChangeVisibility(false);
            HeadPanelController.Instance.questionPanel.ChangeVisibility(false);
        }
        else
        {
            HeadPanelController.Instance.startPanel.CloseSubPanels();
            HeadPanelController.Instance.startPanel.ChangeVisibility();
        }
    }

    private void SavePlayerPrefs()
    {
        HeadPanelController.Instance.uiPanel.DeactiveSlots();
        HeadPanelController.Instance.uiPanel.SetAmmo(-1);
        SaveController.Instance.SaveDefaultPrefs();
    }

    private void LoadPlayerPrefs()
    {
        HeadPanelController.Instance.uiPanel.DeactiveSlots();
        HeadPanelController.Instance.uiPanel.SetAmmo(-1);
        SaveController.Instance.LoadPrefs();
    }

    public void SetMusicVolume(float volume)
    {
        lastMusicVolume = volume;
        if (isMute) return;
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        lastSFXVolume = volume;
        if (isMute) return;
        audioMixer.SetFloat("sfxVolume", volume);
    }

    public void Mute(bool value)
    {
        isMute = value;
        audioMixer.SetFloat("musicVolume", value == true ? -80 : lastMusicVolume);
        audioMixer.SetFloat("sfxVolume", value == true ? -80 : lastSFXVolume);
    }

    public void SetCurrentBackground(Sprite bg, Vector2 scale)
    {
        currentBackgroundImage.sprite = bg;
        currentBackgroundImage.transform.localScale = new Vector2(scale.x, scale.y);
    }

    private void SetCharacterPosition()
    {
        player.transform.position = player.GetComponent<PlayerBase>().StartPosition;
    }

    public void ExitApplication()
    {
        Debug.Log("Application Exit");
        Application.Quit();
    }
}
