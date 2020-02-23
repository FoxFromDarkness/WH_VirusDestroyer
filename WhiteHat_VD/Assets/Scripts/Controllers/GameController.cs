using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    public static bool IsInputEnable { get; set; }
    public static GameController Instance;
    public GameObject player;

    [Space]
    [Header("Audio Options")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float lastMusicVolume;
    [SerializeField] private float lastSFXVolume;
    [SerializeField] private bool isMute = false;



    private void Start()
    {
        if (Instance == null)
            Instance = this.GetComponent<GameController>();
    }

    public void StartNewGame(bool isLoading)
    {
        player.SetActive(true);
        player.GetComponent<PlayerBase>().StartPosition = new Vector3(-1240.0f, 255.0f);
        GetComponent<SceneController>().UnloadAllScenes(false);
        GetComponent<SceneController>().LoadScene(false, "GameLevel_TestLevel", SetCharacterPosition);
        player.GetComponent<PlayerBase>().SetStartPlayerOptions();

        if (isLoading)
            LoadPlayerPrefs();
        else
            SavePlayerPrefs();

        AppController.Instance.ShowHideMainMenu();
        IsInputEnable = true;
    }

    private void SavePlayerPrefs()
    {
        HeadPanelController.Instance.uiPanel.DeactiveSlots();
        HeadPanelController.Instance.uiPanel.SetAmmo(-1);
        SaveController.Instance.SavePrefs();
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

    private void SetCharacterPosition()
    {
        player.transform.position = player.GetComponent<PlayerBase>().StartPosition;
    }
}
