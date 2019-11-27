using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool IsInputEnable { get; set; }
    public static GameController Instance;

    [Header("PlayGame Options")]
    public GameObject player;
    private UIPanelController uIPanel;

    private void Start()
    {
        if (Instance == null)
            Instance = FindObjectOfType<GameController>();

        uIPanel = player.GetComponent<PlayerController>().uiPanel;
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
        uIPanel.DeactiveSlots();
        uIPanel.SetAmmo(-1);
        SaveController.Instance.SavePrefs();
    }

    private void LoadPlayerPrefs()
    {
        uIPanel.DeactiveSlots();
        uIPanel.SetAmmo(-1);
        SaveController.Instance.LoadPrefs();
    }

    private void SetCharacterPosition()
    {
        player.transform.position = player.GetComponent<PlayerBase>().StartPosition;
    }
}
